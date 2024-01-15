using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Kitpymes.Core.Shared.Tests.Fakes
{
    public static class FakeServiceCollectionExtensions
    {
        public static IServiceCollection FakeAddHostEnvironment(this IServiceCollection services, Action<FakeHostEnvironment> action)
        {
            services.TryAddScoped(typeof(IHostEnvironment), X => FakeHostEnvironment.Configure(action));

            return services;
        }

        public static IServiceCollection FakeAddWebHostEnvironment(this IServiceCollection services, Action<FakeWebHostEnvironment> action)
        {
            services.TryAddScoped(typeof(IWebHostEnvironment), X => FakeWebHostEnvironment.Configure(action));

            return services;
        }
    }
}
