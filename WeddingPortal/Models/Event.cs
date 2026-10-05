using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Location { get; set; } = "";

        [Required]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }
    }
}