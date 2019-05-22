using Hl.Identity.Domain.Authorization.Permissions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public class MenuManager : ManagerBase, IMenuManager
    {
        private readonly IDapperRepository<Menu, long> _menuRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;

        public MenuManager(IDapperRepository<Menu, long> menuRepository,
            IDapperRepository<Permission, long> permissionRepository)
        {
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task CreateMenu(Menu menu, Permission permission)
        {
            await UnitOfWorkAsync(async (conn, trans) => {              
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(permission, conn, trans);
                menu.PermissionId = permissionId;
                await _menuRepository.InsertAsync(menu, conn, trans);
            },Connection);
        }

        public async Task UpdateMenu(Menu menu, Permission permission)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                await _permissionRepository.UpdateAsync(permission, conn, trans);
                await _menuRepository.UpdateAsync(menu, conn, trans);
            }, Connection);
        }
    }
}
