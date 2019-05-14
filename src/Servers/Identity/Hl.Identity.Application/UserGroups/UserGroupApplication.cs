using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.UserGroups;
using Hl.Identity.IApplication.UserGroups;
using Hl.Identity.IApplication.UserGroups.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Application.UserGroups
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class UserGroupApplication : ProxyServiceBase, IUserGroupApplication
    {
        private readonly IDapperRepository<UserGroup, long> _userGroupRepository;
        public UserGroupApplication(IDapperRepository<UserGroup, long> userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }
        public async Task<string> Create(CreateUserGroupInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            if (input.ParentId != 0)
            {
                var parentUserGroup = await _userGroupRepository.SingleOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentUserGroup == null)
                {
                    throw new BusinessException($"不存在父Id为{input.ParentId}的用户组");
                }
            }
            var existUserGroup = await _userGroupRepository.FirstOrDefaultAsync(p => p.GroupName == input.GroupName);
            if (existUserGroup != null)
            {
                throw new BusinessException($"已经存在{input.GroupName}的用户组");
            }
            var userGroupEntity = input.MapTo<UserGroup>();
            await _userGroupRepository.InsertAsync(userGroupEntity);
            return "新增用户组成功";
        }
    }
}
