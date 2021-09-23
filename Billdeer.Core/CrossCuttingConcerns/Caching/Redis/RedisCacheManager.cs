using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.CrossCuttingConcerns.Caching.Redis
{/// <summary>
/// Caching for redis. (Not stable and not recommended).
/// </summary>
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T Get<T>(string key)
        {
            var result = default(T);
            _distributedCache.Get(key);
            return result;
        }

        public object Get(string key)
        {
            var result = (object)_distributedCache.GetString(key);
            return result;
        }

        public void Add(string key, object data, int duration)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();

            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //TODO: Fix json serilazing.
            var serilazedData = JsonConvert.SerializeObject(data, jss);
            Console.WriteLine(data);
            Console.WriteLine(serilazedData);
            _distributedCache.SetString(key, serilazedData, options.SetAbsoluteExpiration(TimeSpan.FromMinutes(duration)));
        }

        public void Add(string key, object data)
        {
            var serilazedData = JsonConvert.SerializeObject(data);
            _distributedCache.SetString(key, serilazedData);
        }

        public bool IsAdd(string key)
        {
            return _distributedCache.Get(key) == null ? false : true;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //RedisInvoker(x => x.RemoveByPattern(pattern));
        }

        //public void Clear()
        //{
        //    RedisInvoker(x => x.FlushAll());
        //}
    }
}
