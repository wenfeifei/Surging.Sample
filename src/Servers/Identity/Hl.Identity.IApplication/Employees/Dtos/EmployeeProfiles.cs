using AutoMapper;
using Hl.Identity.Domain.Employees.Entities;

namespace Hl.Identity.IApplication.Employees.Dtos
{
    public class EmployeeProfiles : Profile
    {
        public EmployeeProfiles()
        {
            CreateMap<CreateEmployeeInput, EmployeeAggregate>();
            CreateMap<UpdateEmployeeInput, EmployeeAggregate>();
        }
    }
}
