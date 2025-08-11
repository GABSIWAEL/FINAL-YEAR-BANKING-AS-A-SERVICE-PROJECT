using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;

public class KongService
{
    private readonly HttpClient _http;
    private readonly string _adminBase;

    public KongService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _adminBase = config["Kong:AdminBase"]?.TrimEnd('/') ?? throw new ArgumentNullException("Kong:AdminBase");
    }

    public async Task CreateConsumerWithKeyAclAndRateAsync(int userId, string apiKey, string plan, bool isAdmin = false)
    {
        var consumerId = $"user-{userId}";

        // 1 - create consumer (PUT is idempotent)
        var consumerPayload = new { username = consumerId };
        await PostJson($"{_adminBase}/consumers/{consumerId}", consumerPayload, HttpMethod.Put);

        // 2 - add key (POST key-auth) - duplicates may return 409, don't fail on that
        var keyPayload = new { key = apiKey };
        await PostJson($"{_adminBase}/consumers/{consumerId}/key-auth", keyPayload, HttpMethod.Post);

        // 3 - add ACL group(s)
        var group = isAdmin ? "admin" : plan.ToLower();
        await PostJson($"{_adminBase}/consumers/{consumerId}/acls", new { group }, HttpMethod.Post);

        // 4 - add rate-limiting plugin per consumer (map plan -> limits)
        var limits = plan.ToUpper() switch
        {
            "BASIC" => new { minute = 10, hour = 600, day = 10000 },
            "PRO"   => new { minute = 50, hour = 3000, day = 100000 },
            "ELITE" => new { minute = 200, hour = 15000, day = 1000000 },
            "SUPER" => new { minute = 1000, hour = 60000, day = 10000000 },
            _       => new { minute = 0, hour = 0, day = 0 } // DENIED or default = no limit or blocked
        };

        var pluginPayload = new
        {
            name = "rate-limiting",
            config = new { minute = limits.minute, hour = limits.hour, day = limits.day }
        };

        await PostJson($"{_adminBase}/consumers/{consumerId}/plugins", pluginPayload, HttpMethod.Post);
    }

private async Task PostJson(string url, object payload, HttpMethod method)
{
    var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
    HttpResponseMessage res = method == HttpMethod.Post
        ? await _http.PostAsync(url, content)
        : await _http.PutAsync(url, content);

    if (!res.IsSuccessStatusCode && res.StatusCode != HttpStatusCode.Conflict)
    {
        var body = await res.Content.ReadAsStringAsync();
        throw new Exception($"Kong error on {url}: {(int)res.StatusCode} {res.ReasonPhrase} - {body}");
    }
}

}
