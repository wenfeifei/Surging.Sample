using DapperExtensions.Mapper;
using Hl.Identity.Domain.Authorization.Users;

namespace Hl.Identity.Domain.Authorization.ClassMappers
{
    public class UserClassMapper : ClassMapper<UserInfo>
    {
        public UserClassMapper()
        {
            Map(p => p.Id).Key(KeyType.Assigned);
            Map(p => p.Roles).Ignore();
            AutoMap();
        }
    }
}
