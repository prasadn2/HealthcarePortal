using AutoMapper;
using HealthcarePortal.DTOs;
using HealthcarePortal.Models;

namespace HealthcarePortal.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreatePatientDTO, Patient>();
            CreateMap<UpdatePatientDTO, Patient>();
            CreateMap<Patient, PatientResponseDTO>();
        }
    }
}