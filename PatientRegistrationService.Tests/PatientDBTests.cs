using PatientRegistrationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRegistrationService.Tests
{
    public class PatientDBTests
    {
        [Fact]
        public void IsMedicalRecordNumberUnique_ShouldReturnTrue_WhenMRNIsUnique()
        {
            // Arrange
            var patientDB = new PatientDB();
            var existingPatient = new Patient
            {
                Name = "John Doe",
                Gender = "Male",
                Contacts = new ContactInfo { Email = "john@example.com", PhoneNumber = "123456789", Address = "123 Main St" },
                MedicalRecordNumber = 123456
            };
            patientDB.AddPatient(existingPatient);

            // Act
            var isUnique = patientDB.IsMedicalRecordNumberUnique(654321);

            // Assert
            Assert.True(isUnique);
        }

        [Fact]
        public void IsMedicalRecordNumberUnique_ShouldReturnFalse_WhenMRNIsNotUnique()
        {
            // Arrange
            var patientDB = new PatientDB();
            var existingPatient = new Patient
            {
                Name = "John Doe",
                Gender = "Male",
                Contacts = new ContactInfo { Email = "john@example.com", PhoneNumber = "123456789", Address = "123 Main St" },
                MedicalRecordNumber = 123456
            };
            patientDB.AddPatient(existingPatient);

            // Act
            var isUnique = patientDB.IsMedicalRecordNumberUnique(123456);

            // Assert
            Assert.False(isUnique);
        }

        [Fact]
        public void GenerateUniqueMRN_ShouldReturnUniqueMRN_WhenCalled()
        {
            // Arrange
            var patientDB = new PatientDB();
            var firstMRN = patientDB.GenerateUniqueMRN();
            var secondMRN = patientDB.GenerateUniqueMRN();

            // Act
            var isUnique = firstMRN != secondMRN;

            // Assert
            Assert.True(isUnique);
        }
    }
}
