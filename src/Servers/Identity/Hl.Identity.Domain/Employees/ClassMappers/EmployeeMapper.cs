using Hl.Core.ClassMapper;
using Hl.Identity.Domain.Employees.Entities;


namespace Hl.Identity.Domain.Employees.ClassMappers
{
    class EmployeeMapper : HlClassMapper<EmployeeAggregate>
    {
        public EmployeeMapper()
        {
            Table("auth_employee");            
            Map(p => p.UserInfos).Ignore();
            AutoMap();
        }
    }
}
