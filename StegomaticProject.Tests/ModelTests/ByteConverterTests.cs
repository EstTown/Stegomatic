using NUnit.Framework;
using StegomaticProject.StegoSystemModel.Miscellaneous;

namespace StegomaticProject.Tests.ModelTests
{
    [TestFixture]
    public class ByteConverterTests
    {
        [TestCase("1234567890", ExpectedResult = "1234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøå", ExpectedResult = "abcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ", ExpectedResult = "ABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?", ExpectedResult = "!#¤%&/()=?")]
        [TestCase("1aA!", ExpectedResult = "1aA!")]
        public string EncodeDecode_String_AreEqual(string text)
        {
            byte[] byteArray = ByteConverter.StringToByteArray(text);
            return ByteConverter.ByteArrayToString(byteArray);
        }
    }
}
