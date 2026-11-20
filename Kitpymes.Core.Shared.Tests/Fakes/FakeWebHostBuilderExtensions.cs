using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Kitpymes.Core.Shared.Tests.Fakes
{
    public static class FakeWebHostBuilderExtensions
    {
        // TestServer(IWebHostBuilder) está obsoleto en .NET 10, pero estos tests prueban extensiones de IWebHostBuilder.
#pragma warning disable ASPDEPR008
        public static HttpClient FakeCreateClient(this IWebHostBuilder webHostBuilder)
        => new TestServer(webHostBuilder).CreateClient();
#pragma warning restore ASPDEPR008
    }
}
