using NUnit.Framework;
using StegomaticProject.StegoSystemModel;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.Tests.ModelTests
{
    [TestFixture]
    public class StegoSystemModelClassTests
    {
        private IStegoSystemModel _stegoModel;
        private Bitmap _image;
        private string _key;
        private string _seed;
        private string _message;

        [OneTimeSetUp]
        public void Initialize()
        {
            _stegoModel = new StegoSystemModelClass();
            int width = 50;
            int height = 50;
            _image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(_image);
            g.Clear(Color.Blue);

            _key = "123";
            _seed = "123";
            _message = "1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!1aA!";
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        [TestCase(false, true)]
        public void EncodeDecode_Configuration_NoLossOfData(bool encrypt, bool compress)
        {
            Bitmap stegoObject;
            string decodedMessage;
            stegoObject = _stegoModel.EncodeMessageInImage(_image, _message, _key, _seed, encrypt, compress);
            decodedMessage = _stegoModel.DecodeMessageFromImage(stegoObject, _key, _seed, encrypt, compress);
            Assert.AreEqual(decodedMessage, _message);
        }

        [Test]
        public void CalculateImageCapacity_Int_Completes()
        {
            int width = 50, height = 50;
            bool compress = true;
            _stegoModel.CalculateImageCapacity(width, height, compress);
            Assert.Pass();
        }
    }
}
