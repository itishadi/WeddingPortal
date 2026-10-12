using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPortal.Models
{
    public class WeddingInfo
    {
        public int Id { get; set; }

        public int? EventId { get; set; }

        public Event? Event { get; set; }

        [Required]
        [StringLength(1000)]
        public string WelcomeMessage { get; set; } = "";

        [Required]
        [StringLength(1000)]
        public string VenueInformation { get; set; } = "";

        [Required]
        [StringLength(500)]
        public string ContactInformation { get; set; } = "";

        public string? HeroTitle { get; set; }

        public string? HeroSubtitle { get; set; }

        public string? HeroDescription { get; set; }

        // Galleri
        public List<WeddingImage> Images { get; set; } = new();

        // Uppladdade filer
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } = new();
    }
}