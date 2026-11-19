using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection.Metadata;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class VerifyExtensionsTests
    {
        #region NullOrEmpty

        [TestMethod]
        public void IsNullOrEmpty_Passing_InvalidValue_Returns_True()
        {
            string? parameter = null;
            var valueExpected = true;

            var valueActual = parameter.ToIsNullOrEmpty();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Messages.NullOrEmpty(paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfNullOrEmpty(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrEmpty

        #region NullOrAny

        [TestMethod]
        public void IsNullOrAny_Passing_InvalidValue_Returns_True()
        {
            string[] parameter = [];
            var valueExpected = true;

            var valueActual = parameter.ToIsNullOrAny();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ThrowIfNullOrAny_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string[] parameter = [];
            var paramName = nameof(parameter);
            var valueExpected = Messages.NullOrAny(paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfNullOrAny(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrAny

        #region Range

        [DataTestMethod]
        [DataRow(-2)]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        public void ThrowIfOutRange_Passing_InvalidValue_Returns_SharedException(int value)
        {
            var min = 1;
            var max = 5;

            var paramName = nameof(value);
            var valueExpected = Messages.OutRange(min, max, paramName);

            var result = Assert.ThrowsException<SharedException>(() => value.ToThrowIfOutRange(min, max, paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void ThrowIfInsideRange_Passing_InvalidValue_Returns_SharedException(int value)
        {
            var min = 1;
            var max = 5;

            var paramName = nameof(value);
            var valueExpected = Messages.InsideRange(min, max, paramName);

            var result = Assert.ThrowsException<SharedException>(() => value.ToThrowIfInsideRange(min, max, paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void ThrowIfInsideRangeOrEqual_Passing_InvalidValue_Returns_SharedException(int value)
        {
            var min = 1;
            var max = 5;

            var paramName = nameof(value);
            var valueExpected = Messages.InsideOrEqualRange(min, max, paramName);

            var result = Assert.ThrowsException<SharedException>(() => value.ToThrowIfInsideOrEqualRange(min, max));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Range

        #region Directory

        [TestMethod]
        public void ThrowIfDirectoryNotExists_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = Guid.NewGuid().ToString();
            var valueExpected = Messages.Directory(parameter, paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfDirectoryNotExists(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Directory

        #region File

        [TestMethod]
        public void ThrowIfFileNotExists_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Messages.File(parameter, paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfFileNotExists(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion File

        #region FileExtension

        [TestMethod]
        public void ThrowIfNotFileExtension_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Messages.FileExtension(parameter, paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfNotFileExtension(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion FileExtension

        #region Email

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void ThrowIfNotEmail_Passing_InvalidValue_Returns_SharedExceptionWithMessage(string? value)
        {
            Assert.ThrowsException<SharedException>(() => value.ToThrowIfNotEmail(nameof(value)));
        }

        #endregion Email

        #region Subdomain

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void ThrowIfNotSubdomain_Passing_InvalidValue_Returns_SharedExceptionWithMessage(string? value)
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Messages.InvalidFormat(paramName);

            var result = Assert.ThrowsException<SharedException>(() => value.ToThrowIfNotSubdomain(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Subdomain

        #region Name

        [TestMethod]
        public void ToIsNameThrow_Passing_InvalidValue_Returns_SharedExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Messages.InvalidFormat(paramName);

            var result = Assert.ThrowsException<SharedException>(() => parameter.ToThrowIfNotName(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Name

        #region ContainsUniqueChars

        [TestMethod]
        public void IsContainsUniqueChars_Passing_ValidValue_Returns_True()
        {
            var value = "1aeiou2";
            var valueExpected = true;

            var result = value.ToIsContainsUniqueChars();

            Assert.AreEqual(valueExpected, result);
        }

        [TestMethod]
        public void IsContainsUniqueChars_Passing_InvalidValue_Returns_False()
        {
            var value = "1aeiou1";
            var valueExpected = false;

            var result = value.ToIsContainsUniqueChars();

            Assert.AreEqual(valueExpected, result);
        }

        #endregion ContainsUniqueChars
    
        [TestMethod]
        public void ToIsRegexMatch_Passing_Catastrophic_Pattern_Throws_Timeout_Instead_Of_Hanging()
        {
            // Patrón con retroceso exponencial: sin tiempo límite, este texto deja el proceso colgado.
            var text = new string('a', 40) + "!";

            Assert.ThrowsException<System.Text.RegularExpressions.RegexMatchTimeoutException>(() => text.ToIsRegexMatch("^(a|aa)+$"));
        }
    }
}
