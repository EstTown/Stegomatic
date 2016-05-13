﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using StegomaticProject.StegoSystemModel.Cryptograhy;
using StegomaticProject.StegoSystemModel.Steganography;
using StegomaticProject.StegoSystemUI;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemModel
{
    public class StegoSystemModelClass : IStegoSystemModel
    {
        private ICompression _compressMethod;
        private ICryptoMethod _cryptoMethod;
        private IStegoAlgorithm _stegoMethod;

        public Func<int, int, int> CalculateImageCapacity { get; set; }

        public StegoSystemModelClass()
        {
            _compressMethod = new GZipStreamCompression();
            _cryptoMethod = new RijndaelCrypto();
            _stegoMethod = new GraphTheoryBased(); // GraphTheoryBased();
        }

        public string DecodeMessageFromImage(Bitmap coverImage, string decryptionKey, string stegoSeed, 
            bool decrypt = true, bool decompress = true)
        {
            byte[] byteMessage;

            try
            {
                byteMessage = _stegoMethod.Decode(coverImage, stegoSeed);
            }
            catch (NotifyUserException)
            {
                throw;
            }

            if (decrypt)
            {
                //byteMessage = _cryptoMethod.Decrypt(byteMessage, encryptionKey);
            }

            if (decompress)
            {
                byteMessage = _compressMethod.Decompress(byteMessage);
            }

            string message = Encoding.UTF8.GetString(byteMessage);
            return message;
        }

        public Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, string stegoSeed, 
            bool encrypt = true, bool compress = true)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);

            if (compress)
            {
                byteMessage = _compressMethod.Compress(byteMessage);
            }

            if (encrypt)
            {
                //byteMessage = _cryptoMethod.Encrypt(byteMessage, encryptionKey);
            }

            Bitmap stegoObject = _stegoMethod.Encode(coverImage, stegoSeed, byteMessage);
            return stegoObject;
        }

        public int CalcCapacityWithCompressionAndStego(int height, int width)
        {
            int capacityOnlyUsingStego = _stegoMethod.CalculateImageCapacity(height, width);
            return _compressMethod.ApproxSizeAfterCompression(capacityOnlyUsingStego);
        }
    }
}
