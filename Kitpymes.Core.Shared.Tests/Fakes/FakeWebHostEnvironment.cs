using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Moq;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeWebHostEnvironment 
    {
        public string? WebRootPath { get; set; }
        IFileProvider? WebRootFileProvider { get; set; }
        public string? ApplicationName { get; set; }
        IFileProvider? ContentRootFileProvider { get; set; }
        public string? ContentRootPath { get; set; }
        public string? EnvironmentName { get; set; }

        public static IWebHostEnvironment Configure(Action<FakeWebHostEnvironment> action)
        {
            var mock = new Mock<IWebHostEnvironment>();

            var options = action.ToConfigureOrDefault();

            if (!string.IsNullOrWhiteSpace(options.WebRootPath))
            {
                mock.Setup(m => m.WebRootPath).Returns(options.WebRootPath);
            }

            if (options.WebRootFileProvider != null)
            {
                mock.Setup(m => m.WebRootFileProvider).Returns(options.WebRootFileProvider);
            }

            if (!string.IsNullOrWhiteSpace(options.ApplicationName))
            {
                mock.Setup(m => m.ApplicationName).Returns(options.ApplicationName);
            }

            if (options.ContentRootFileProvider != null)
            {
                mock.Setup(m => m.ContentRootFileProvider).Returns(options.ContentRootFileProvider);
            }

            if (!string.IsNullOrWhiteSpace(options.EnvironmentName))
            {
                mock.Setup(m => m.EnvironmentName).Returns(options.EnvironmentName);
            }            

            if (!string.IsNullOrWhiteSpace(options.ContentRootPath))
            {
                mock.Setup(m => m.ContentRootPath).Returns(options.ContentRootPath);
            }

            return mock.Object;
        }
    }
}
