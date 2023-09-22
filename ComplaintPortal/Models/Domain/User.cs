using System.ComponentModel.DataAnnotations;

namespace ComplaintPortal.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^(?!e$)([0-9]+)$", ErrorMessage = "Please enter a valid integer.")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }     
        public string Complaint { get; set; }
        public string Status { get; set; }
    }
}
