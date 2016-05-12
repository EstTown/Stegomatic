using NUnit.Framework;
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

        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void EncryptDecrypt_String_ResultIsEqual(string text)
        {
            string cipherText = 
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void DecryptEncrypt_String_ResultIsEqual(string text)
        {

        }
    }
}
