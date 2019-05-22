﻿using Hl.Identity.IApplication.Menus.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.Menus
{
    [ServiceBundle("v1/api/permission/{service}")]
    public interface IMenuApplication : IServiceKey
    {
        Task<string> CreateMenu(CreateMenuInput input);
      
    }
}
