using Kitpymes.Core.Shared.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Moq;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeHostEnvironment : IHostEnvironment
    {
        public string ApplicationName { get; set; } = string.Empty;
#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IFileProvider? ContentRootFileProvider { get; set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public string ContentRootPath { get; set; } = string.Empty;
        public string EnvironmentName { get; set; } = string.Empty;

        public static IHostEnvironment Configure(Action<FakeHostEnvironment> action)
        {
            var mock = new Mock<IHostEnvironment>();

            var options = action.ToConfigureOrDefault();

            if (!string.IsNullOrWhiteSpace(options.ApplicationName))
            {
                mock.Setup(m => m.ApplicationName).Returns(options.ApplicationName);
            }

            if (options.ContentRootFileProvider != null)
            {
                mock.Setup(m => m.ContentRootFileProvider).Returns(options.ContentRootFileProvider);
            }

            if (!string.IsNullOrWhiteSpace(options.ContentRootPath))
            {
                mock.Setup(m => m.ContentRootPath).Returns(options.ContentRootPath);
            }

            if (!string.IsNullOrWhiteSpace(options.EnvironmentName))
            {
                mock.Setup(m => m.EnvironmentName).Returns(options.EnvironmentName);
            } 

            return mock.Object;
        }
    }
}
