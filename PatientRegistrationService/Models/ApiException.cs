namespace PatientRegistrationService.Models
{
    //  ApiException Mopdel represents the error details when an exception occurs during request processing
    public class ApiException
    {
        public int StatusCode { get; }
        public string Message { get; }
        public string? Details { get; }

        public ApiException(int statusCode, string message, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
