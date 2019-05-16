using System.Data.Common;
using System.Threading.Tasks;
using Hl.Identity.Domain.Authorization.UserGroups;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly IDapperRepository<UserInfo,long> _userRepository;
        private readonly IDapperRepository<UserRole, long> _userRoleRepository;
        private readonly IDapperRepository<UserGroupRelation, long> _userGroupRelationRepository;

        public UserManager(IDapperRepository<UserInfo, long> userRepository,
            IDapperRepository<UserRole, long> userRoleRepository,
            IDapperRepository<UserGroupRelation, long> userGroupRelationRepository)
        {
            _userRepository = userRepository;
            _userGroupRelationRepository = userGroupRelationRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task DeleteByUserId(long userId)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                await _userRepository.DeleteAsync(p => p.Id == userId, conn, trans);
                await _userRoleRepository.DeleteAsync(p => p.UserId == userId, conn, trans);
                await _userGroupRelationRepository.DeleteAsync(p => p.UserId == userId, conn, trans);
            }, Connection);
        }

    }
}
