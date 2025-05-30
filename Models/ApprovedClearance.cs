namespace MyBackendApi.Models
{
    public class ApprovedClearance
    {
        public int Id { get; set; }
        public required string Username { get; set; } // ✅ This associates the record with the user

        public required string CustomerName { get; set; }

        public decimal Value { get; set; }

        public decimal CapacityKW { get; set; }

        public required string WhatsAppNumber { get; set; }

        public DateTime Date { get; set; }
    }

}
    
