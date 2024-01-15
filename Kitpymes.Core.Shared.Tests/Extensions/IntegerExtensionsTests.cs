using Kitpymes.Core.Shared.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class IntegerExtensionsTests
    {
        enum FakeEnumeration { a, b, c = 234 }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(-1)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        public void ToStringFormat_Passing_ValidValue_Returns_ValueWithoutRemovedChars(int input)
        {
            var valueActual = input.ToStringFormat();

            Assert.AreEqual(input.ToString(CultureInfo.CurrentCulture), valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_Enumeration_Key_Returns_Enumeration()
        {
            int? keyEnum = 234;

            var enumExpected = FakeEnumeration.c;

            FakeEnumeration enumActual = keyEnum.ToEnum<FakeEnumeration>();

            Assert.AreEqual(enumExpected, enumActual);
        }
    }
}
