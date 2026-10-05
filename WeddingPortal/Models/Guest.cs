using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string? PhoneNumber { get; set; }

        [Range(0, 10)]
        public int NumberOfAttendants { get; set; }

        public bool WillAttend { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }
    }
}