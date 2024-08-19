namespace PatientRegistrationService.Models
{
    /// <summary>
    /// Represents a patient in the system with a Name, unique medical record number, age, gender, and a set of contacts.  
    /// </summary>
    public class Patient
    {
        public int MedicalRecordNumber { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public AdmittingDiagnosis? Diagnosis { get; set; }
        public Physician? AssignedPhysician { get; set; }
        public Department? AssignedDepartment { get; set; }
        public required ContactInfo Contacts { get; set; }
    }
}
