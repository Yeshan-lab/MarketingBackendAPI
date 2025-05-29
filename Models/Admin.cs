namespace MyBackendApi.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string WhatsAppNumber { get; set; } = null!;
        public DateTime JoinedDate { get; set; }
        // Optional navigation property
        public ICollection<UserLogin>? UserLogins { get; set; }

    }
}
