using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserRole : AuditedEntity<string>
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

    }
}
