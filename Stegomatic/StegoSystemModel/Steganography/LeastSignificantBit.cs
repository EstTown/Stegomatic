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
            // Seed is unused.
            string binaryMessage = MessageToBinary(message);

            Console.WriteLine("Size: " + binaryMessage.Length);

            Bitmap stegoObject = HideMessage(coverImage, binaryMessage, Convert.ToInt32(seed));
            return stegoObject;
        }

        private Bitmap HideMessage(Bitmap carrier, string binaryMessage, int red)
        {
            Color colorOfPixel, newColor;
            int x = 0, y = 0;

            for (int i = 0; i < binaryMessage.Length; i++)
            {
                if (++x >= carrier.Width)
                {
                    x = 0;
                    y++;
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
            return string.Concat(message.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        private byte[] BinaryToByteArray(string binaryMessage)
        {
            int numOfBytes = binaryMessage.Length / 8;
            byte[] byteMessage = new byte[numOfBytes];
            for(int i = 0; i < numOfBytes; ++i)
            {
                byteMessage[i] = Convert.ToByte(binaryMessage.Substring(8 * i, 8), 2);
            }
            return byteMessage;
        }

        public byte[] Decode(Bitmap carrier, string seed)
        {
            string binaryMessage = GetMessage(carrier, Convert.ToInt32(seed));
            return BinaryToByteArray(binaryMessage);
        }

        private string GetMessage(Bitmap carrier, int red)
        {
            Color colorOfPixel;
            StringBuilder message = new StringBuilder();

            for (int x = 0; x < carrier.Width; x++)
            {
                for (int y = 0; y < carrier.Height; y++)
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
