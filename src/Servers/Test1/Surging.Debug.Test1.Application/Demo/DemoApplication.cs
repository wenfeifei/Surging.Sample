using Surging.Core.ProxyGenerator;
using Surging.Debug.Test1.IApplication.Demo;
using Surging.Debug.Test1.IApplication.Demo.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surging.Debug.Test1.Application.Demo
{
    public class DemoApplication : ProxyServiceBase, IDemoApplication
    {
        public async Task<string> GetUserName(QueryUserInput input)
        {
            var id = await GetService<IServiceProxyProvider>().Invoke<string>(new Dictionary<string, object>() {
                { "id", Guid.NewGuid().ToString()},

            }, "v1/api/debug/demo/getuserid");
            return input.UserId + Guid.NewGuid() + id;
        }

        public async Task<string> GetUserId(string id)
        {
            return id;
        }
    }
}
