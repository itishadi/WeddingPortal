//using System.ComponentModel.DataAnnotations;

//namespace WeddingPortal.Models
//{
//    public class Event
//    {
//        public int Id { get; set; }

//        [Required]
//        public string Title { get; set; } = "";

//        [Required]
//        public string Location { get; set; } = "";

//        [Required]
//        public DateTime EventDate { get; set; }

//        public string? Description { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(150, ErrorMessage = "Location cannot be longer than 150 characters.")]
        public string Location { get; set; } = "";

        [Required(ErrorMessage = "Event date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string? Description { get; set; }
    }
}