using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Cryptograhy
{
    public interface ICryptoMethod
    {
        string Encrypt(string plaintext, byte[] Key, byte[] IV);
        string Decrypt(string ciphertext, byte[] Key, byte[] IV);
    }
}
