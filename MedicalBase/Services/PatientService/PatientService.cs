using AutoMapper;
using MedicalBase.DTOS.Patient;
using MedicalBase.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.RegularExpressions;

namespace MedicalBase.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private static List<Patient> patients = new List<Patient>
        { 
            new Patient(),
            new Patient { Id = 1, FirstName = "Krzysztof", Pesel = "10987654321"} 
        };

        public bool IsValidPesel(string pesel)
        {
            string peselPattern = @"^\d{11}$";

            if (Regex.IsMatch(pesel, peselPattern))
            {
                int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
                int sum = 0;
                for (int i = 0; i < 11; i++)
                {
                    sum += (pesel[i] - '0') * weights[i];
                }

                return sum % 10 == 0;
            }

            return false;
        }

        public bool isPeselIntegerAndLength(string pesel)
        {
            if (pesel.Length == 11 && long.TryParse(pesel, out _))
            {
                return true;
            }

            return false;
        }

        public bool isValidPhoneNumber(string phoneNumber)
        {
            string phoneNumberPattern = @"^[0-9]{9}$"; ;
            return Regex.IsMatch(phoneNumber, phoneNumberPattern);

        }

        private readonly IMapper _mapper;

        public PatientService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPatientDTO>>> GetAllPatients()
        {
            var serviceResponse = new ServiceResponse<List<GetPatientDTO>>
                { Data = patients.Select(c => _mapper.Map<GetPatientDTO>(c)).ToList() };
            ;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPatientDTO>> GetPatient(int id)
        {
            var serviceResponse = new ServiceResponse<GetPatientDTO>();

            try
            {
                var patient = patients.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Patient not found.");
                serviceResponse.Data = _mapper.Map<GetPatientDTO>(patient);
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPatientDTO>> GetPatientByPesel(string pesel)
        {
            var serviceResponse = new ServiceResponse<GetPatientDTO>();

            try
            {
                var patient = patients.FirstOrDefault(x => x.Pesel == pesel) ?? throw new Exception("Patient not found.");
                serviceResponse.Data = _mapper.Map<GetPatientDTO>(patient);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPatientDTO>> AddPatient(AddPatientDTO newPatient)
        {
            var serviceResponse = new ServiceResponse<GetPatientDTO>();
            if (!isPeselIntegerAndLength(newPatient.Pesel))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Pesel is not valid";
                return serviceResponse;
            }

            if (!IsValidPesel(newPatient.Pesel))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Check sum is not valid!";
                return serviceResponse;
            }

            if (!isValidPhoneNumber(newPatient.PhoneNumber))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Phone number is not valid!";
                return serviceResponse;
            }

            if (patients.Any(x => x.Pesel == newPatient.Pesel))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Patient already exists!";
                return serviceResponse;
            }

            var patient = _mapper.Map<Patient>(newPatient);
            patient.Id = patients.Max(c => c.Id) + 1;
            patient.CreatedDate = DateTime.Now;
            patients.Add(patient);
            serviceResponse.Data = _mapper.Map<GetPatientDTO>(patient);

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetPatientDTO>> DeletePatient(int id)
        {
            var serviceResponse = new ServiceResponse<GetPatientDTO>();

            try
            {
                var patient = patients.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Patient not found.");
                patients.Remove(patient);
                serviceResponse.Data = _mapper.Map<GetPatientDTO>(patient);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPatientDTO>> UpdatePatient(UpdatePatientDTO updatedPatient)
        {
            var serviceResponse = new ServiceResponse<GetPatientDTO>();

            try
            {
                var patient = patients.FirstOrDefault(c => c.Id == updatedPatient.Id) ?? throw new Exception("Patient not found.");

                if (!isPeselIntegerAndLength(updatedPatient.Pesel))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Pesel is not valid";
                    return serviceResponse;
                }

                if (!IsValidPesel(updatedPatient.Pesel))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Check sum is not valid!";
                    return serviceResponse;
                }

                if (!isValidPhoneNumber(updatedPatient.PhoneNumber))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Phone number is not valid!";
                    return serviceResponse;
                }

                patient.FirstName = updatedPatient.FirstName;
                patient.LastName = updatedPatient.LastName;
                patient.Pesel = updatedPatient.Pesel;
                patient.PhoneNumber = updatedPatient.PhoneNumber;

                serviceResponse.Data = _mapper.Map<GetPatientDTO>(patient);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

    }
}
