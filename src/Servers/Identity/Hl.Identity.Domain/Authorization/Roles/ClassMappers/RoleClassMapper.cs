using DapperExtensions.Mapper;
using Hl.Identity.Domain.Authorization.Roles;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    public class RoleClassMapper : ClassMapper<Role>
    {
        public RoleClassMapper()
        {
            AutoMap();
        }
    }
}
