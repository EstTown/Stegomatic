using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemController
{
    public interface IVerifyUserInput
    {
        string File(string path);
        string Message(string message);
        string EncryptionKey(string encryptionKey);
        string StegoSeed(string StegoSeed);
    }
}
