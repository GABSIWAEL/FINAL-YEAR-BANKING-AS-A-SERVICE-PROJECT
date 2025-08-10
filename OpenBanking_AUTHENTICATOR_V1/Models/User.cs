using System.ComponentModel.DataAnnotations;

namespace OpenBanking_AUTHENTICATOR_V1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        // Google User Id or sub from Google JWT
        public string GoogleId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool isActive { get; set; } = true;  // Default value is true if user is active by default
        public Etat Society_Type { get; set; } = Etat.USER;  // Default to USER if not specified
    }
}
