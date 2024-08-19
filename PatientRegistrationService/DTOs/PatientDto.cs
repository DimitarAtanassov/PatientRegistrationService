using PatientRegistrationService.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientRegistrationService.DTOs
{

    /// <summary>
    /// Data Transfer Object: used for updating existing patient information.
    /// </summary>
    public class PatientDto
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public AdmittingDiagnosis? Diagnosis { get; set; }
        public ContactInfo? Contacts { get; set; }
    }
}
