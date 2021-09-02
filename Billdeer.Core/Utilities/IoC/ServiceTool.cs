using Microsoft.Extensions.DependencyInjection;
using System;

namespace Billdeer.Core.Utilities.IoC
{   /// <summary>
    /// Zincirde bulunmayan yerlerde injectionları kontrol etmemize yarıyor.
    /// </summary>
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }

    }
}
