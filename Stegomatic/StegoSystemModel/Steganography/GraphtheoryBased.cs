using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class GraphTheoryBased : IStegoAlgorithm
    {
        public GraphTheoryBased() //constructor
        {
        }

        //Create list for selected pixels from source image
        List<Pixel> PixelList = new List<Pixel>();

        public const int SamplesVertexRatio = 3;
        public const int Modulo = 4;
        public const int MaxEdgeWeight = 10;
        public const int PixelsPerByte = 12;    

        public byte[] Decode(Bitmap coverImage, string seed)
        {
            
            
            throw new NotImplementedException();
        }

        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            throw new NotImplementedException();
        }

        /*Method for calculating the weight of an edge*/
        public byte CalculateEdgeWeight(Vertex vertOne, Vertex vertTwo)
        {
            byte weight = 0;
            return weight;
        }

        private void GetRandomPixels(Bitmap image, int amount, int seed)
        {
            //Create array at the lenght of a 'amount of total pixels'
            int[] array = new int[Convert.ToInt32(image.Width * image.Height) * 2];

            //Create random-object, based on incoming seed
            Random r = new Random(seed);

            //Load to array
            for (int j = 0; j < array.Length; j++)
            {
                //This interval controls density of selection
                array[j] = r.Next(2, 10);
            }

            //Reset all variables before each run
            int posx = 0;
            int posy = 0;
            int pixels = 0;
            int i = 0;
            Color color;

            for (int j = 0; j < array.Length; j++)
            {
                posx += array[j];
                pixels++;

                if (posx * posy >= (image.Height * image.Width))
                {
                    break;
                }

                if (posx >= image.Width)
                {
                    int remainder = (image.Width - posx) * -1;
                    posx = 0 + remainder;
                    posy++;

                    if (posy >= image.Height)
                    {
                        posy = 0;
                    }
                }

                if (j == array.Length)
                {
                    j = 0;
                    if (pixels >= (image.Height * image.Width))
                    {
                        break;
                    }
                }

                //Gets selected pixel data; and creates new Pixel-object and stores in list
                Pixel pix = new Pixel(image.GetPixel(posx, posy), posx, posy);
                PixelList.Add(pix);
                if (PixelList.Count >= amount)
                {
                    break;
                }
                i++;
            }

            //Print log when done
            Console.WriteLine("Pixels imported successfully!");
            Console.WriteLine("Pixels: " + i + " were successfully extracted.");

        }



        /*Method for getting the value of bitpairs into a list of ints from a byte-array*/
        public List< IEnumerable<int>> ChopBytesToBitPairs(byte[] byteArray)
        {
            /*List fo int values*/
            List< IEnumerable<int>> messageValues = new List<IEnumerable <int>>();

            foreach (byte value in byteArray)
            {
                messageValues.Add(ConvertBitsToInt(value));
            }

            return messageValues;
        }

        /*Method for converting bitpairs to ints from a byte*/
        public IEnumerable<int> ConvertBitsToInt(byte byteValue)
        {
            int value;
            BitArray bitValues = new BitArray(new byte[] { byteValue });

            for (int index = 7; index > -1; index -= 2)   
            {
                if(bitValues[index] == true && bitValues[index - 1] == true)
                {
                    value = 3;
                }
                else if(bitValues[index] == true && bitValues[index - 1] == false)
                {
                    value = 2;
                }
                else if(bitValues[index] == false && bitValues[index - 1] == true)
                {
                    value = 1;
                }
                else
                {
                    value = 0;
                }

                yield return value;
            }
        }

        /*Method for calculating the required amount of pixels to hide the input message*/
        public int CalculateRequredPixels(byte[] byteArray)
        {
            int amount, counter = 0;

            foreach(byte value in byteArray)
            {
                counter++;
            }

            return amount = counter * PixelsPerByte;
        }
    }
}
