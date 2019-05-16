using Hl.Identity.Domain.Employees.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.Employees.Dtos
{
    public class CreateEmployeeInput : EmployeeDtoBase
    {

        [Required(ErrorMessage = "用户名不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "用户名不允许为空")]
        public string UserName { get; set; }
    }
}
