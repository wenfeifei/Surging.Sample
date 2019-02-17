using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Roles
{
    public class Role : FullAuditedEntity<string>
    {
        public string RoleName { get; set; }

        public string Notes { get; set; }
    }
}
