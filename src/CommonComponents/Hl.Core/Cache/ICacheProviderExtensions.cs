using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hl.Core.Cache
{
    public static class ICacheProviderProviderExtensions
    {
        private static ISerializer<string> _serializer = ServiceLocator.GetService<ISerializer<string>>();

        public static async Task<T> GetAsyn<T>(this ICacheProvider cacheProvider, string key, Func<Task<T>> factory, long? storeTime = null) where T : class
        {
            T returnValue;
            try
            {
                var resultJson = cacheProvider.Get<string>(key);
                if (string.IsNullOrEmpty(resultJson) || resultJson == "\"[]\"")
                {
                  
                    returnValue = await factory();
                    cacheProvider.Update(key, _serializer.Serialize(returnValue), storeTime);
                }
                else
                {
                    returnValue = _serializer.Deserialize(resultJson,typeof(T)) as T;
                }
                return returnValue;
            }
            catch
            {
                returnValue = await factory();
                return returnValue;
            }
        }

        public static void Update(this ICacheProvider cacheProvider, string key, string jsonValue, long? storeTime = null)
        {
            if (storeTime.HasValue)
            {
                cacheProvider.Remove(key);
                cacheProvider.Add(key, jsonValue, storeTime.Value);
            }
            else
            {
                cacheProvider.Remove(key);
                cacheProvider.Add(key, jsonValue);
            }
        }

        public static void Update<T>(this ICacheProvider cacheProvider, string key, T value, long? storeTime = null)
        {
            var jsonValue = _serializer.Serialize(value);
            cacheProvider.Update(key, jsonValue, storeTime);
        }

        public static void RemoveWithMatch(this ICacheProvider cacheProvider, string key)
        {
            var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
            cacheProvider.RemoveWithPrefix(matchKey);
        }

        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, string key)
        {
            await Task.Run(()=> {
                var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
                cacheProvider.RemoveWithPrefix(matchKey);
            });
        }

        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, string key, Func<Task> action)
        {
            await action();
            var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
            cacheProvider.RemoveWithPrefix(matchKey);
        }

        public static void RemoveWithMatch(this ICacheProvider cacheProvider, params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
                cacheProvider.RemoveWithPrefix(matchKey);
            }
           
        }
        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
                await Task.Run(() =>
                {
                    cacheProvider.RemoveWithPrefix(matchKey);
                });
            }

        }
        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider,string[] keys, Func<Task> action)
        {
            await action();
            await cacheProvider.RemoveWithMatchAsync(keys);

        }
    }
}

