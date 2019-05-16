using Hl.BasicData.Common.SystemConf;
using Hl.Core.ServiceApi;
using Hl.Core.Utils;
using Hl.Identity.Domain.Authorization;
using Hl.Identity.Domain.Authorization.UserGroups;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employees.Entities;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Employees
{
    public class EmployeeManager : ManagerBase, IEmployeeManager
    {
        private readonly IDapperRepository<EmployeeAggregate, long> _employeeRepository;
        private readonly IDapperRepository<UserInfo, long> _userRepository;
        private readonly IDapperRepository<UserRole, long> _userRoleRepository;
        private readonly IDapperRepository<UserGroupRelation, long> _userGroupRelationRepository;

        private readonly IServiceProxyProvider _serviceProxyProvider;
        private readonly IPasswordHelper _passwordHelper;

        public EmployeeManager(IDapperRepository<EmployeeAggregate, long> employeeRepository,
            IDapperRepository<UserInfo, long> userRepository,
            IDapperRepository<UserRole, long> userRoleRepository,
            IDapperRepository<UserGroupRelation, long> userGroupRelationRepository,
            IServiceProxyProvider serviceProxyProvider,
            IPasswordHelper passwordHelper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userGroupRelationRepository = userGroupRelationRepository;
            _serviceProxyProvider = serviceProxyProvider;
            _passwordHelper = passwordHelper;
        }

        public async Task CreateEmployee(EmployeeAggregate employee)
        {
           
            await UnitOfWorkAsync(async (conn, tran) =>
            {
                var employeeId = await _employeeRepository.InsertAndGetIdAsync(employee,conn,tran);

                var userInfo = new UserInfo()
                {
                    Email = employee.Email,
                    EmployeeId = employeeId,
                    Phone = employee.Phone,
                    UserName = employee.UserName,

                };
                var rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfPwdModeName } };
                var pwdConfig = await _serviceProxyProvider.Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
                if (pwdConfig == null)
                {
                    throw new BusinessException("获取用户加密模式失败,请先完成系统初始化");
                }
                var generatePwdMode = ConvertHelper.ParseEnum<GeneratePwdMode>(pwdConfig.ConfigValue);
                var plainPwd = string.Empty;
                if (generatePwdMode == GeneratePwdMode.Fixed)
                {
                    rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfFieldModeName } };
                    var fixedPwdConf = await _serviceProxyProvider.Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
                    if (pwdConfig == null)
                    {
                        throw new BusinessException("未配置员工用户默认密码");
                    }
                    plainPwd = fixedPwdConf.ConfigValue;
                }
                else
                {
                    plainPwd = PasswordGenerator.GetRandomPwd(IdentityConstants.RandomLen);
                    // :todo email send pwd
                }
                userInfo.Password = _passwordHelper.EncryptPassword(userInfo.UserName, plainPwd);
                await _userRepository.InsertAsync(userInfo, conn, tran);

            }, Connection);

        }

        public async Task DeleteEmployeeById(long id)
        {
            var userInfos = await _userRepository.GetAllAsync(p=>p.EmployeeId == id);
            await UnitOfWorkAsync(async (conn, trans) => {
                await _employeeRepository.DeleteAsync(p => p.Id == id);
                foreach (var userInfo in userInfos)
                {
                    await _userRepository.DeleteAsync(p => p.Id == userInfo.Id, conn, trans);
                    await _userRoleRepository.DeleteAsync(p => p.UserId == userInfo.Id, conn, trans);
                    await _userGroupRelationRepository.DeleteAsync(p => p.UserId == userInfo.Id, conn, trans);
                }
            },Connection);
        }
    }
}
