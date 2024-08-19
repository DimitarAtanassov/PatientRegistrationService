using PatientRegistrationService.Models;
using Serilog;
namespace PatientRegistrationService
{

    /// <summary>
    /// Represents an in-memory storage.
    /// Doesn't involve I/O bound operations like db access or network access calls, so asynchronous method implementation wouldn't provide any real benefits 
    /// </summary>
    public class PatientDB
    {
        private readonly List<Patient> _patients = new List<Patient>();

        private Random _rand = new Random();

        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        public Patient GetPatientWithMRN(int medicalRecordNumber)
        {
            var patient = _patients.FirstOrDefault(p => p.MedicalRecordNumber == medicalRecordNumber);

            if (patient == null) throw new InvalidOperationException($"Patient with MRN {medicalRecordNumber} not found.");

            Log.Information($"Patient with MRN: {medicalRecordNumber} found.");

            return patient;
        }

        public void AddPatient(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");

            if (!IsMedicalRecordNumberUnique(patient.MedicalRecordNumber)) throw new InvalidOperationException($"Patient with MRN {patient.MedicalRecordNumber} already exists.");

            _patients.Add(patient);
        }

        public bool RemovePatient(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");
            }

            var existingPatient = GetPatientWithMRN(patient.MedicalRecordNumber);
            if (existingPatient == null)
            {
                throw new InvalidOperationException($"Patient with MRN {patient.MedicalRecordNumber} does not exist.");
            }

            return _patients.Remove(existingPatient);
        }

        /// <summary>
        /// Checks if a patient with the specified MRN exists in the database.
        /// </summary>
        /// <param name="medicalRecordNumber">The MRN to check.</param>
        /// <returns>True if MRN is unique, false otherwise.</returns>
        public bool IsMedicalRecordNumberUnique(int medicalRecordNumber)
        {
            return !_patients.Any(p => p.MedicalRecordNumber == medicalRecordNumber);
        }

        /// <summary>
        /// Generates a unique Medical Record Number (MRN) for a new patient.
        /// </summary>
        /// <returns>A unique MRN.</returns>
        public int GenerateUniqueMRN()
        {
            int uniqueMRN;
            do
            {
                uniqueMRN = _rand.Next(100000, 10000000);
            } while (!IsMedicalRecordNumberUnique(uniqueMRN));

            return uniqueMRN;
        }
    }
}
