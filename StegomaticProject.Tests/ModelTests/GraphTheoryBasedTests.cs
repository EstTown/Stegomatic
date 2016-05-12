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

        [OneTimeSetUp]
        public void Initialize()
        {
            // _stegoTest = new GraphTheoryBased();
            _standardSeed = "123";

            int width = 50;
            int height = 50;
            _image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(_image);
            g.Clear(Color.Blue);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Message_ResultIsDifferent(string text)
        {

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

        public void Encode_Seed_SeedAffectsResult(string seed)
        {

        }

        public void Decode_InvalidSeed_DoesNotDecode(string seed)
        {

        }

        public void EncodeDecode_Message_NoLossOfData(string message)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(message);
            Bitmap coverImage = _stegoTest.Encode(_image, _standardSeed, byteMessage);
            _stegoTest.Decode(coverImage, _standardSeed);
            string decodedMessage = ByteConverter.ByteArrayToString(byteMessage);
        }
    }
}
