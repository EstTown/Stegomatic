using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using StegomaticProject.StegoSystemModel.Cryptograhy;
using StegomaticProject.StegoSystemModel.Steganography;
using StegomaticProject.StegoSystemUI;
using System.Threading;

namespace StegomaticProject.StegoSystemModel
{
    public class StegoSystemModelClass : IStegoSystemModel
    {
        private ICompression _compressMethod;
        private ICryptoMethod _cryptoMethod;
        private IStegoAlgorithm _stegoMethod;


        //public static void EncodeWasCalled(Object sender, EventArgs e)
        //{
        //    Console.WriteLine("Encoded was clicked!");
        //}

        //public static void DecodeWasCalled(Object sender, EventArgs e)
        //{
        //    Console.WriteLine("Decoded was clicked!");
        //}

        public StegoSystemModelClass()
        {
            _compressMethod = new GZipStreamCompression();
            _cryptoMethod = new RijndaelCrypto();
            _stegoMethod = new GraphTheoryBased(); // GraphTheoryBased();

            
        }

        public string DecodeMessageFromImage(Bitmap coverImage, string decryptionKey, string stegoSeed, 
            bool decrypt = true, bool decompress = true)
        {
            //if (decryptionKey == string.Empty || decryptionKey == null)
            //{
            //    decrypt = false;
            //}
            // PROP DET OVENSTÅENDE IND I ENCRYPTION KLASSEN??!

            byte[] byteMessage = _stegoMethod.Decode(coverImage, stegoSeed);

            if (decrypt)
            {
               // byteMessage = _cryptoMethod.Decrypt(decryptionKey, byteMessage);
            }

            if (decompress)
            {
                byteMessage = _compressMethod.Decompress(byteMessage);
            }

            string message = ByteConverter.ByteArrayToString(byteMessage);

            return message;
        }

        public Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, string stegoSeed, 
            bool encrypt = true, bool compress = true)
        {

            byte[] byteMessage = ByteConverter.StringToByteArray(message);

            if (compress)
            {
                byteMessage = _compressMethod.Compress(byteMessage);
            }

            if (encrypt)
            {
                //byteMessage = _cryptoMethod.Encrypt(byteMessage, encryptionKey);
            }

            Bitmap StegoObject = _stegoMethod.Encode(coverImage, stegoSeed, byteMessage);

            return StegoObject;
        }
    }
}
