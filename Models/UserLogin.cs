namespace MyBackendApi.Models
{
    public class UserLogin
    {
        public int Id { get; set; }

 
        public required string Username { get; set; } = null!;

        
        public required string Password { get; set; } = null!;

        
        public required string Role { get; set; } = null!;

    }
}
