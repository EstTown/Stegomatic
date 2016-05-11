using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Cryptograhy
{
    public interface ICryptoMethod
    {
        byte[] Encrypt(byte[] plaintext, string password);
        byte[] Decrypt(byte[] ciphertext, string password);
    }
}
