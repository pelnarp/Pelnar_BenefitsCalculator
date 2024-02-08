using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Automapper
{
    public class EmployeeDtoProfile : Profile
    {
        public EmployeeDtoProfile()
        {
            // CPE
            CreateMap<Employee, GetEmployeeDto>()
            .ForMember(x => x.Id, map => map.MapFrom(x => x.Id))
            .ForMember(x => x.FirstName, map => map.MapFrom(x => x.FirstName))
            .ForMember(x => x.LastName, map => map.MapFrom(x => x.LastName))
            .ForMember(x => x.Salary, map => map.MapFrom(x => x.Salary))
            .ForMember(x => x.DateOfBirth, map => map.MapFrom(x => x.DateOfBirth));

            CreateMap<Dependent, GetDependentDto>()
            .ForMember(x => x.Id, map => map.MapFrom(x => x.Id))
            .ForMember(x => x.FirstName, map => map.MapFrom(x => x.FirstName))
            .ForMember(x => x.LastName, map => map.MapFrom(x => x.LastName))
            .ForMember(x => x.Relationship, map => map.MapFrom(x => x.Relationship))
            .ForMember(x => x.DateOfBirth, map => map.MapFrom(x => x.DateOfBirth));
        }
    }
}