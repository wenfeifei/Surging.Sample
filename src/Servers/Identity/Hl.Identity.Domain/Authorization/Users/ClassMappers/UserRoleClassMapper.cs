using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    public class UserRoleClassMapper : HlClassMapper<UserRole>
    {
        public UserRoleClassMapper()
        {         
            AutoMap();
        }

    }
}
