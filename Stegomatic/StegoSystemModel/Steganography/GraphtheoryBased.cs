using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class GraphTheoryBased : IStegoAlgorithm
    {
        public GraphTheoryBased() //constructor
        {

        }
        public List<Pixel> PixelList = new List<Pixel>();
        //Create list for values of bitpairs in message
        public List<IEnumerable<byte>> BitPairValueList = new List<IEnumerable<byte>>();

        public const int SamplesVertexRatio = 3, Modulo = 4, MaxEdgeWeight = 10, PixelsPerByte = 12;

        public byte[] Decode(Bitmap coverImage, string seed)
        {
            
            throw new NotImplementedException();
        }

        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            
            //call bunch of methods that prepare for graph construction


            //at some point we need to calculate a graph, therefore make new graph
            //Graph graph = new Graph(PixelList, message);




            //maybe some modify stuff here
            throw new NotImplementedException();
        }

        /*Method for calculating the weight of an edge*/
        public byte CalculateEdgeWeight(Pixel vertPixOne, Pixel vertPixTwo) 
        {
            byte weight = 0;
            return weight;
        }

        private int ShortenAndParsePassphraseToInt32(string passphrase) //converts user stego passphrase into an int32 seed
        {
            int seed;
            string temp = "";

            while (true)
            {
                bool b = Int32.TryParse(passphrase, out seed);
                Console.WriteLine(b);
                if (b == true)
                {
                    break;
                }
                else
                {
                    for (int i = 0; i < passphrase.Length; i += 2)
                    {
                        temp += passphrase[i];
                    }
                    passphrase = temp;
                    temp = "";
                }
            }
            return seed;
        }
        private void GetRandomPixelsAddToList2(Bitmap image, int pixelsNeeded, string passphrase)
        {
            int key = ShortenAndParsePassphraseToInt32(passphrase);
            int numberOfPixels = image.Width*image.Height;

            //generate sequence of numbers through seed
            Random r = new Random(key);

            //Generates a set of numbers {0,...,n}, where n = amount of pixels in an image. Then it randomly selects numbers from this list, which will correspond to a pixel position in that image
            List<int> pixelPositions = Enumerable.Range(0, numberOfPixels).OrderBy(x => r.Next(0, numberOfPixels)).Take(pixelsNeeded).ToList();

            int tempPosX;
            int tempPosY;
            for (int i = 0; i < pixelsNeeded; i++)
            {
                tempPosX = pixelPositions[i]%image.Width;
                tempPosY = pixelPositions[i]/image.Width;
                //make new pixel 
                Pixel pixel = new Pixel(image.GetPixel(tempPosX, tempPosY), tempPosX, tempPosY);
                PixelList.Add(pixel);
            }
        }
        private void GetRandomPixelsAddToList1(Bitmap image, string passphrase, int amount)
        {
            //Create array at the lenght of a 'amount of total pixels'
            int[] array = new int[Convert.ToInt32(image.Width * image.Height) * 2];

            int seed = ShortenAndParsePassphraseToInt32(passphrase);

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

        //
        public void EmbedPixelListIntoImagePixels(List<Pixel> PixelList)//already has acces to coverimage
        {
            throw new NotImplementedException();
        }

        /*Method for getting the value of bitpairs into a list of ints from a byte-array*/
        public List< IEnumerable<byte>> ChopBytesToBitPairs(byte[] byteArray)
        {
            /*List fo int values*/
            List< IEnumerable<byte>> messageValues = new List<IEnumerable <byte>>();

            foreach (byte value in byteArray)
            {
                messageValues.Add(ConvertBitsToInt(value));
            }

            return messageValues;
        }

        /*Method for converting bitpairs to ints from a byte*/
        public IEnumerable<byte> ConvertBitsToInt(byte byteValue)
        {
            byte value;
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
        public int CalculateRequiredPixels(byte[] byteArray)
        {
            int amount = byteArray.Length*PixelsPerByte;
            return amount;
        }

        //Method for swapping pixels in the list og matched edges
        public void PixelSwap(List<Edge> matchedEdges)
        {
            for (int i = 0; i < matchedEdges.Count; i++)
            {
                TradePixelValues(matchedEdges[i].VertexPixelOne, matchedEdges[i].VertexPixelTwo);
            }
        }

        //Method for helping pixels trade values
        public void TradePixelValues(Pixel pixelOne, Pixel pixelTwo)
        {
            int tempPosX = pixelOne.PosX;
            int tempPosY = pixelOne.PosY;

            pixelOne.PosX = pixelTwo.PosX;
            pixelOne.PosY = pixelTwo.PosY;

            pixelTwo.PosX = tempPosX;
            pixelTwo.PosY = tempPosY;
        }

        public void PixelModify(Vertex UnmatchedVert)
        {
            bool checker = true;

            while(checker)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (UnmatchedVert.TargetValues[i] == UnmatchedVert.CalculateVertexValue())
                    {
                        checker = false;
                    }
                    else
                    {
                        UnmatchedVert.PixelsForThisVertex[i] = Bitmap.SetPixel(UnmatchedVert.PixelsForThisVertex[i].PosX,
                            UnmatchedVert.PixelsForThisVertex[i].PosY, UnmatchedVert.PixelsForThisVertex[i].Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }

    }
}
