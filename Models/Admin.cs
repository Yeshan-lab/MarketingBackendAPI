namespace MyBackendApi.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string WhatsAppNumber { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
