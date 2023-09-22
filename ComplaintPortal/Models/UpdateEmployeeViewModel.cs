namespace ComplaintPortal.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; } // Guid → unique identifier
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
    }
}
