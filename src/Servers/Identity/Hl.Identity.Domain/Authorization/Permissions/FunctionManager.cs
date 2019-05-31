using Hl.Identity.Domain.Authorization.Menus;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class FunctionManager : ManagerBase, IFunctionManager
    {
        private readonly IDapperRepository<Function, long> _functionRepository;
        private readonly IDapperRepository<PermissionFunction, long> _permissionFunctionRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;

        public FunctionManager(IDapperRepository<Function, long> functionRepository,
            IDapperRepository<PermissionFunction, long> permissionFunctionRepository,
            IDapperRepository<Permission, long> permissionRepository)
        {
            _functionRepository = functionRepository;
            _permissionFunctionRepository = permissionFunctionRepository;
            _permissionRepository = permissionRepository;
        }

        //public async Task CreateFunction(Function function, Permission permission, long menuId)
        //{
        //    await UnitOfWorkAsync(async (conn, trans) => {
        //        var funcId = await _functionRepository.InsertAndGetIdAsync(function,conn,trans);
        //        var permissionId = await _permissionRepository.InsertAndGetIdAsync(permission,conn,trans);
        //        await _menuFunctionRepository.InsertAsync(new MenuFunction() {
        //            FunctionId = funcId,
        //            MenuId = menuId,                   
        //        },conn,trans);
        //        await _permissionFunctionRepository.InsertAsync(new PermissionFunction() {
        //            FunctionId = funcId,
        //            PermissionId = permissionId
        //        }, conn, trans);
        //    }, Connection);
        //}

        public async Task CreateOperation(Permission operation, IEnumerable<long> functionIds)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(operation, conn, trans);
                foreach (var funcId in functionIds)
                {
                    var permissionFunc = new PermissionFunction() {
                        FunctionId = funcId,
                        PermissionId = permissionId,
                    };
                    await _permissionFunctionRepository.InsertAsync(permissionFunc, conn, trans);
                }
            }, Connection);
        }
    }
}
