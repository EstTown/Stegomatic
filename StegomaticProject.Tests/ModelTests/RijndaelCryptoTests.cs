using NUnit.Framework;
using StegomaticProject.StegoSystemModel.Cryptograhy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.Tests.ModelTests
{
    [TestFixture]
    public class RijndaelCryptoTests
    {
        private ICryptoMethod _cryptoTest;

        [OneTimeSetUp]
        public void CryptoSetUp()
        {
            _cryptoTest = new RijndaelCrypto();
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void EncryptDecrypt_String_ResultIsEqual(string text)
        {
            string password = "pa$$word";
            string cipherText = _cryptoTest.Encrypt(text, password);
            string decryptedText = _cryptoTest.Decrypt(cipherText, password);
            Assert.AreEqual(text, decryptedText);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void DecryptEncrypt_String_ResultIsEqual(string text)
        {
            string password = "pa$$word";
            string decryptedText = _cryptoTest.Decrypt(text, password);
            string cipherText = _cryptoTest.Encrypt(decryptedText, password);
            Assert.AreEqual(text, cipherText);
        }
    }
}
