using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemModel.Miscellaneous;
using StegomaticProject.StegoSystemModel.Cryptograhy;
using StegomaticProject.StegoSystemModel.Steganography;

namespace StegomaticProject.StegoSystemModel
{
    public class StegoSystemModel : IStegoSystemModel
    {
        private ICompression _compressMethod;
        private ICryptoMethod _cryptoMethod;
        private IStegoAlgorithm _stegoMethod;



        public StegoSystemModel()
        {
            _compressMethod = new GZipStreamCompression();
            _cryptoMethod = new RijndaelCrypto();
            _stegoMethod = new GraphTheoryBased();
        }

        public string DecodeMessageFromImage(Bitmap coverImage, string decryptionKey,
            bool decrypt = true, bool decompress = true)
        {
            //if (decryptionKey == string.Empty || decryptionKey == null)
            //{
            //    decrypt = false;
            //}
            // PROP DET OVENSTÅENDE IND I ENCRYPTION KLASSEN??!

            byte[] byteMessage = _stegoMethod.Decode(coverImage, decryptionKey);

            if (decrypt)
            {
                byteMessage = _cryptoMethod.Decrypt(decryptionKey, byteMessage);
            }

            if (decompress)
            {
                byteMessage = _compressMethod.Decompress(byteMessage);
            }

            string message = ByteConverter.ByteArrayToString(byteMessage);

            return message;
        }

        public Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, 
            bool encrypt = true, bool compress = true)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(message);

            if (compress)
            {
                byteMessage = _compressMethod.Compress(byteMessage);
            }

            if (encrypt)
            {
                byteMessage = _cryptoMethod.Encrypt(byteMessage, encryptionKey);
            }

            Bitmap StegoObject = _stegoMethod.Encode(coverImage, encryptionKey, byteMessage);
            // SKAL DET VÆRE ENCRYPTION KEY DER KOMMER IND HER??? HVAD SKAL DET VÆRE DET SEED OG HVOR FÅR VI DET FRA?

            

            return StegoObject;
        }
    }
}
