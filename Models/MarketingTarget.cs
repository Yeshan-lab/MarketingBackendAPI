namespace MyBackendApi.Models
{
    public class MarketingTarget
    {
        public int Id { get; set; } // Primary Key
        public string Username { get; set; } // Who submitted the target
        public int Month { get; set; } // Target month (1–12)
        public int Year { get; set; } // Target year (e.g., 2025)
        public decimal ValueTarget { get; set; } // Value in LKR
        public decimal KWTarget { get; set; } // change from double to decimal
    }
}
