using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemLogic.Cryptograhy
{
    public class RijndaelCrypto : ICryptoMethod
    {
        public byte[] Decrypt(string decryptionKey, byte[] ciphertext)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(byte[] message, string encryptionKey)
        {
            throw new NotImplementedException();
        }
    }
}
