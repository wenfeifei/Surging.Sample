using Hl.Core.Manager;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserStore : ManagerBase, ITransientDependency
    {
        private readonly IDapperRepository<UserInfo, string> _userRepository;
        private readonly IDapperRepository<UserRole, string> _userRoleRepository;
        public UserStore(IDapperRepository<UserInfo, string> userRepository,
            IDapperRepository<UserRole, string> userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> CreateAsync(UserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(UserInfo));
            }
            UnitOfWork((conn,trans)=>{
                var userId = _userRepository.InsertAndGetIdAsync(user, conn, trans).Result;
                if (user.Roles.Any())
                {
                    foreach (var role in user.Roles)
                    {
                        role.UserId = userId;
                        _userRoleRepository.InsertAsync(role,conn, trans).Wait();
                    }
                }
            }, Connection);
            return true;
        }
    }
}
