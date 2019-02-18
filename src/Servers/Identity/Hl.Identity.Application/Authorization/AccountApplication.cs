using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.IApplication.Authorization;
using Hl.Identity.IApplication.Authorization.Dtos;
using Hl.Identity.IApplication.Authorization.Validators;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hl.Identity.Application.Authorization
{
    [ModuleName("v1identity",Version = "v1")]
    public class AccountApplication : ProxyServiceBase, IAccountApplication
    {
        private readonly UserManager _userManager;


        public AccountApplication(UserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<PayloadOutput> Login(LoginInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegisterInput input)
        {
            var validator = GetService<RegisterValidator>();
            var validatorResult = await validator.ValidateAsync(input);
            validatorResult.IsValidResult();
            var userEntity = input.MapTo<UserInfo>();
            await _userManager.CreateAsync(userEntity);
            return "注册用户成功";
        }
    }
}
