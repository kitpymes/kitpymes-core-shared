using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kitpymes.Core.Shared.Tests
{
    public static class FakeServiceCollectionExtensions
    {
        public static IServiceCollection FakeAddWebHostEnvironment(this IServiceCollection services, Action<FakeWebHostEnvironment> action)
        {
            services.TryAddScoped(typeof(IWebHostEnvironment), X => FakeWebHostEnvironment.Configure(action));

            return services;
        }       
    }
}
