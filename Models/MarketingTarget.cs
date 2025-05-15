namespace MyBackendApi.Models
{
    public class MarketingTarget
    {
        public int Id { get; set; }
        public required string MarketingOfficerName { get; set; }
        public decimal TargetValue { get; set; }
        public double TargetCapacityKW { get; set; }
        public DateTime TargetMonth { get; set; }
    }
}
