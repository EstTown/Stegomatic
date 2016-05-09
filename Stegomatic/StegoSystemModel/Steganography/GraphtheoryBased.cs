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
            //first we have to get the information of how much data was embedded. Then we can decode the message and put it into a byte array
            

            //actual decoding
            throw new NotImplementedException();
        }

        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            //call bunch of methods that prepare for graph construction
            int amountOfPixels = CalculateRequiredPixels(message);
            Console.WriteLine("Pixels calculated: {0}", amountOfPixels);
            GetRandomPixelsAddToList1(coverImage, seed, amountOfPixels);
            Console.WriteLine("pixels in list: {0}", PixelList.Count);

            //convert secretmessage
            List<byte> newMessage = ByteArrayToValues(message);

            
            //at some point we need to calculate a graph, therefore make new graph
            Graph graph = new Graph(PixelList, amountOfPixels);
            Console.WriteLine("Before constuct graph");
            Console.ReadKey();
            graph.ConstructGraph(amountOfPixels, newMessage);
            Console.WriteLine("Constructed a new graph");
            Console.ReadKey();
            Console.WriteLine("will now try to modify graph");

            graph.ModifyGraph();
            Console.WriteLine("Modified graph");
            Console.ReadKey();

            coverImage = EmbedPixelListIntoImage(coverImage, amountOfPixels);

            return coverImage;
        }
        
        public byte CalculateEdgeWeight(Pixel vertPixOne, Pixel vertPixTwo) 
        {
            byte weight = 0;
            return weight;
        }

        private int ShortenAndParsePassphraseToInt32(string passphrase) //converts user stego passphrase into an int32 seed
        {
            int seed;
            string temp = "";

            //convert passphrase to ASCII values
            passphrase = ConvertTextToASCIIValue(passphrase);
            
            while (true)
            {
                bool b = Int32.TryParse(passphrase, out seed);
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
        private string ConvertTextToASCIIValue(string passphrase)
        {

            byte[] arrayBytes = Encoding.ASCII.GetBytes(passphrase);

            string convertedPassphrase = "";

            //put values from arrayBytes into string
            foreach (byte element in arrayBytes)
            {
                convertedPassphrase += Convert.ToString(element);
            }
            
            return convertedPassphrase;
        }
        private void GetRandomPixelsAddToList2(Bitmap image, string passphrase, int pixelsNeeded)
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

        private byte[] AddMetaData(byte[] secretMessage)
        {
            int sizeOfEmbeddedData = secretMessage.Length;

            string stringSize = sizeOfEmbeddedData.ToString();

            byte[] embeddedDataArrayInfo = new byte[stringSize.Length]; //every char to byte array decimal value
            
            for (int i = 0; i < stringSize.Length; i++)
            {
                embeddedDataArrayInfo[i] = Convert.ToByte(stringSize[i]);
            }

            //define always present character, which seperates metadata from message
            //could be a problem here, but decode part can fix that
            string seperater = "?";

            byte[] seperaterByteArray = Encoding.ASCII.GetBytes(seperater);


            //call help method for combining all 3 arrays
            byte[] array2 = new byte[2];
            return array2;
        }

        /*Method for getting the value of bitpairs into a list of ints from a byte-array*/
        public List<Byte> ByteArrayToValues(byte[] byteArray)
        {
            List<Byte> Values = new List<byte>();

            foreach (Byte item in byteArray)
            {
                BitArray bitValues = new BitArray(BitConverter.GetBytes(item).ToArray());
                for (int index = 7; index > -1; index -= 2)
                {
                    if (bitValues[index] == true && bitValues[index - 1] == true)
                    {
                        Values.Add(3);
                    }
                    else if (bitValues[index] == true && bitValues[index - 1] == false)
                    {
                        Values.Add(2);
                    }
                    else if (bitValues[index] == false && bitValues[index - 1] == true)
                    {
                        Values.Add(1);
                    }
                    else
                    {
                        Values.Add(0);
                    }
                }
            }

            return Values;
        }

        private int CalculateRequiredPixels(byte[] byteArray)
        {
            int amount = byteArray.Length*PixelsPerByte;
            return amount;
        }
        private Bitmap EmbedPixelListIntoImage(Bitmap image, int amountOfPixels)//already has acces to coverimage
        {
            for (int i = 0; i < amountOfPixels; i++)
            {
                image.SetPixel(PixelList[i].PosX, PixelList[i].PosY, 
                    (Color.FromArgb(PixelList[i].Color.A, PixelList[i].Color.R+PixelList[i].ColorDifference, PixelList[i].Color.G, PixelList[i].Color.B)));
            }
            return image;
        }
    }
}
