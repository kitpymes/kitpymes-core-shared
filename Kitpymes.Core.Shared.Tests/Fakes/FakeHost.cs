using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.TestHost;
using Kitpymes.Core.Shared.Extensions;

namespace Kitpymes.Core.Shared.Tests.Fakes
{
    public class FakeHost
    {
        public string EnvironmentName { get; set; } = "Testing";
        public string JsonFile { get; set; } = "appsettings.json";
        public string DirectoryPath { get; set; } = Directory.GetCurrentDirectory();
        public string? SolutionRelativeContentRoot { get; set; }

        public static IWebHostBuilder Create<TStartup>(Action<FakeHost>? action = null)
            where TStartup : StartupBase
        {
            var options = action.ToConfigureOrDefault();

            // WebHost está obsoleto en .NET 10, pero estos tests prueban extensiones de IWebHostBuilder.
#pragma warning disable ASPDEPR008
            var host = WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>()
                .UseEnvironment(options.EnvironmentName)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var configPath = Path.Combine(options.DirectoryPath, options.JsonFile);
#pragma warning restore ASPDEPR008

                    config.AddJsonFile(configPath, true);
                });

            if (!options.SolutionRelativeContentRoot.ToIsNullOrEmpty())
            {
                host.UseSolutionRelativeContentRoot(options.SolutionRelativeContentRoot);
            }

            return host;
        }
    }
}
