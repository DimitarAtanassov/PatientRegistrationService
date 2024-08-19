using PatientRegistrationService.DTOs;
using PatientRegistrationService.Models;

namespace PatientRegistrationService.Interfaces
{
    public interface IPatientService
    {
        public List<Patient> GetAllPatients();
        public Patient GetPatientWithMRN(int medicalRecordNumber);
        public Patient UpdatePatient(int medicalRecordNumber, PatientDto updatePatientInfo);
        public bool DeletePatient(Patient patient);
        public Patient RegisterPatient(RegisterDto registerDto);
        public int GetPatientAge(Patient patient); // DateTime -> int for Paitent Age
        public PatientResponseDto ConvertToPatientResponseDto(Patient patient);
    }
}
