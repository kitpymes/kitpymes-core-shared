using Kitpymes.Core.Shared.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests.Util
{
    [TestClass]
    public class HashTests
    {
        #region CreateRandom

        [TestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        public void CreateRandom_WithDefaultAllowableCharacters_Passing_ValidArguments_Returns_String_Random_Value(int length)
        {
            var hash = Hash.CreateRandom(length);

            Assert.IsTrue(hash.Length == length);
        }

        [TestMethod]
        [DataRow(8, "ae")]
        [DataRow(5, "aeiou123456")]
        [DataRow(10, "7890ABFLFLFFL")]
        [DataRow(20, "qazwscefbthmiklp09876543")]
        public void CreateRandom_WithCustomAllowableCharacters_Passing_Valid_Arguments_Returns_String_Random_Value(int length, string allowableCharacters)
        {
            var hash = Hash.CreateRandom(length, allowableCharacters);

            Assert.IsTrue(hash.Length == length);
        }

        #endregion CreateRandom

        #region SHA256

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CreateSHA256_Passing_Invalid_Arguments_Returns_SharedException(string text)
        {
            Assert.ThrowsExactly<SharedException>(() => Shared.Util.Hash.CreateSHA256(text));
        }

        [TestMethod]
        [DataRow("Password")]
        [DataRow(nameof(HashTests))]
        [DataRow("sdlfkjldsfksdfñl·%$·&$/U&/)(&()?=)¿?^P*^PÑL,.,.lñ´ñ+98098'098098")]
        public void CreateAndVerifySHA256_Passing_Valid_Arguments_Returns_True(string text)
        {
            var hash = Shared.Util.Hash.CreateSHA256(text);

            var isValid = Shared.Util.Hash.VerifySHA256(text, hash);

            Assert.IsTrue(isValid);
        }

        #endregion SHA256

        #region SHA512

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CreateSHA512_Passing_Invalid_Arguments_Returns_SharedException(string text)
        {
            Assert.ThrowsExactly<SharedException>(() => Hash.CreateSHA512(text));
        }

        [TestMethod]
        [DataRow("Password")]
        [DataRow(nameof(HashTests))]
        [DataRow("sdlfkjldsfksdfñl·%$·&$/U&/)(&()?=)¿?^P*^PÑL,.,.lñ´ñ+98098'098098")]
        public void CreateAndVerifySHA512_Passing_Valid_Arguments_Returns_True(string text)
        {
            var hash = Hash.CreateSHA512(text);

            var isValid = Hash.VerifySHA512(text, hash);

            Assert.IsTrue(isValid);
        }

        #endregion SHA512
    }
}
