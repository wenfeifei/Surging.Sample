using DapperExtensions.Mapper;
using Hl.Identity.Domain.Authorization.Users;

namespace Hl.Identity.Domain.Authorization.ClassMappers
{
    public class UserRoleClassMapper : ClassMapper<UserRole>
    {
        public UserRoleClassMapper()
        {
            Map(p => p.Id).Key(KeyType.Assigned);
            AutoMap();
        }

    }
}
