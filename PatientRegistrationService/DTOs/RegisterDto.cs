using PatientRegistrationService.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PatientRegistrationService.DTOs
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public AdmittingDiagnosis? Diagnosis { get; set; }
        public required ContactInfo Contacts { get; set; }
    }
}
