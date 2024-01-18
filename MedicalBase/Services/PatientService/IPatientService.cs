using MedicalBase.DTOS.Patient;
using MedicalBase.Models;

namespace MedicalBase.Services.PatientService
{
    public interface IPatientService
    {
        Task<ServiceResponse<List<GetPatientDTO>>> GetAllPatients();
        Task<ServiceResponse<GetPatientDTO>> GetPatient(int id);
        Task<ServiceResponse<GetPatientDTO>> GetPatientByPesel(string pesel);
        Task<ServiceResponse<GetPatientDTO>> AddPatient(AddPatientDTO newPatient);
        Task<ServiceResponse<GetPatientDTO>> DeletePatient(int id);
        Task<ServiceResponse<GetPatientDTO>> UpdatePatient(UpdatePatientDTO updatedPatient);
        bool IsValidPesel(string pesel);
        bool isPeselIntegerAndLength(string pesel);
        bool isValidPhoneNumber(string phoneNumber);
    }
}
