using NUnit.Framework;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using System.Text;

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

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Compress_String_ResultSizeLessOrEqual(string text)
        {
            byte[] byteText = ConvertToByteArray(text);
            byte[] compressedByteText = _gZipStreamTest.Compress(byteText);

            Assert.Less(compressedByteText.Length, byteText.Length);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Decompress_String_ResultSizeGreaterOrEqual(string text)
        {
            byte[] byteText = ConvertToByteArray(text);
            byte[] compressedByteText = _gZipStreamTest.Compress(byteText);

            byte[] decompressedByteText = _gZipStreamTest.Decompress(compressedByteText);

            Assert.Less(compressedByteText.Length, decompressedByteText.Length);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Decompress_DecompressedString_NothingHappens(string text)
        {
            byte[] byteText = ConvertToByteArray(text);

            byte[] decompressedByteText = _gZipStreamTest.Decompress(byteText);

            Assert.AreEqual(byteText.Length, decompressedByteText.Length);
        }

        private byte[] ConvertToByteArray(string text)
        {
            return Encoding.Unicode.GetBytes(text);
        }
    }
}
