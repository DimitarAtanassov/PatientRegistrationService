using PatientRegistrationService.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientRegistrationService.DTOs
{
    public class PatientResponseDto
    {
        public int MedicalRecordNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public AdmittingDiagnosis? Diagnosis { get; set; }
        public Physician? AssignedPhysician { get; set; }
        public Department? AssignedDepartment { get; set; }
        public ContactInfoDto Contacts { get; set; } = null!;
    }
}
