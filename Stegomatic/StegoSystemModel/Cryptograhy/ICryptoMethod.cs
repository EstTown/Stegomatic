namespace StegomaticProject.StegoSystemModel.Cryptograhy
{
    public interface ICryptoMethod
    {
        string Encrypt(string plaintext, string password);
        string Decrypt(string ciphertext, string password);
    }
}
