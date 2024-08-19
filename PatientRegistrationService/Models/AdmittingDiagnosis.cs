using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PatientRegistrationService.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // serialize enums as strings for req to api and resp from api
    public enum AdmittingDiagnosis
    {
        Breast,
        Lung,
        Prostate,
        Unspecified
    }
}
