using System.Drawing;

namespace StegomaticProject.StegoSystemController
{
    public interface IVerifyUserInput
    {
        string File(string path);
        string Message(string message);
        string EncryptionKey(string encryptionKey);
        string StegoSeed(string StegoSeed);
        void Image(Bitmap coverImage);
    }
}
