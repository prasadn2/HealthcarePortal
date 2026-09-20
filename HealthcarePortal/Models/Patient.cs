namespace HealthcarePortal.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; } = string.Empty;
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
