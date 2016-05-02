using NUnit.Framework;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StegomaticProject.Tests.ModelTests
{
    [TestFixture]
    public class GZipStreamCompressionTests
    {
        private GZipStreamCompression _gZipStreamTest;

        [OneTimeSetUp]
        public void Initialize()
        {
            _gZipStreamTest = new GZipStreamCompression();
        }

        [TestCase("1234567890", ExpectedResult = "1234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøå", ExpectedResult = "abcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ", ExpectedResult = "ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?", ExpectedResult = "!#¤%&/()=?")]
        [TestCase("1aA!", ExpectedResult = "1aA!")]
        public string CompressAndDecompress_CompareInputOutput_NoLossOfData(string text)
        {
            byte[] byteText = ConvertToByteArray(text);

            byteText = _gZipStreamTest.Compress(byteText);
            byteText = _gZipStreamTest.Decompress(byteText);

            return text;
        }

        [TestCase("1234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?")]
        [TestCase("1aA!")]
        public void Compress_Base64String_ResultSizeLess(string text)
        {
            byte[] byteText = ConvertToByteArray(text);
            byte[] compressedByteText = _gZipStreamTest.Compress(byteText);
            Assert.Less(compressedByteText.Length, byteText.Length);
        }

        [TestCase("1234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?")]
        [TestCase("1aA!")]
        public void Decompress_Base64String_ResultSizeGreater(string text)
        {
            byte[] byteText = ConvertToByteArray(text);
            byte[] compressedByteText = _gZipStreamTest.Compress(byteText);

            byte[] decompressedByteText = _gZipStreamTest.Decompress(byteText);

            Assert.Greater(compressedByteText.Length, decompressedByteText.Length);
        }

        [Test]
        public void Decompress_DecompressedString_NothingHappens(string text)
        {
            byte[] byteText = ConvertToByteArray(text);

            byte[] decompressedByteText = _gZipStreamTest.Decompress(byteText);
            Assert.AreEqual(byteText.Length, decompressedByteText.Length);
        }

        private byte[] ConvertToByteArray(string text)
        {
            return Convert.FromBase64String(text);
        }
    }
}
