using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kitpymes.Core.Shared.Tests.Extensions
{
    [TestClass]
    public class QueryableExtensionsTests
    {
        private readonly List<FakeUser> fakeListExpected = new List<FakeUser>
        {
            new FakeUser
            {
                Age = 55,
                Email = Guid.NewGuid().ToString(),
                Name = "a",
            },
            new FakeUser
            {
                Age = 18,
                Email = Guid.NewGuid().ToString(),
                Name = "h",
            },
            new FakeUser
            {
                Age = 40,
                Email = Guid.NewGuid().ToString(),
                Name = "b",
            },
        };

        #region ToWhere

        [TestMethod]
        public void ToWhere_Passing_Null_Predicate_Returns_Same_List()
        {
            Expression<Func<FakeUser, bool>>? predicate = null;

            var fakeListActual = fakeListExpected.AsQueryable().ToWhere(predicate).ToList();

            CollectionAssert.AreEquivalent(fakeListExpected, fakeListActual);
        }

        [TestMethod]
        public void ToWhere_Passing_Valid_Predicate_Returns_Filter_List()
        {
            Expression<Func<FakeUser, bool>>? predicate = x => x.Age > 20;

            var fakeListActual = fakeListExpected.AsQueryable().ToWhere(predicate).ToList();

            Assert.IsTrue(fakeListActual.Count == 2);
        }

        #endregion ToWhere

        #region ToPaged

        [TestMethod]
        public void ToPaged_Passing_Age_Ascending_Order_Returns_Filter_List()
        {
            var property = "Age";
            var ascending = true;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 18);
            Assert.IsTrue(fakeListActual.First().Name == "h");
        }

        [TestMethod]
        public void ToPaged_Passing_Age_Descending_Order_Returns_Filter_List()
        {
            var property = "Age";
            var ascending = false;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 55);
            Assert.IsTrue(fakeListActual.First().Name == "a");
        }

        [TestMethod]
        public void ToPaged_Passing_Name_Ascending_Order_Returns_Filter_List()
        {
            var property = "Name";
            var ascending = true;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 55);
            Assert.IsTrue(fakeListActual.First().Name == "a");
        }

        [TestMethod]
        public void ToPaged_Passing_Name_Descending_Order_Returns_Filter_List()
        {
            var property = "Name";
            var ascending = false;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 18);
            Assert.IsTrue(fakeListActual.First().Name == "h");
        }

        [TestMethod]
        public void ToPaged_Passing_Size_Returns_Only_That_Many_Items()
        {
            var fakeListActual = fakeListExpected.AsQueryable().ToPaged("Age", true, index: 1, size: 2).ToList();

            Assert.AreEqual(2, fakeListActual.Count);
            Assert.AreEqual(18, fakeListActual[0].Age);
            Assert.AreEqual(40, fakeListActual[1].Age);
        }

        [TestMethod]
        public void ToPaged_Passing_Second_Page_Returns_Next_Items()
        {
            var fakeListActual = fakeListExpected.AsQueryable().ToPaged("Age", true, index: 2, size: 2).ToList();

            Assert.AreEqual(1, fakeListActual.Count);
            Assert.AreEqual(55, fakeListActual[0].Age);
        }

        [TestMethod]
        public void ToPaged_Passing_Page_Beyond_The_End_Returns_Empty()
        {
            var fakeListActual = fakeListExpected.AsQueryable().ToPaged("Age", true, index: 3, size: 2).ToList();

            Assert.AreEqual(0, fakeListActual.Count);
        }

        [DataTestMethod]
        [DataRow(0, 20)]
        [DataRow(1, 0)]
        [DataRow(-1, 20)]
        public void ToPaged_Passing_Invalid_Index_Or_Size_Throws(int index, int size)
        {
            Assert.ThrowsException<Kitpymes.Core.Shared.Util.SharedException>(() => fakeListExpected.AsQueryable().ToPaged("Age", true, index, size).ToList());
        }

        #endregion ToPaged
    }
}
