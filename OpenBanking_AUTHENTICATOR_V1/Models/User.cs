using System.ComponentModel.DataAnnotations;

namespace OpenBanking_AUTHENTICATOR_V1.Models
{
    public class User
{
    [Key]
    public int Id { get; set; }
    public string? GoogleId { get; set; }  // nullable
    public string? Email { get; set; }     // nullable
    public string? Name { get; set; }      // nullable
    public string? Password { get; set; }  // already nullable
    public bool isActive { get; set; } = true;
    public Etat Society_Type { get; set; } = Etat.USER;
    public BuisnessPlan buisnessPlan { get; set; } = BuisnessPlan.DENIED;
    public string? ApiKey { get; set; }
    public DateTime? ApiKeyExpiry { get; set; }
}

}
