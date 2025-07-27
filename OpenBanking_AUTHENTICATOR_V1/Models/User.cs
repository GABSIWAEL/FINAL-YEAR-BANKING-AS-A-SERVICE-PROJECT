namespace OpenBanking_AUTHENTICATOR_V1.Models
{
    public class User
    {
        public int Id { get; set; }

        // Google User Id or sub from Google JWT
        public string GoogleId { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
