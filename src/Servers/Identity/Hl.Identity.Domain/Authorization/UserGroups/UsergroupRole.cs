using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroupRole : AuditedEntity<long>
    {
        public long UserGroupId { get; set; }
        public long RoleId { get; set; }
    }
}
