using PatientRegistrationService.DTOs;
using PatientRegistrationService.Interfaces;
using PatientRegistrationService.Models;
using Serilog;
namespace PatientRegistrationService.Services
{
    public class PatientService(PatientDB patientDB) : IPatientService
    {
        public List<Patient> GetAllPatients()
        {
            return patientDB.GetAllPatients();
        }

        public Patient GetPatientWithMRN(int medicalRecordNumber)
        {
            try
            {
                Log.Information($"Retrieving patient with MRN: {medicalRecordNumber}.");
                
                return patientDB.GetPatientWithMRN(medicalRecordNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Error retrieving patient: {ex.Message}");
            }
        }

        // Register a new Patient
        public Patient RegisterPatient(RegisterDto registerDto)
        {
            Log.Information($"Registering new patient: {registerDto.Name}.");
            
            var newPatient = CreateNewPatient(registerDto);

            patientDB.AddPatient(newPatient);

            Log.Information($"Patient {newPatient.Name} registered with MRN: {newPatient.MedicalRecordNumber}.");
            
            return newPatient;

        }

        public Patient UpdatePatient(int medicalRecordNumber, PatientDto updatePatientInfo)
        {
            Log.Information($"Updating patient with MRN: {medicalRecordNumber}.");

            var patient = GetPatientWithMRN(medicalRecordNumber);

            if (patient == null) throw new InvalidOperationException($"Patient with MRN {medicalRecordNumber} not found.");

            // Update patient details
            if (!string.IsNullOrEmpty(updatePatientInfo.Name))
            {
                patient.Name = updatePatientInfo.Name;
            }

            if (updatePatientInfo.DateOfBirth.HasValue)
            {
                patient.DateOfBirth = updatePatientInfo.DateOfBirth.Value;
            }

            if (!string.IsNullOrEmpty(updatePatientInfo.Gender))
            {
                patient.Gender = updatePatientInfo.Gender;
            }

            if (updatePatientInfo.Contacts != null)
            {
                patient.Contacts = updatePatientInfo.Contacts;
            }


            // Case: Want to diagnose an already existing patient with no diagnosis
            if (patient.Diagnosis == null && updatePatientInfo.Diagnosis != null)
            {
                if (!Enum.IsDefined(typeof(AdmittingDiagnosis), updatePatientInfo.Diagnosis)) //  Diagnosis Type Validation
                {
                    throw new ArgumentException("Invalid admitting diagnosis.");
                }

                patient.Diagnosis = updatePatientInfo.Diagnosis;

                Log.Information($"Patient with MRN: {medicalRecordNumber}, diagnosed with {patient.Diagnosis}");
                
                AssignPhysicianAndDepartment(patient);
            }

            Log.Information($"Patient with MRN: {medicalRecordNumber} updated successfully.");
            return patient;
        }


        public bool DeletePatient(Patient patient)
        {
            if (PatientDiagnosed(patient))
            {
                return false;
            }

            return patientDB.RemovePatient(patient);
        }

        private void AssignPhysicianAndDepartment(Patient patient)
        {
            if (patient.Diagnosis == AdmittingDiagnosis.Breast || patient.Diagnosis == AdmittingDiagnosis.Lung)
            {
                patient.AssignedPhysician = new Physician { Name = "Dr. Susan Jones" };
                patient.AssignedDepartment = new Department { Name = "Department J" };
            }
            else
            {
                patient.AssignedPhysician = new Physician { Name = "Dr. Ben Smith" };
                patient.AssignedDepartment = new Department { Name = "Department S" };
            }
        }

        // Helper function to check if patient has a Diagnosis; if true cannot delete patient.
        private bool PatientDiagnosed(Patient patient)
        {
            return patient.Diagnosis != null;
        }

        private Patient CreateNewPatient(RegisterDto registerDto)
        {
            var newPatient = new Patient
            {
                Name = registerDto.Name,
                Gender = registerDto.Gender,
                DateOfBirth = registerDto.DateOfBirth,
                Diagnosis = registerDto.Diagnosis,
                Contacts = registerDto.Contacts,
                MedicalRecordNumber = patientDB.GenerateUniqueMRN()
            };

            if (newPatient.Diagnosis != null)
            {
                AssignPhysicianAndDepartment(newPatient);
            }

            return newPatient;
        }

        // Calculates Patient's age from Date of Birth for response (Would make a DateTime extension in real world use case)
        public int GetPatientAge(Patient patient)
        {
            var today = DateTime.Today;
            var age = today.Year - patient.DateOfBirth.Year;

            // Adjust Age if the patient's birthday hasn't occurred yet this year 
            if (patient.DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;

        }

        public PatientResponseDto ConvertToPatientResponseDto(Patient patient)
        {
            return new PatientResponseDto
            {
                MedicalRecordNumber = patient.MedicalRecordNumber,
                Name = patient.Name,
                Gender = patient.Gender,
                Age = GetPatientAge(patient), // Calculate Age from Date of Birth
                Diagnosis = patient.Diagnosis,
                AssignedPhysician = patient.AssignedPhysician,
                AssignedDepartment = patient.AssignedDepartment,
                Contacts = new ContactInfoDto
                {
                    Email = patient.Contacts.Email,
                    PhoneNumber = patient.Contacts.PhoneNumber,
                    Address = patient.Contacts.Address
                }
            };
        }
    }
}
