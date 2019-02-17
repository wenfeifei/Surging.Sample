using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserInfo : FullAuditedEntity<string>
    {
        public UserInfo()
        {
            Roles = new List<UserRole>();
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string QQ { get; set; }

        public string Wechat { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
