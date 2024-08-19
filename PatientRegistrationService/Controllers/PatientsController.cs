using Microsoft.AspNetCore.Mvc;
using PatientRegistrationService.DTOs;
using PatientRegistrationService.Interfaces;
using PatientRegistrationService.Models;

namespace PatientRegistrationService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {

        //GET /api/Patients
        [HttpGet]
        public ActionResult<List<PatientResponseDto>> GetPatients()
        {
            var patients = patientService.GetAllPatients();

            var patientResponses = patients.Select(patientService.ConvertToPatientResponseDto).ToList();

            return Ok(patientResponses);
        }

        //GET /api/Patients/{medicalRecordNumber}
        [HttpGet("{medicalRecordNumber}")]
        public ActionResult<PatientResponseDto> GetPatient(int medicalRecordNumber)
        {
            var patient = patientService.GetPatientWithMRN(medicalRecordNumber);

            if (patient == null) return NotFound($"Patient with MRN: {medicalRecordNumber} could NOT be found");

            var patientResponse = patientService.ConvertToPatientResponseDto(patient);

            return Ok(patientResponse);
        }

        //PUT /api/Patients/{medicalRecordNumber}
        [HttpPut("{medicalRecordNumber}")]
        public ActionResult<Patient> UpdatePatient(int medicalRecordNumber, PatientDto updatePatientInfo)
        {
            var patient = patientService.UpdatePatient(medicalRecordNumber, updatePatientInfo);

            if (patient == null) return NotFound($"Patient with MRN: {medicalRecordNumber} could NOT be found");

            var patientResponse = patientService.ConvertToPatientResponseDto(patient);


            return Ok(patientResponse);
        }

        //POST /api/Patients/
        [HttpPost]
        public ActionResult<Patient> RegisterPatient(RegisterDto registerDto)
        {
            var registeredPatient = patientService.RegisterPatient(registerDto);

            var patientResponse = patientService.ConvertToPatientResponseDto(registeredPatient);

            return CreatedAtAction(nameof(GetPatient), new { medicalRecordNumber = registeredPatient.MedicalRecordNumber }, patientResponse);

        }

        //DELETE /api/Patients/{medicalRecordNumber}
        [HttpDelete("{medicalRecordNumber}")]
        public ActionResult<Patient> DeletePatient(int medicalRecordNumber)
        {
            var patientToDelete = patientService.GetPatientWithMRN(medicalRecordNumber);

            if (patientToDelete == null) return NotFound(new { status = 404, message = $"Cannot find patient with ID: {medicalRecordNumber}" });

            bool patientDeleted = patientService.DeletePatient(patientToDelete);

            if (patientDeleted) return NoContent();

            return BadRequest(new { status = 400, message = $"Can't delete {patientToDelete.Name}, already diagnosed" });
        }
    }
}
