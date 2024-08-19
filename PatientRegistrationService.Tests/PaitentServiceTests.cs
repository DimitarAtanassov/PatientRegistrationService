using PatientRegistrationService.Models;
using PatientRegistrationService.Services;

namespace PatientRegistrationService.Tests
{
    public class PatientServiceTests
    {
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _service = new PatientService();
        }

        [Fact]
        public void GetPatientAge_ShouldReturnCorrectAge_WhenBirthdayHasPassed()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "John Doe",
                Gender = "Male",
                Contacts = new ContactInfo { Email = "john@example.com", PhoneNumber = "123456789", Address = "123 Main St" },
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act
            var age = _service.GetPatientAge(patient);

            // Assert
            Assert.Equal(DateTime.Today.Year - 1980, age);
        }

        [Fact]
        public void GetPatientAge_ShouldReturnCorrectAge_WhenBirthdayIsToday()
        {
            // Arrange
            var today = DateTime.Today;
            var patient = new Patient
            {
                Name = "Jane Doe",
                Gender = "Female",
                Contacts = new ContactInfo { Email = "jane@example.com", PhoneNumber = "987654321", Address = "456 Another St" },
                DateOfBirth = new DateTime(today.Year - 30, today.Month, today.Day)
            };

            // Act
            var age = _service.GetPatientAge(patient);

            // Assert
            Assert.Equal(30, age);
        }

        [Fact]
        public void GetPatientAge_ShouldReturnCorrectAge_WhenBirthdayHasNotPassedThisYear()
        {
            // Arrange
            var today = DateTime.Today;
            var patient = new Patient
            {
                Name = "Jake Doe",
                Gender = "Male",
                Contacts = new ContactInfo { Email = "jake@example.com", PhoneNumber = "456789123", Address = "789 Third St" },
                DateOfBirth = new DateTime(today.Year - 30, today.Month + 1, today.Day)
            };

            // Act
            var age = _service.GetPatientAge(patient);

            // Assert
            Assert.Equal(29, age);
        }
    }
}