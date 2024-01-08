using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class HttpContextExtensionsTests
    {
        #region IPv6

        [TestMethod]
        public void ToIPv6_Passing_HttpContext_IP_Value_Returns_IP()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
            });

            Assert.AreEqual(ipExpected.MapToIPv6().ToString(), httpContext.ToIPv6());
        }

        [TestMethod]
        public void ToTryIPv6_Passing_HttpContext_IP_Value_Returns_IP()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
            });

            httpContext.ToTryIPv6(out var ipActual);

            Assert.AreEqual(ipExpected.MapToIPv6().ToString(), ipActual);
        }

        #endregion IPv6

        #region ToDetails

        [TestMethod]
        public void ToDetails_Passing_HttpContext_Returns_Details_HttpContext()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);
            var headerKey = "User-Agent";
            var headerValueExpected = Guid.NewGuid().ToString();
            var contentTypeExpected = Guid.NewGuid().ToString();
            var schemeExpected = Guid.NewGuid().ToString();
            var hostExpected = Guid.NewGuid().ToString();
            var pathExpected = "/" + Guid.NewGuid().ToString();
            var authenticationType = Guid.NewGuid().ToString();
            var claimTypeExpected = Guid.NewGuid().ToString();
            var claimValueExpected = Guid.NewGuid().ToString();

            var headers = new Dictionary<string, IEnumerable<string>>();
            headers.ToAddOrUpdate(headerKey, headerValueExpected);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
                x.User?.ToAddIdentity(authenticationType, (claimTypeExpected, claimValueExpected));
                x.Request = new FakeHttpRequest
                {
                    ContentType = contentTypeExpected,
                    Scheme = schemeExpected,
                    Host = new HostString(hostExpected),
                    Path = pathExpected,
                }.AddHeader(headers);
            });

            var result = httpContext.ToDetails().ToSerialize();

            if (result != null)
            {
                Assert.IsTrue(result.Contains(ipExpected.MapToIPv6().ToString()));
                Assert.IsTrue(result.Contains(contentTypeExpected));
                Assert.IsTrue(result.Contains(schemeExpected));
                Assert.IsTrue(result.Contains(hostExpected));
                Assert.IsTrue(result.Contains(pathExpected));
                Assert.IsTrue(result.Contains(headerValueExpected));
            }
        }

        [TestMethod]
        public void ToDetails_Passing_HttpContext_WithOptionalData_Returns_Details_HttpContext()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);
            var headerKey = "User-Agent";
            var headerValueExpected = Guid.NewGuid().ToString();
            var contentTypeExpected = Guid.NewGuid().ToString();
            var schemeExpected = Guid.NewGuid().ToString();
            var hostExpected = Guid.NewGuid().ToString();
            var pathExpected = "/" + Guid.NewGuid().ToString();
            var authenticationType = Guid.NewGuid().ToString();
            var claimTypeExpected = Guid.NewGuid().ToString();
            var claimValueExpected = Guid.NewGuid().ToString();

            var optionalData = new Dictionary<string, IEnumerable<object>>();
            var keyOptionalData = Guid.NewGuid().ToString();
            var valueOptionalData = Guid.NewGuid().ToString();
            optionalData.ToAddOrUpdate(keyOptionalData, valueOptionalData);

            var headers = new Dictionary<string, IEnumerable<string>>();
            headers.ToAddOrUpdate(headerKey, headerValueExpected);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
                x.User?.ToAddIdentity(authenticationType, (claimTypeExpected, claimValueExpected));
                x.Request = new FakeHttpRequest
                {
                    ContentType = contentTypeExpected,
                    Scheme = schemeExpected,
                    Host = new HostString(hostExpected),
                    Path = pathExpected,
                }.AddHeader(headers);
            });

            var result = httpContext.ToDetails(optionalData).ToSerialize();

            if (result != null)
            {
                Assert.IsTrue(result.Contains(ipExpected.MapToIPv6().ToString()));
                Assert.IsTrue(result.Contains(contentTypeExpected));
                Assert.IsTrue(result.Contains(schemeExpected));
                Assert.IsTrue(result.Contains(hostExpected));
                Assert.IsTrue(result.Contains(pathExpected));
                Assert.IsTrue(result.Contains(headerValueExpected));
                Assert.IsTrue(result.Contains(keyOptionalData));
                Assert.IsTrue(result.Contains(valueOptionalData));
            }
        }

        #endregion ToDetails
    }
}
