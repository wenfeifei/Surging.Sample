using Hl.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public class CreateUserGroupInput
    {
        [Required(ErrorMessage = "父级用户组Id不允许为空,默认值为0")]
        public long ParentId { get; set; }

        [Required(ErrorMessage = "用户组名称不允许为空")]
        [MaxLength(50,ErrorMessage = "用户组名称最长不允许超过50")]
        public string GroupName { get; set; }
        public Status Status { get; set; }
    }
}
