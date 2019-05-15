using Hl.Identity.Domain.Employees.Entities;
using Surging.Core.CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Employees
{
    public interface IEmployeeManager : ITransientDependency
    {
        Task CreateEmployee(EmployeeAggregate employee);
    }
}
