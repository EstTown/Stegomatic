using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StegomaticProject.CustomExceptions;
using System.Drawing;

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
            if (string.IsNullOrEmpty(encryptionKey))
            {
                //return "123"; //string.Empty;
                throw new NotifyUserException("A password is required!", "Error");
            }
            return encryptionKey;
        }

        public string StegoSeed(string stegoSeed)
        {
            if (string.IsNullOrEmpty(stegoSeed))
            {
                //return "123"; //string.Empty;
                throw new NotifyUserException("A password is required!", "Error");
            }
            return stegoSeed;
        }

        public void Image(Bitmap coverImage)
        {
            if (coverImage == null)
            {
                throw new NotifyUserException("No image choosen.");
            }
        }
    }
}
