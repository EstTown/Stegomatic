using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace StegomaticProject.StegoSystemModel.Cryptograhy
{
    public class RijndaelCrypto : ICryptoMethod
    {
        /*Constructor for the class*/
        public RijndaelCrypto(string password)
        {
            /*Key and Salt for AES Encryption, Salt will always be the same unless changed, Key will depend on both Salt and password*/
            byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            /*New instance of the AES-class with definded Key and Initialization Vector (IV)*/
            using (RijndaelManaged myAes = new RijndaelManaged())
            {
                myAes.Key = key.GetBytes(32); //256-bit Key
                myAes.IV = key.GetBytes(16);  //128-bit IV
            }
        }

        /*Metod for decryption of the ciphertext*/
        public string Decrypt(string cipherText, byte[] Key, byte[] IV)
        {
            /*Exceptions*/
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;
            byte[] encrypted = Convert.FromBase64String(cipherText);

            /*New instance of the AES-class*/
            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                /*Create decrypter*/
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                /*Streams for decryption*/
                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            /*Reads the decrypted bytes from the stream to a string*/
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            /*Returns the decrypted string*/
            return plaintext;
        }

        /*Method for encryption of the plaintext*/
        public string Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            /*Exceptions*/
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;
            string cipherText = null;

            /*New instance of the AES-class*/
            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                /*Creates encrypter*/
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                /*Streams for encryption*/
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            /*Writes all data to the stream*/
                            swEncrypt.Write(plainText);
                        }
                        /*Byte-array to encrypted text*/
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            /*Returns ciphertext as string*/
            return cipherText = Convert.ToBase64String(encrypted);
        }
    }
}
