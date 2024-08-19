
# Patient Registration Service

## Overview

The **Patient Registration Service** is a RESTful API built using ASP.NET Core. It allows for the capture and management of patient registration information, including patient demographics, admitting diagnosis, attending physician, and assigned department.

## Features

- Register new patients
- Update existing patient information
- Delete patients (with constraints)
- Custom validation for enum values
- Centralized error handling with custom error responses

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or any other C# IDE
- [Postman](https://www.postman.com/) for API testing

### Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/DimitarAtanassov/patient-registration-service.git
   ```

2. Navigate to the project directory:
   ```sh
   cd patient-registration-service
   ```

3. Restore the required NuGet packages:
   ```sh
   dotnet restore
   ```

4. Build the project:
   ```sh
   dotnet build
   ```

5. Run the application:
   ```sh
   dotnet run
   ```

## API Endpoints

### Get All Patients

- **Endpoint**: `/api/Patients`
- **Method**: `GET`
- **Response**: Returns a list of all registered patients.

### Get Patient by Medical Record Number (MRN)

- **Endpoint**: `/api/Patients/{medicalRecordNumber}`
- **Method**: `GET`
- **Response**: Returns the details of a specific patient by MRN.

### Register a New Patient

- **Endpoint**: `/api/Patients`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
      "name": "John Doe",
      "dateOfBirth": "1990-01-01",
      "gender": "Male",
      "diagnosis": "Lung",
      "contacts": {
          "email": "john.doe@example.com",
          "phoneNumber": "555-555-5555",
          "address": "123 Main St"
      }
  }
  ```
- **Response**: Returns the newly created patient.

### Update Patient Information

- **Endpoint**: `/api/Patients/{medicalRecordNumber}`
- **Method**: `PUT`
- **Request Body**:
  ```json
  {
      "name": "John Doe",
      "dateOfBirth": "1990-01-01",
      "gender": "Male",
      "diagnosis": "Lung",
      "contacts": {
          "email": "john.doe@example.com",
          "phoneNumber": "555-555-5555",
          "address": "123 Main St"
      }
  }
  ```
- **Response**: Returns the updated patient information.

### Delete a Patient

- **Endpoint**: `/api/Patients/{medicalRecordNumber}`
- **Method**: `DELETE`
- **Response**: Returns `204 No Content` if the patient is successfully deleted. If the patient has been diagnosed, it returns a `400 Bad Request` with an appropriate error message.

## Validation and Error Handling
### Centralized Exception Handling

The application uses a custom middleware (`ExceptionHandlingMiddleware`) to handle exceptions globally. This ensures that all exceptions are logged and the user receives a consistent error response.

In development, detailed error messages and stack traces are returned, while in production, a generic error message is provided.

## Demonstrating Functionality

To demonstrate the functionality of this service:

1. Use the provided API endpoints to create, update, and delete patients.
2. Utilize Postman or any similar tool to manually test each endpoint.
3. Access the codebase to understand the implementation details of the service.

### Code Access

To access the codebase, ensure you have cloned the repository as mentioned in the setup instructions. You can view and modify the code using any C# IDE, such as Visual Studio.

## License
Unknown
