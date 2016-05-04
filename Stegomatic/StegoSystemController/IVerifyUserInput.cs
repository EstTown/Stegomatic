using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemController
{
    public interface IVerifyUserInput
    {
        void File(string path);
        void Message(string message);
        void EncryptionKey(string encryptionKey);
        void StegoSeed(string StegoSeed);
    }
}
