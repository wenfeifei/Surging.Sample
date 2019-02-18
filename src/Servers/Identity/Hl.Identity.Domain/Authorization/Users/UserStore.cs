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
        private readonly IDapperRepository<UserRole, string> _userRoleRepository;
        public UserStore(IDapperRepository<UserInfo, string> userRepository,
            IDapperRepository<UserRole, string> userRoleRepository)
        {
            UserRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public IDapperRepository<UserInfo, string> UserRepository { get; }

        public async Task<bool> CreateAsync(UserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(UserInfo));
            }
            UnitOfWork((conn,trans)=>{
                var userId = UserRepository.InsertAndGetIdAsync(user, conn, trans).Result;
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
