using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendApi.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        // Foreign Keys
        public int? AdminId { get; set; }
        public int? OfficerId { get; set; }

        // Navigation Properties
        [ForeignKey("AdminId")]
        public Admin? Admin { get; set; }

        [ForeignKey("OfficerId")]
        public MarketingOfficer? Officer { get; set; }
    }
}