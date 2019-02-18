using Hl.Core.Manager;
using Hl.Infrastructure.Utilities;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Utilities;
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
            var exsitUserInfo = await _userStore.UserRepository.FirstOrDefaultAsync(p => p.UserName == user.UserName);
            if (exsitUserInfo != null)
            {
                throw new BusinessException($"系统中已经存在{user.UserName},请重新输入后用户名");
            }
            user.Password = ServiceLocator.GetService<IPasswordHelper>().EncryptPassword(user.UserName,user.Password);
            await _userStore.CreateAsync(user);
        }
    }
}
