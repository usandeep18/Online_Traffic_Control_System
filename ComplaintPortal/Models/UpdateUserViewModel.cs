namespace ComplaintPortal.Models
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Complaint { get; set; }
        public string Status { get; set; }
    }
}
