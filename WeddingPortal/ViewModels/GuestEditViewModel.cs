using System.ComponentModel.DataAnnotations;

namespace WeddingPortal.ViewModels
{
    public class GuestEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string? PhoneNumber { get; set; }

        [Range(0, 10, ErrorMessage = "Number of attendants must be between 0 and 10.")]
        public int NumberOfAttendants { get; set; }

        public bool? WillAttend { get; set; }

        [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters.")]
        public string? Message { get; set; }
    }
}