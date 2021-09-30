using Billdeer.Core.CrossCuttingConcerns.Caching;
using Billdeer.Core.CrossCuttingConcerns.Caching.Microsoft;
using Billdeer.Core.CrossCuttingConcerns.Caching.Redis;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Billdeer.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddMemoryCache();
            serviceCollection.AddStackExchangeRedisCache(options =>
           {
               options.Configuration = "localhost:6379";
           });
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>();
            serviceCollection.AddTransient<FileLogger>();
            serviceCollection.AddTransient<PostgreSqlLogger>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
