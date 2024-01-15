using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Tests.Fakes;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class HttpResponseExtensionsTests
    {
        [TestMethod]
        [DataRow("/test")]
        public async Task ToResultAsync_Passing_StatusCodeAnd_MessageAndContentTypeAndHeader_Values_Returns_Values(string url)
        {
            // Arrange
            var keyExpected = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var headers = new Dictionary<string, IEnumerable<string>>()
                .ToAddOrUpdate(keyExpected, value)
                .ToAddOrUpdate(keyExpected, valueExpected);

            var statusCodeExpected = HttpStatusCode.Unauthorized;
            string contentTypeExpected = Guid.NewGuid().ToString();
            string messageExpected = Guid.NewGuid().ToString();

            void HandleMapTest(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    await context.Response.ToResultAsync
                    (
                        statusCodeExpected,
                        messageExpected,
                        contentTypeExpected,
                        headers
                    );
                });
            }

            var projectDirectoryName = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName
                .Split("\\")
                .Last();

            var client = FakeHost
                .Create<FakeStartup>(host => host.SolutionRelativeContentRoot = projectDirectoryName)
                .Configure(app => app.Map(url, HandleMapTest))
                .FakeCreateClient();

            // Act
            var httpResponse = await client.GetAsync(url);
            var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(statusCodeExpected, httpResponse.StatusCode);
            StringAssert.Contains(messageExpected, httpResponseBody);
            StringAssert.Contains(contentTypeExpected, httpResponse.Content.Headers.GetValues("content-type").First());
            StringAssert.Contains(valueExpected, httpResponse.Headers.GetValues(keyExpected).First(x => x == valueExpected));
        }
    }
}
