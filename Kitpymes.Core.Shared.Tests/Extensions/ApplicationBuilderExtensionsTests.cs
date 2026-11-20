using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Tests.Fakes;
using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class ApplicationBuilderExtensionsTests
    {
        #region ToEnvironment

        [TestMethod]
        public void ToHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToHostEnvironment();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToWebHostEnvironment();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToEnvironment_With_TEnviromentType_IHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment<IHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment<IWebHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToRequiredEnvironment<IHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToRequiredEnvironment<IWebHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IHostEnvironment_Returns_SharedException()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            Assert.ThrowsExactly<SharedException>(application.ToRequiredEnvironment<IHostEnvironment>);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_SharedException()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            Assert.ThrowsExactly<SharedException>(application.ToRequiredEnvironment<IWebHostEnvironment>);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddHostEnvironment(x => x.EnvironmentName = Shared.Util.AppEnvironment.Development.ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IWebHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Shared.Util.AppEnvironment.Development.ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IWebHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToIsEnviroment_Passing_IWebHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Shared.Util.AppEnvironment.Development.ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsEnviroment_Passing_IWebHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        #endregion ToEnvironment

        #region ToExists

        [TestMethod]
        public void ToExists_Passing_Service_Not_Found_Returns_False()
        {
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToExists<IWebHostEnvironment>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToExists_Passing_Valid_Service_Returns_True()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToExists<IWebHostEnvironment>();

            Assert.IsTrue(result);
        }

        #endregion ToExists

        #region ToService

        [TestMethod]
        public void ToService_Passing_Valid_Service_Returns_Service()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var serviceActual = application.ToService<IWebHostEnvironment>();

            Assert.IsNotNull(serviceActual);
            Assert.AreEqual(environmentNameExpected, serviceActual?.EnvironmentName);
            Assert.IsTrue(serviceActual?.IsDevelopment());
        }


        [TestMethod]
        public void ToService_Passing_Service_Not_Found_Returns_Null()
        {
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var serviceActual = application.ToService<IWebHostEnvironment>();

            Assert.IsNull(serviceActual);
        }

        #endregion ToService

        #region ToRequiredService

        [TestMethod]
        public void ToRequiredService_Passing_Valid_Service_Returns_Service()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var serviceActual = application.ToRequiredService<IWebHostEnvironment>();

            Assert.IsNotNull(serviceActual);
        }

        [TestMethod]
        public void ToRequiredService_Passing_Parameter_Application_Null_Returns_SharedException()
        {
            var services = new ServiceCollection();
            ApplicationBuilder? application = null;

#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var exceptionActual = Assert.ThrowsExactly<SharedException>(() => application.ToRequiredService<IWebHostEnvironment>());
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            Assert.IsNotNull(exceptionActual);
        }

        [TestMethod]
        public void ToRequiredService_Passing_Service_Not_Found_Returns_SharedException()
        {
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var exceptionActual = Assert.ThrowsExactly<SharedException>(() => application.ToRequiredService<IWebHostEnvironment>());

            Assert.IsNotNull(exceptionActual);
        }

        #endregion ToRequiredService
    }
}
