using NUnit.Framework;
using System.Drawing;
using StegomaticProject.StegoSystemModel.Steganography;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using StegomaticProject.CustomExceptions;

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
            
            _stegoTest = new GraphTheoryBased();
            _standardSeed = "123";
            _standardMessage = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";
            _standardByteMessage = ByteConverter.StringToByteArray(_standardMessage);
            _standardStegoObject = new Bitmap(_stegoTest.Encode(_image, _standardSeed, _standardByteMessage));
        }

        private int ImagePixelDifference(Bitmap original, Bitmap modified)
        {
            int pixelDifferences = 0;
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    if (!original.GetPixel(x, y).Equals(modified.GetPixel(x, y)))
                    {
                        pixelDifferences++;
                    }
                }
            }
            return pixelDifferences;
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("")]
        public void Encode_Message_ResultIsDifferent(string text)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(text);
            Bitmap stegoObject = _stegoTest.Encode(new Bitmap(_image), _standardSeed, byteMessage);
            Assert.Greater(ImagePixelDifference(stegoObject, _image), 0);
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Decode_PlainImage_ResultIsEmpty(string seed)
        {
            try
            {
                byte[] byteMessage = _stegoTest.Decode(_image, seed);
                string message = ByteConverter.ByteArrayToString(byteMessage);
            }
            catch (NotifyUserException)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Seed_SeedAffectsResult(string seed)
        {
            Bitmap stegoObject = new Bitmap(_stegoTest.Encode(_image, seed, _standardByteMessage));
            Assert.Greater(ImagePixelDifference(stegoObject, _standardStegoObject), 0);
        }

        [Test]
        public void Decode_InvalidSeed_DoesNotDecode()
        {
            string seed = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";

            try
            {
                _stegoTest.Decode(_standardStegoObject, seed);
            }
            catch (NotifyUserException)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void EncodeDecode_Message_NoLossOfData(string message)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(message);
            Bitmap coverImage = new Bitmap(_stegoTest.Encode(_image, _standardSeed, byteMessage));
            _stegoTest.Decode(coverImage, _standardSeed);
            string decodedMessage = ByteConverter.ByteArrayToString(byteMessage);
            Assert.AreEqual(message, decodedMessage);
        }
    }
}
