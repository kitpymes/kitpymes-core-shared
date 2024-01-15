using Kitpymes.Core.Shared.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private static Dictionary<string, IEnumerable<TValue>> GetList<TValue>() => new();

        [TestMethod]
        public void AddOrUpdate_Add_Values_Returns_Collection_WithValues()
        {
            var keyExpected = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var list = GetList<string>().ToAddOrUpdate(keyExpected, valueExpected);

            Assert.IsTrue(list.ContainsKey(keyExpected));
            Assert.IsTrue(list.ToContainsKeyValue(keyExpected, valueExpected));
        }

        [TestMethod]
        public void AddOrUpdate_Update_Values_Returns_Collection_WithUpdateValue()
        {
            var keyExpected = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var list = GetList<string>().ToAddOrUpdate(keyExpected, value);

            var valueUpdate1 = Guid.NewGuid().ToString();
            list.ToAddOrUpdate(keyExpected, valueUpdate1);

            var valueUpdate2 = Guid.NewGuid().ToString();
            list.ToAddOrUpdate(keyExpected, valueUpdate2);

            var valueExpected = Guid.NewGuid().ToString();
            list.ToAddOrUpdate(keyExpected, valueExpected);

            Assert.IsTrue(list.ContainsKey(keyExpected));
            Assert.IsTrue(list.ToContainsKeyValue(keyExpected, valueExpected));
            Assert.IsTrue(list.ToValues(keyExpected)?.Count() == 1);
        }
    }
}
