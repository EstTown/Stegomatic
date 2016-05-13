using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemModel.Steganography;
using StegomaticProject.StegoSystemModel.Miscellaneous;

namespace StegomaticProject.Tests.ModelTests
{
    [TestFixture]
    public class GraphTheoryBasedTests
    {
        IStegoAlgorithm _stegoTest;
        Bitmap _image;
        string _standardSeed;
        string _standardMessage;
        byte[] _standardByteMessage;
        Bitmap _standardStegoObject;

        [OneTimeSetUp]
        public void Initialize()
        {
            int width = 50;
            int height = 50;
            _image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(_image);
            g.Clear(Color.Blue);

            // _stegoTest = new GraphTheoryBased();
            _standardSeed = "123";
            _standardMessage = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";
            _standardByteMessage = ByteConverter.StringToByteArray(_standardMessage);
            _standardStegoObject = _stegoTest.Encode(_image, _standardSeed, _standardByteMessage);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Message_ResultIsDifferent(string text)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(text);
            Bitmap stegoObject = _stegoTest.Encode(_image, _standardSeed, byteMessage);
            Assert.AreNotEqual(stegoObject, _image);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Decode_PlainImage_ResultIsEmpty(string seed)
        {
            byte[] byteMessage = _stegoTest.Decode(_image, seed);
            string message = ByteConverter.ByteArrayToString(byteMessage);
            Assert.IsEmpty(message);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Seed_SeedAffectsResult(string seed)
        {
            Bitmap stegoObject = _stegoTest.Encode(_image, seed, _standardByteMessage);
            Assert.AreNotEqual(stegoObject, _standardStegoObject);
        }

        public void Decode_InvalidSeed_DoesNotDecode()
        {
            string seed = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";
            byte[] decodedMessage = _stegoTest.Decode(_standardStegoObject, seed);
            Assert.AreNotEqual(decodedMessage, _standardByteMessage);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void EncodeDecode_Message_NoLossOfData(string message)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(message);
            Bitmap coverImage = _stegoTest.Encode(_image, _standardSeed, byteMessage);
            _stegoTest.Decode(coverImage, _standardSeed);
            string decodedMessage = ByteConverter.ByteArrayToString(byteMessage);
        }
    }
}
