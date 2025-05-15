namespace MyBackendApi.Models
{
    public class MarketingOfficer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WhatsAppNumber { get; set; }

        // Constructor initializes Name and WhatsAppNumber
        public MarketingOfficer(string name, string whatsAppNumber)
        {
            Name = name;
            WhatsAppNumber = whatsAppNumber;
            JoinDate = DateTime.Now; // Optional: Assign a default value for JoinDate
        }

        public DateTime JoinDate { get; set; } // You can also initialize this here if needed
    }
}
