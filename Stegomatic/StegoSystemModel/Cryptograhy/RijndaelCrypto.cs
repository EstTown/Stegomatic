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
        /*Metod for decryption of the ciphertext*/
        public byte[] Decrypt(byte[] cipherText, string password)
        {
            byte[] plaintext;

            /*New instance of the AES-class*/
            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

                aesAlg.Key = key.GetBytes(32); //256-bit Key
                aesAlg.IV = key.GetBytes(16); //128-bit IV

                /*Create decrypter*/
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                /*Streams for decryption*/
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        //using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        //{
                        //    /*Reads the decrypted bytes from the stream to a string*/
                        //    plaintext = srDecrypt.ReadToEnd();
                        //}

                        using (MemoryStream resultStream = new MemoryStream())
                        {
                            csDecrypt.CopyTo(resultStream);
                            plaintext = resultStream.ToArray();
                        }
                    }
                }
            }

            /*Returns the decrypted string*/
            return plaintext;
        }

        /*Method for encryption of the plaintext*/
        public byte[] Encrypt(byte[] plainText, string password)
        {
            byte[] cipherText;

            /*New instance of the AES-class*/
            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                byte[] salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

                aesAlg.Key = key.GetBytes(32); //256-bit Key
                aesAlg.IV = key.GetBytes(16); //128-bit IV

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
                        cipherText = msEncrypt.ToArray();
                    }
                }
            }

            /*Returns ciphertext as string*/
            return cipherText; // = Convert.ToBase64String(encrypted);
        }
    }
}
