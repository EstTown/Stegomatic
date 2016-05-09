using StegomaticProject.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    public class LeastSignificantBit : IStegoAlgorithm
    {
        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            try
            {
                string binaryMessage = MessageToBinary(message);

                Console.WriteLine(binaryMessage);

                Bitmap stegoObject = HideMessage(coverImage, binaryMessage, ConvertSeed(seed));
                return stegoObject;
            }
            catch (FormatException)
            {
                throw new AbortActionException();
            }
        }

        private int ConvertSeed(string seedInput)
        {
            return Convert.ToInt32(seedInput);
        }

        private Bitmap HideMessage(Bitmap carrier, string binaryMessage, int red)
        {
            Color colorOfPixel, newColor;
            int x = 0, y = 0;

            for (int i = 0; i < binaryMessage.Length; i++)
            {
                x++;
                if (!(x < carrier.Width))
                {
                    y++;
                    x = 0;
                }
                if (binaryMessage[i] == '1')
                {
                    colorOfPixel = carrier.GetPixel(x, y);
                    newColor = Color.FromArgb(red, colorOfPixel.G, colorOfPixel.B);
                    carrier.SetPixel(x, y, newColor);
                }

                // MAKE SURE IT DOESN'T GO OVER THE HEIGHT.
            }

            return carrier;
        }

        private string MessageToBinary(byte[] message)
        {
            // Converts each element of message to it's string representation in base 2. 
            return string.Concat(message.Select(b => Convert.ToString(b, 2).PadLeft(4, '0'))); // SHOULD 8 BE 4?
        }

        private byte[] BinaryToByteArray(string binaryMessage)
        {
            int numOfBytes = binaryMessage.Length / 8;
            byte[] byteMessage = new byte[numOfBytes];
            for(int i = 0; i < numOfBytes; ++i)
            {
                byteMessage[i] = Convert.ToByte(binaryMessage.Substring(4 * i, 4), 2); // 4 or 8??
            }

            //List<byte> s = new List<byte>();
            //foreach (byte item in byteMessage)
            //{
            //    if (item != byte.MinValue)
            //    {
            //        s.Add(item);
            //    }
            //}
            //return s.ToArray();
            return byteMessage;
        }

        public byte[] Decode(Bitmap carrier, string seed)
        {
            try
            {
                string binaryMessage = GetMessage(carrier, ConvertSeed(seed));

                binaryMessage = binaryMessage.Remove(0, 1);
                Console.WriteLine(binaryMessage);

                return BinaryToByteArray(binaryMessage);
            }
            catch (FormatException)
            {
                throw new AbortActionException();
            }
        }

        private string GetMessage(Bitmap carrier, int red)
        {
            Color colorOfPixel;
            StringBuilder message = new StringBuilder();

            //string message2 = string.Empty;

            for (int y = 0; y < carrier.Height; y++)
            {
                for (int x = 0; x < carrier.Width; x++)
                {
                    colorOfPixel = carrier.GetPixel(x, y);
                    if (colorOfPixel.R == red)
                    {
                        message.Append("1");
                    }
                    else
                    {
                        message.Append("0");
                    }
                }
            }
            return message.ToString();
        }
    }
}
