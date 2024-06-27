using AutoMapper;
using intEmp.Dto;
using intEmp.Entity;

namespace intEmp.Profiles;

public class EmployeeProfile: Profile
{
    public EmployeeProfile()
    {
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary));

            CreateMap<Salary, SalaryResponseDto>();
        }
    }
}