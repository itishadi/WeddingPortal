//using System.ComponentModel.DataAnnotations;

//namespace WeddingPortal.Models
//{
//    public class WeddingInfo
//    {
//        public int Id { get; set; }

//        [Required]
//        public string WelcomeMessage { get; set; } = "";

//        public string VenueInformation { get; set; } = "";

//        public string ContactInformation { get; set; } = "";
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.Models
{
    public class WeddingInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Welcome message is required.")]
        [StringLength(1000, ErrorMessage = "Welcome message cannot be longer than 1000 characters.")]
        public string WelcomeMessage { get; set; } = "";

        [Required(ErrorMessage = "Venue information is required.")]
        [StringLength(1000, ErrorMessage = "Venue information cannot be longer than 1000 characters.")]
        public string VenueInformation { get; set; } = "";

        [Required(ErrorMessage = "Contact information is required.")]
        [StringLength(500, ErrorMessage = "Contact information cannot be longer than 500 characters.")]
        public string ContactInformation { get; set; } = "";
    }
}