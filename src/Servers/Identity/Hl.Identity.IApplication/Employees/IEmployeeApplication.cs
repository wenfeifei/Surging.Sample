
using Hl.Core.Commons.Dtos;
using Hl.Core.Maintenance;
using Hl.Identity.IApplication.Employees.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.Employees
{
    [ServiceBundle("v1/api/employee/{service}")]
    public interface IEmployeeApplication : IServiceKey
    {
        /// <summary>
        /// 创建员工信息接口
        /// </summary>
        /// <param name="input">员工信息</param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-4-30", Name = "创建员工接口")]
        Task<string> Create(CreateEmployeeInput input);

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-5-16", Name = "修改员工")]
        Task<string> Update(UpdateEmployeeInput input);

        [Service(Director = Maintainer.Liuhll, Date = "2019-5-16", Name = "删除员工")]
        Task<string> Delete(DeleteByIdInput input);
    }
}
