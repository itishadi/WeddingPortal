using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.Models
{
    public class WeddingInfo
    {
        public int Id { get; set; }

        [Required]
        public string WelcomeMessage { get; set; } = "";

        public string VenueInformation { get; set; } = "";

        public string ContactInformation { get; set; } = "";
    }
}