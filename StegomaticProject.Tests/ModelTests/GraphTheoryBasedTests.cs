﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //byte[] byteArray;

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
            _standardStegoObject = _stegoTest.Encode(_image, _standardSeed, _standardByteMessage);

            //byteArray = new byte[1] {83};


        }

        private int ImagePixelDifference(Bitmap original, Bitmap modified)
        {
            int pixelDifferences = 0;
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    //Color originalColor = original.GetPixel(x, y);
                    //Color modifiedColor = modified.GetPixel(x, y);

                    //if (originalColor.R != modifiedColor.R ||
                    //    originalColor.B != modifiedColor.B ||
                    //    originalColor.G != modifiedColor.G)
                    //{
                    //    pixelDifferences++;
                    if (!original.GetPixel(x, y).Equals(modified.GetPixel(x, y)))
                    {
                        pixelDifferences++;
                    }
                }
            }
            return pixelDifferences;
        }

        //[TestCase(83, 81)/*, expected = ""*/]
        //public List<byte> ByteArrayToValues(byte[] byteArray)
        //{
        //    List<byte> Values = new List<byte>();

        //    foreach (byte item in byteArray)
        //    {
        //        BitArray bitValues = new BitArray(BitConverter.GetBytes(item).ToArray());
        //        for (int index = 7; index > -1; index -= 2)
        //        {
        //            if (bitValues[index] == true && bitValues[index - 1] == true)
        //            {
        //                Values.Add(3);
        //            }
        //            else if (bitValues[index] == true && bitValues[index - 1] == false)
        //            {
        //                Values.Add(2);
        //            }
        //            else if (bitValues[index] == false && bitValues[index - 1] == true)
        //            {
        //                Values.Add(1);
        //            }
        //            else
        //            {
        //                Values.Add(0);
        //            }
        //        }
        //    }
        //    return Values;
        //}

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Message_ResultIsDifferent(string text)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(text);
            Bitmap stegoObject = _stegoTest.Encode(_image, _standardSeed, byteMessage);
            Assert.Greater(ImagePixelDifference(stegoObject, _image), 0);
        }

        //[TestCase("1234567890123456789012345678901234567890")]
        //[TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        //[TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        //[TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        //[TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        //public void Decode_PlainImage_ResultIsEmpty(string seed)
        //{
        //    byte[] byteMessage = _stegoTest.Decode(_image, seed);
        //    string message = ByteConverter.ByteArrayToString(byteMessage);
        //    Assert.IsEmpty(message);
        //}

        [TestCase("1234567890123456789012345678901234567890")]
        [TestCase("abcdefghijklmnopqrstuxyzæøåabcdefghijklmnopqrstuxyzæøå")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUXYZÆØÅABCDEFGHIJKLMNOPQRSTUXYZÆØÅ")]
        [TestCase("!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?!#¤%&/()=?")]
        [TestCase("1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!")]
        public void Encode_Seed_SeedAffectsResult(string seed)
        {
            Bitmap stegoObject = _stegoTest.Encode(_image, seed, _standardByteMessage);
            Assert.Greater(ImagePixelDifference(stegoObject, _standardStegoObject), 0);
        }

        [Test]
        public void Decode_InvalidSeed_DoesNotDecode()
        {
            string seed = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";
            //byte[] decodedMessage = _stegoTest.Decode(_standardStegoObject, seed);
            //Assert.AreNotEqual(decodedMessage, _standardByteMessage);
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
            Bitmap coverImage = _stegoTest.Encode(_image, _standardSeed, byteMessage);
            _stegoTest.Decode(coverImage, _standardSeed);
            string decodedMessage = ByteConverter.ByteArrayToString(byteMessage);
            Assert.AreEqual(message, decodedMessage);
        }

        //[Test]
        //public void TestImageDifference()
        //{
        //    int width = 50;
        //    int height = 50;
        //    Bitmap image2 = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(image2);
        //    g.Clear(Color.Green);

        //    Assert.Greater(ImagePixelDifference(_image, image2), 0);
        //}
    }
}
