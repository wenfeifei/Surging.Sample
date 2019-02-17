using Hl.Core.Manager;
using Surging.Core.CPlatform.Ioc;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserManager : ITransientDependency
    {
        private readonly UserStore _userStore;
        public UserManager(UserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task CreateAsync(UserInfo user)
        {
            await _userStore.CreateAsync(user);
        }
    }
}
