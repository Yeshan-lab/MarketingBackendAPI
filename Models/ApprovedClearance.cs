namespace MyBackendApi.Models
{
    public class ApprovedClearance
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }
        public decimal Value { get; set; }
        public double CapacityKW { get; set; }
        public required string WhatsAppNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
