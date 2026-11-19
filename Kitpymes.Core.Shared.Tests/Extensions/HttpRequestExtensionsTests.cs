using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Tests.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class HttpRequestExtensionsTests
    {
        [TestMethod]
        public void ToHeader_Passing_HttpRequest_Header_Many_Values_Returns_Values()
        {
            var keyExpected = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var headers = new Dictionary<string, IEnumerable<string>>()
                .ToAddOrUpdate(keyExpected, value)
                .ToAddOrUpdate(keyExpected, valueExpected);

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(headers));

            var headerActual = fakeHttpRequest.ToHeader(keyExpected);

            Assert.IsTrue(headerActual?.Contains(valueExpected));
        }

        [TestMethod]
        public void ToTryGetHeader_Passing_HttpRequest_Header_Many_Values_Returns_Values()
        {
            var keyExpected = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var headers = new Dictionary<string, IEnumerable<string>>()
                .ToAddOrUpdate(keyExpected, value)
                .ToAddOrUpdate(keyExpected, valueExpected);

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(headers));

            if (fakeHttpRequest.ToTryHeader(keyExpected, out var headerActual))
            {
                Assert.IsTrue(headerActual?.Contains(valueExpected));
            }
        }

        [TestMethod]
        public void ToPath_Passing_HttpRequest_Scheme_Host_Path_Returns_Url()
        {
            var scheme = Guid.NewGuid().ToString();
            var host = Guid.NewGuid().ToString();
            var path = "/" + Guid.NewGuid().ToString();
            var urlExpected = $"{scheme}://{host}{path}";

            var fakeHttpRequest = FakeHttpRequest.Configure(x =>
            {
                x.Scheme = scheme;
                x.Host = new HostString(host);
                x.Path = path;
            });

            var urlActual = fakeHttpRequest.ToPath();

            Assert.AreEqual(urlExpected, urlActual);
        }

        [TestMethod]
        public void ToSubdomain_Passing_HttpRequest_Host_Returns_Subdomain()
        {
            var host = new HostString(Guid.NewGuid().ToString());
            var subdomainExpected = host.Host?.Split('.')[0].Trim();

            var fakeHttpRequest = FakeHttpRequest.Configure(x =>
            {
                x.Host = host;
            });

            var subdomainActual = fakeHttpRequest.ToSubdomain();

            Assert.AreEqual(subdomainExpected, subdomainActual);
        }

        [TestMethod]
        public void ToTryContentType_Passing_HttpRequestWithContentType_Value_Returns_ContentType_String_Value()
        {
            var valueExpected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.ContentType = valueExpected);

            fakeHttpRequest.ToTryContentType(out var valueActual);

            Assert.AreEqual(valueExpected, valueActual);
        }
    
        [TestMethod]
        public void ToSubdomain_Passing_Uppercase_Host_In_Turkish_Culture_Returns_Ascii_Lowercase()
        {
            // En turco, "I".ToLower() da "ı" (sin punto): el subdominio dejaría de ser ASCII.
            var cultureOriginal = System.Globalization.CultureInfo.CurrentCulture;
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");

            try
            {
                var fakeHttpRequest = FakeHttpRequest.Configure(x =>
                {
                    x.Host = new HostString("IMAGES.example.com");
                });

                Assert.AreEqual("images", fakeHttpRequest.ToSubdomain());
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = cultureOriginal;
            }
        }
    }
}
