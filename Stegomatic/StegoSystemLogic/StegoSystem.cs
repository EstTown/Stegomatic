using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StegomaticProject.StegoSystemLogic.Miscellaneous;
using StegomaticProject.StegoSystemLogic.Cryptograhy;
using StegomaticProject.StegoSystemLogic.Steganography;

namespace StegomaticProject.StegoSystemLogic
{
    public class StegoSystem : IStegoSystem
    {
        private GZipStream _compresser;
        private Confounder _confounder;
        private ICryptoMethod _cryptography;
        private IStegoAlgorithm _steganography;

        public StegoSystem()
        {
            _compresser = new GZipStream();
            _confounder = new Confounder();
            _cryptography = new RijndaelCrypto();
            _steganography = new GraphtheoryBased();
        }

        public string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey)
        {
            bool decrypt = true;
            if (encryptionKey == string.Empty || encryptionKey == null)
            {
                decrypt = false;
            }
            throw new NotImplementedException();
        }

        public Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, 
            bool encrypt = true, bool compress = true, bool confound = true)
        {
            byte[] byteMessage = ByteConverter.StringToByteArray(message);

            if (compress)
            {
                byteMessage = _compresser.Compress(byteMessage);
            }
            if (confound)
            {
                byteMessage = _confounder.AddConfounder(byteMessage);
            }
            if (encrypt)
            {
                byteMessage = _cryptography.Encrypt(byteMessage, encryptionKey);
            }

            Bitmap StegoObject = _steganography.Encode(coverImage, encryptionKey, byteMessage);
            // SKAL DET VÆRE ENCRYPTION KEY DER KOMMER IND HER??? HVAD SKAL DET VÆRE DET SEED OG HVOR FÅR VI DET FRA?


            throw new NotImplementedException();
        }
    }
}
