using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class ServiceCollectionExtensionsTests
    {
        #region ToEnvironment

        [TestMethod]
        public void ToHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });

            var result = services.ToHostEnvironment();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });

            var result = services.ToWebHostEnvironment();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToEnvironment_With_TEnviromentType_IHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });

            var result = services.ToEnvironment<IHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });

            var result = services.ToEnvironment<IWebHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IHostEnvironment_Returns_IHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection().FakeAddHostEnvironment(x => { });

            var result = services.ToRequiredEnvironment<IHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_IWebHostEnvironment_NotNull()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => { });

            var result = services.ToRequiredEnvironment<IWebHostEnvironment>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IHostEnvironment_Returns_SharedException()
        {
            var fakeEnvironmentExpected = new FakeHostEnvironment { };
            var services = new ServiceCollection();

            Assert.ThrowsException<SharedException>(services.ToRequiredEnvironment<IHostEnvironment>);
        }

        [TestMethod]
        public void ToRequiredEnvironment_With_TEnviromentType_IWebHostEnvironment_Returns_SharedException()
        {
            var fakeEnvironmentExpected = new FakeWebHostEnvironment { };
            var services = new ServiceCollection();

            Assert.ThrowsException<SharedException>(services.ToRequiredEnvironment<IWebHostEnvironment>);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddHostEnvironment(x => x.EnvironmentName = Util.Environment.Development.ToString());

            var result = services.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());

            var result = services.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IWebHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Util.Environment.Development.ToString());

            var result = services.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsDevelopment_Passing_IWebHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());

            var result = services.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToIsEnviroment_Passing_IWebHostEnvironment_With_Valid_EnviromentName_Returns_True()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Util.Environment.Development.ToString());

            var result = services.ToIsDevelopment();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToIsEnviroment_Passing_IWebHostEnvironment_With_Invalid_EnviromentName_Returns_False()
        {
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = Guid.NewGuid().ToString());

            var result = services.ToIsDevelopment();

            Assert.IsFalse(result);
        }

        #endregion ToEnvironment

        #region ToExists

        [TestMethod]
        public void ToExists_Passing_NotFound_Service_Returns_False()
        {
            var services = new ServiceCollection();

            var result = services.ToExists<IWebHostEnvironment>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToExists_Passing_Valid_Service_Returns_True()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToExists<IWebHostEnvironment>();

            Assert.IsTrue(result);
        }

        #endregion ToExists

        #region ToService

        [TestMethod]
        public void ToService_Passing_NotFound_Service_Returns_Null()
        {
            var services = new ServiceCollection();

            var serviceActual = services.ToService<IWebHostEnvironment>();

            Assert.IsNull(serviceActual);
        }

        [TestMethod]
        public void ToService_Passing_Valid_Service_Returns_Service()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddWebHostEnvironment(x => x.EnvironmentName = environmentNameExpected);

            var serviceActual = services.ToService<IWebHostEnvironment>();

            Assert.IsNotNull(serviceActual);
            Assert.AreEqual(environmentNameExpected, serviceActual?.EnvironmentName);
            Assert.IsTrue(serviceActual?.IsDevelopment());
        }

        #endregion ToService

        #region ToServiceMatchingInterface

        [TestMethod]
        public void ToServiceMatchingInterface_Passing_Empties_Assemblies_Returns_SharedException()
        {
            var assemblies = Array.Empty<Assembly>();

            var services = new ServiceCollection();

            var exceptionActual = Assert.ThrowsException<SharedException>
            (
                () => services.ToServiceMatchingInterface(assemblies)
            );

            Assert.IsNotNull(exceptionActual);
        }

        [TestMethod]
        public void ToServiceMatchingInterface_Passing_Valid_Assembly_And_Valid_Interface_Returns_Service()
        {
            var assemblies = new Assembly[] { this.GetType().Assembly };

            var services = new ServiceCollection();

            services.ToServiceMatchingInterface<IFakeLoggerService>(assemblies);

            Assert.IsTrue(services.ToExists<IFakeLoggerService>());
        }

        [TestMethod]
        public void ToServiceMatchingInterface_Passing_Valid_Assembly_Returns_Service()
        {
            var assemblies = new Assembly[] { this.GetType().Assembly };

            var services = new ServiceCollection();

            services.ToServiceMatchingInterface(assemblies);

            Assert.IsTrue(services.ToExists<IFakeLoggerService>());
        }

        #endregion ToServiceMatchingInterface

        #region ToServiceMatchingClass

        [TestMethod]
        public void ToServiceMatchingClass_Passing_Empties_Assemblies_Returns_SharedException()
        {
            var assemblies = Array.Empty<Assembly>();

            var services = new ServiceCollection();

            var exceptionActual = Assert.ThrowsException<SharedException>
            (
                () => services.ToServiceMatchingClass(typeof(FakeLoggerService), assemblies)
            );

            Assert.IsNotNull(exceptionActual);
        }

        [TestMethod]
        public void ToServiceMatchingClass_Passing_Valid_Assembly_And_Valid_Class_Returns_Service()
        {
            var assemblies = new Assembly[] { this.GetType().Assembly };

            var services = new ServiceCollection();

            services.ToServiceMatchingClass<FakeLoggerService>(assemblies);

            Assert.IsTrue(services.ToExists<IFakeLoggerService>());
        }

        [TestMethod]
        public void ToServiceMatchingClass_Passing_Valid_Assembly_And_Valid_Class_Type_Returns_Service()
        {
            var assemblies = new Assembly[] { this.GetType().Assembly };

            var services = new ServiceCollection();

            services.ToServiceMatchingClass(typeof(FakeLoggerService), assemblies);

            Assert.IsTrue(services.ToExists<IFakeLoggerService>());
        }

        #endregion ToServiceMatchingInterface

        #region ToConfiguration

        [TestMethod]
        public void ToConfiguration_Passing_Empty_DirectoryPath_Returns_SharedException()
        {
            string? directoryPath = string.Empty;
            var jsonFileName = "FakeAppSettings";
            var expectedMessage = Messages.FieldDirectory(directoryPath, nameof(directoryPath));

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<SharedException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Invalid_DirectoryPath_Returns_SharedException()
        {
            string? directoryPath = Guid.NewGuid().ToString();
            var jsonFileName = "FakeAppSettings";
            var expectedMessage = Messages.FieldDirectory(directoryPath, nameof(directoryPath));

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<SharedException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Valid_DirectoryPath_Invalid_JsonFileName_Not_Optional_Returns_SharedException()
        {
            var directoryPath = Directory.GetCurrentDirectory();
            var jsonFileName = Guid.NewGuid().ToString();
            var expectedMessage = Messages.FieldFile($"{directoryPath}/{jsonFileName}.json", "filePath");

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<SharedException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Valid_DirectoryPath_Valid_JsonFileName_Returns_Ok()
        {
            // Hay que ir a las propiedades de archivo .json y setearle en "Copiar en el directorio de salida" => "Copiar si es posterior"
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var services = new ServiceCollection();
            var configurationActual = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>()
                .ToThrowIfNullOrEmpty();

            var isEnabledFacebookActual = configurationActual.GetValue<bool>("FakeSettings:FacebookSettings:Enabled");
            var keyFacebookActual = configurationActual.GetValue<string>("FakeSettings:FacebookSettings:Key");

            Assert.AreEqual(true, isEnabledFacebookActual);
            Assert.IsNotNull(keyFacebookActual);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Key_Values_Returns_Ok()
        {
            var expectedConfiguration = new (string key, string? value)[] {
                ("FakeSettings:FacebookSettings:Enabled", "true"),
                ("FakeSettings:FacebookSettings:Key", "d43fde43f43f")
            };

            var services = new ServiceCollection();
            var configurationActual = services
                .ToConfiguration(expectedConfiguration)
                .ToService<IConfiguration>()
                .ToThrowIfNullOrEmpty();

            var isEnabledFacebookActual = configurationActual.GetValue<bool>("FakeSettings:FacebookSettings:Enabled");
            var keyFacebookActual = configurationActual.GetValue<string>("FakeSettings:FacebookSettings:Key");
            
            Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[0].value), isEnabledFacebookActual);
            Assert.AreEqual(expectedConfiguration[1].value, keyFacebookActual);
        }

        #endregion ToConfiguration

        #region ToSettings

        [TestMethod]
        public void ToSettings_Passing_Values_From_AppSettingsFile_Returns_Settings()
        {
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFilename = "FakeAppSettings";

            var services = new ServiceCollection();
            var settingsActual = services
                .ToConfiguration(directoryPath, (jsonFilename, false, true))
                .ToSettings<FakeSettings>();

            if (settingsActual is not null)
            {
                Assert.IsTrue(settingsActual.FacebookSettings.Enabled);
                Assert.IsNotNull(settingsActual.FacebookSettings.Key);

                Assert.IsFalse(settingsActual.GoogleSettings.Enabled);
                Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.GoogleSettings.Key));

                Assert.IsFalse(settingsActual.LinkedinSettings.Enabled);
                Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.LinkedinSettings.Key));
            }
        }

        [TestMethod]
        public void ToSettings_Passing_Values_From_Memory_Returns_Settings()
        {
            var expectedConfiguration = new (string key, string? value)[] {
                ("FakeSettings:FacebookSettings:Enabled", "true"),
                ("FakeSettings:FacebookSettings:Key", "d43fde43f43f"),
                ("FakeSettings:GoogleSettings:Enabled", "false"),
                ("FakeSettings:GoogleSettings:Key", null)
            };

            var services = new ServiceCollection();
            var settingsActual = services
                .ToConfiguration(expectedConfiguration)
                .ToSettings<FakeSettings>();

            if (settingsActual is not null)
            {
                Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[0].value), settingsActual.FacebookSettings.Enabled);
                Assert.AreEqual(expectedConfiguration[1].value, settingsActual.FacebookSettings.Key);

                Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[2].value), settingsActual.GoogleSettings.Enabled);
                Assert.AreEqual(expectedConfiguration[3].value, settingsActual.GoogleSettings.Key);

                Assert.IsFalse(settingsActual.LinkedinSettings.Enabled);
                Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.LinkedinSettings.Key));
            }
        }

        #endregion ToSettings
    }
}
