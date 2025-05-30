namespace MyBackendApi.Models
{
    public class QuotationClearance
    {
        public int Id { get; set; }
        public required string Username { get; set; } // ✅ User who created the quotation
        public required string CustomerName { get; set; }
        public decimal Value { get; set; }
        public double CapacityKW { get; set; }
        public required string WhatsAppNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
