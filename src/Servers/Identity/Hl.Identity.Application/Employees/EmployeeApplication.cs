using System.Threading.Tasks;
using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employees;
using Hl.Identity.Domain.Employees.Entities;
using Hl.Identity.IApplication.Employees;
using Hl.Identity.IApplication.Employees.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;


namespace Hl.Identity.Application.Employees
{
    [ModuleName(ApiConsts.Identity.ServiceKey,Version = "v1")]
    public class EmployeeApplication : ProxyServiceBase, IEmployeeApplication
    {
        private readonly IDapperRepository<EmployeeAggregate, long> _employeeRepository;
        private readonly IDapperRepository<UserInfo, long> _userRepository;
        private readonly IEmployeeManager _employeeManager;

        public EmployeeApplication(IDapperRepository<EmployeeAggregate, long> employeeRepository,
            IDapperRepository<UserInfo, long> userRepository,
            IEmployeeManager employeeManager)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _employeeManager = employeeManager;
        }

        public async Task<string> Create(CreateEmployeeInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitEployee = await _employeeRepository.FirstOrDefaultAsync(p => p.UserName == input.UserName 
            || p.Email == input.Email 
            || p.Phone == input.Phone);
            if (exsitEployee != null)
            {
                throw new BusinessException("已经存在该员工信息,请检查员工账号信息");
            }

            var exsitUserInfo = await _userRepository.FirstOrDefaultAsync(p => p.UserName == input.UserName
            || p.Email == input.Email
            || p.Phone == input.Phone);
            if (exsitUserInfo != null)
            {
                throw new BusinessException("已经存在该员工信息,请检查员工账号信息");
            }
            var employee = input.MapTo<EmployeeAggregate>();
            await GetService<IEmployeeManager>().CreateEmployee(employee);
            return "新增员工成功";
        }

        public async Task<string> Delete(DeleteByIdInput input)
        {
            await _employeeManager.DeleteEmployeeById(input.Id);
            return "删除员工成功";
        }

        public async Task<string> Update(UpdateEmployeeInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var employee = await _employeeRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (employee == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的员工信息");
            }
            if (input.Email != employee.Email)
            {
                var exsitEmployee = await _employeeRepository.FirstOrDefaultAsync(p => p.Email == input.Email);
                if (exsitEmployee != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Email}的员工信息");
                }
                var exsitUser = await _userRepository.FirstOrDefaultAsync(p => p.Email == input.Email);
                if (exsitUser != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Email}的用户信息");
                }
            }
            if (input.Phone != employee.Phone)
            {
                var exsitEmployee = await _employeeRepository.FirstOrDefaultAsync(p => p.Phone == input.Phone);
                if (exsitEmployee != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Phone}的员工信息");
                }
                var exsitUser = await _userRepository.FirstOrDefaultAsync(p => p.Phone == input.Phone);
                if (exsitUser != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Phone}的用户信息");
                }
            }
            employee = input.MapTo(employee);
            await _employeeRepository.UpdateAsync(employee);
            return "更新员工信息成功";
        }
    }
}
