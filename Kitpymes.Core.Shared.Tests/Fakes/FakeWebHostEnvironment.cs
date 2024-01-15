using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Moq;
using System;

namespace Kitpymes.Core.Shared.Tests.Fakes
{
    public class FakeWebHostEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = string.Empty;

#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IFileProvider? ContentRootFileProvider { get; set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).

        public string ContentRootPath { get; set; } = string.Empty;

        public string EnvironmentName { get; set; } = string.Empty;

        public string WebRootPath { get; set; } = string.Empty;

#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IFileProvider? WebRootFileProvider { get; set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).

        public static IWebHostEnvironment Configure(Action<FakeWebHostEnvironment> action)
        {
            var mock = new Mock<IWebHostEnvironment>();

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

            if (!string.IsNullOrWhiteSpace(options.WebRootPath))
            {
                mock.Setup(m => m.WebRootPath).Returns(options.WebRootPath);
            }

            if (options.WebRootFileProvider != null)
            {
                mock.Setup(m => m.WebRootFileProvider).Returns(options.WebRootFileProvider);
            }

            return mock.Object;
        }
    }
}
