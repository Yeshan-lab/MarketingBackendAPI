using System.Text.Json.Serialization;

namespace MyBackendApi.Models
{
    public class Negotiation
    {
        public int Id { get; set; }  // Primary key
        public required string Username { get; set; } // ✅ User who created the quotation
        public required string CustomerName { get; set; }
        public decimal Value { get; set; }  // Amount in decimal
        public double CapacityKW { get; set; }
        [JsonPropertyName("whatsAppNumber")]
        public required string WhatsAppNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;// Optionally, add date of negotiation
    }
}
