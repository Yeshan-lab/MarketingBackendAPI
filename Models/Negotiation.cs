namespace MyBackendApi.Models
{
    public class Negotiation
    {
        public int Id { get; set; }  // Primary key
        public required string Username { get; set; } // ✅ User who created the quotation
        public required string CustomerName { get; set; }
        public decimal Value { get; set; }  // Amount in decimal
        public double CapacityKW { get; set; }
        public required string WhatsAppNumber { get; set; }
        public DateTime Date { get; set; } // Optionally, add date of negotiation
    }
}
