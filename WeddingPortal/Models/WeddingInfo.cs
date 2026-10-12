using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPortal.Models
{
    public class WeddingInfo
    {
        public int Id { get; set; }

        // Koppling till Event
        public int? EventId { get; set; }

        public Event? Event { get; set; }

        [Required(ErrorMessage = "Welcome message is required.")]
        [StringLength(1000)]
        public string WelcomeMessage { get; set; } = "";

        [Required(ErrorMessage = "Venue information is required.")]
        [StringLength(1000)]
        public string VenueInformation { get; set; } = "";

        [Required(ErrorMessage = "Contact information is required.")]
        [StringLength(500)]
        public string ContactInformation { get; set; } = "";

        [StringLength(200)]
        public string? HeroTitle { get; set; }

        [StringLength(200)]
        public string? HeroSubtitle { get; set; }

        [StringLength(2000)]
        public string? HeroDescription { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}