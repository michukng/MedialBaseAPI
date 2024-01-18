using AutoMapper;
using MedicalBase.DTOS.Patient;
using MedicalBase.Models;

namespace MedicalBase
{
    public class AutoProfileMapper : Profile
    {
        public AutoProfileMapper() 
        {
            CreateMap<Patient, GetPatientDTO>();
            CreateMap<AddPatientDTO, Patient>();
        }
    }
}
