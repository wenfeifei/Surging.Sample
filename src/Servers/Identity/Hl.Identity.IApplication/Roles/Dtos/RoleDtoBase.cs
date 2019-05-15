using Hl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Roles.Dtos
{
    public abstract class RoleDtoBase
    {
        [Required(ErrorMessage = "角色编码不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "角色编码格式不正确")]
        public string Code { get; set; }

        [Required(ErrorMessage = "角色名称不允许为空")]
        [MaxLength(50,ErrorMessage = "角色名称长度不允许超过50")]
        public string Name { get; set; }
        public string Memo { get; set; }
        public Status Status { get; set; }
    }
}
