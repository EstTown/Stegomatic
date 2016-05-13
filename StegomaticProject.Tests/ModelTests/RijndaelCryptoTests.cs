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
        string _standardPassword;

        [OneTimeSetUp]
        public void Initialize()
        {
            _cryptoTest = new RijndaelCrypto();
            _standardPassword = "pa$$word";
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void EncryptDecrypt_String_ResultIsEqual(string text)
        {
            string cipherText = _cryptoTest.Encrypt(text, _standardPassword);
            string decryptedText = _cryptoTest.Decrypt(cipherText, _standardPassword);
            Assert.AreEqual(text, decryptedText);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encrypt_String_ResultNotEqual(string text)
        {
            string cipherText = _cryptoTest.Encrypt(text, _standardPassword);
            Assert.AreNotEqual(text, cipherText);
        }

        [TestCase("1234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?")]
        [TestCase("1aA!")]
        public void Encrypt_Password_PasswordChangesResult(string password1)
        {
            string text = "abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå";
            string cipherText1 = _cryptoTest.Encrypt(text, password1);
            string cipherText2 = _cryptoTest.Encrypt(text, _standardPassword);
            Assert.AreNotEqual(cipherText1, cipherText2);
        }
    }
}
