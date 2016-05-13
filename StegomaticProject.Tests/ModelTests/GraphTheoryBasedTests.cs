using NUnit.Framework;
using System;
using System.Collections;
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
        byte[] byteArray;

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

            byte[] byteArray = new byte[1] {83};


        }

        [TestCase(83, 81)]

        public List<byte> ByteArrayToValues(byte[] byteArray)
        {
            List<byte> Values = new List<byte>();

            foreach (byte item in byteArray)
            {
                BitArray bitValues = new BitArray(BitConverter.GetBytes(item).ToArray());
                for (int index = 7; index > -1; index -= 2)
                {
                    if (bitValues[index] == true && bitValues[index - 1] == true)
                    {
                        Values.Add(3);
                    }
                    else if (bitValues[index] == true && bitValues[index - 1] == false)
                    {
                        Values.Add(2);
                    }
                    else if (bitValues[index] == false && bitValues[index - 1] == true)
                    {
                        Values.Add(1);
                    }
                    else
                    {
                        Values.Add(0);
                    }
                }
            }
            return Values;
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
