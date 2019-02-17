using DapperExtensions.Mapper;
using Hl.Identity.Domain.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.ClassMappers
{
    public class RoleClassMapper : ClassMapper<Role>
    {
        public RoleClassMapper()
        {
            Map(p => p.Id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}
