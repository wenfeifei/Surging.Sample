using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.IApplication.Employees.Dtos
{
    public class UpdateEmployeeInput : EmployeeDtoBase
    {
        public long Id { get; set; }
    }
}
