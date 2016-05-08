using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemController
{
    public class VerifyUserInput : IVerifyUserInput
    {
        public string File(string path)
        {
            FileInfo pathToCheck = new FileInfo(path);
            if (!pathToCheck.Exists)
            {
                throw new NotifyUserException("Invalid path: " + pathToCheck.FullName);
            }
            else if (pathToCheck.IsReadOnly)
            {
                throw new NotifyUserException("ReadOnly path: " + pathToCheck.FullName);
            }
            return path;
        }

        public string Message(string message)
        {
            if (message == null)
            {
                return string.Empty;
            }
            return message;
        }

        public string EncryptionKey(string encryptionKey)
        {
            if (encryptionKey == null)
            {
                return string.Empty;
            }
            return encryptionKey;
        }

        public string StegoSeed(string stegoSeed)
        {
            if (stegoSeed == null)
            {
                return string.Empty;
            }
            return stegoSeed;
        }
    }
}
