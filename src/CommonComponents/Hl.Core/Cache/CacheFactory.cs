using Surging.Core.Caching;
using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Core.Cache
{
    public static class CacheFactory
    {
        private static CacheTargetType cacheType = CacheTargetType.Redis;
        private static string cacheSectionName = "ddlCache";
        static CacheFactory()
        {
            // :todo set value from setting
        }

        public static ICacheProvider CreateCacheProvider()
        {
            ICacheProvider _cacheProvider;
            switch (cacheType)
            {
                case CacheTargetType.Redis:
                    _cacheProvider = CacheContainer.GetService<ICacheProvider>($"{cacheSectionName}.{cacheType.ToString()}");
                    break;
                default:
                    throw new CPlatformException("暂时只支持redis缓存类型",null);
                    
            }
            return _cacheProvider;
        }

    }
}
