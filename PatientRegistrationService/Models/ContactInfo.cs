namespace PatientRegistrationService.Models
{
    /// <summary>
    /// Represents a Patient's contact info
    /// </summary>
    public class ContactInfo
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

    }
}
