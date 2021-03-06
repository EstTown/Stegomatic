﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    public class GraphTheoryBased : IStegoAlgorithm
    {
        // Create list for values of bitpairs in message
        public List<IEnumerable<byte>> BitPairValueList = new List<IEnumerable<byte>>();
        public const int SamplesVertexRatio = 3, Modulo = 4, MaxEdgeWeight = 10, PixelsPerByte = 12;
        
        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public byte[] Decode(Bitmap coverImage, string seed)
        {
            // First we have to get the information of how much data was embedded. 
            // Then we can decode the message and put it into a byte array

            int pixelsToSkip;
            int amountOfPixels = GetMetaData(coverImage, seed, out pixelsToSkip);
            
            // Actual decoding begins
            List<Pixel> pixelList = GetRandomPixelsAddToList2(coverImage, seed, amountOfPixels + pixelsToSkip);
            
            pixelList = ShortenPixelList(pixelList, pixelsToSkip);
            
            Graph graph = new Graph();
            List<DecodeVertex> decodeVertexList = graph.ConstructGraph(pixelList, amountOfPixels);
            
            List<byte> byteList = new List<byte>();
            byteList = ValuesToByteArray(decodeVertexList);
            
            return byteList.ToArray();
        }

        private List<Pixel> ShortenPixelList(List<Pixel> pixelList, int pixelsToSkip)
        {
            for (int i = pixelsToSkip-1; i >= 0; i--)
            {
                pixelList.RemoveAt(i);
            }
            return pixelList;
        }

        public int GetMetaData(Bitmap coverImage, string seed, out int pixelsToSkip)
        {
            // Always read 120 pixels = 10 chars
            int amountOfPixels = 120;
            List<Pixel> pixelList = GetRandomPixelsAddToList2(coverImage, seed, amountOfPixels);

            Graph graph = new Graph();
            List<DecodeVertex> decodeVertexList = graph.ConstructGraph(pixelList, amountOfPixels);

            List<byte> byteList = new List<byte>();
            byteList = ValuesToByteArray(decodeVertexList);
            

            // Remove unnecessary bytes not part of meta data
            for (int i = byteList.Count-1; i >= 0; i--)
            {
                if (byteList[i] != 63)
                {
                    byteList.RemoveAt(i);
                }
                else if (byteList[i] == 63)
                {
                    byteList.RemoveAt(i);
                    break;
                }
            }
            
            pixelsToSkip = (byteList.Count+1)*GraphTheoryBased.PixelsPerByte; 
            // Amount of pixels that metadata requires
            
            string metaData = Encoding.UTF8.GetString(byteList.ToArray());
            
            // Analyse metadata
            int characters;
            bool b = Int32.TryParse(metaData, out characters);

            if (b == false)
            {
                throw new NotifyUserException("Metadata not found. \nMost likely due to a wrong password.");
            }
            
            return characters*GraphTheoryBased.PixelsPerByte;
        }

        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            message = AddMetaData(message);

            int amountOfPixels = CalculateRequiredPixels(message);
            if (amountOfPixels > (coverImage.Width * coverImage.Height))
            {
                throw new NotifyUserException("Too many characters.");
            }
            List<Pixel> pixelList = GetRandomPixelsAddToList2(coverImage, seed, amountOfPixels);
            
            // Convert secret message
            List<byte> newMessage = ByteArrayToValues(message);

            // At some point we need to calculate a graph, therefore make a new graph
            Graph graph = new Graph();
            List<EncodeVertex> encodeVertexList;
            List<Edge> listOfEdges = graph.ConstructGraph(pixelList, amountOfPixels, newMessage, out encodeVertexList);

            graph.ModifyGraph(listOfEdges, encodeVertexList);
            
            coverImage = EmbedPixelListIntoImage(pixelList, coverImage, amountOfPixels);
            
            return coverImage;
        }

        private int CalculateEdgeWeight(Pixel vertPixOne, Pixel vertPixTwo)
        {
            int weight = 0;
            return weight;
        }

        private int ShortenAndParsePassphraseToInt32(string passphrase) 
        {
            // Converts user stego passphrase into an int32 seed

            int seed;
            string temp = "";

            // Convert passphrase to UTF8 values
            passphrase = ConvertTextToUTF8Value(passphrase);

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

        private string ConvertTextToUTF8Value(string passphrase)
        {

            byte[] arrayBytes = Encoding.UTF8.GetBytes(passphrase);

            string convertedPassphrase = "";

            //put values from arrayBytes into string
            foreach (byte element in arrayBytes)
            {
                convertedPassphrase += Convert.ToString(element);
            }

            return convertedPassphrase;
        }
        
        private List<Pixel> GetRandomPixelsAddToList2(Bitmap image, string passphrase, int pixelsNeeded)
        {
            List<Pixel> pixelList = new List<Pixel>();
            int key = ShortenAndParsePassphraseToInt32(passphrase);
            int numberOfPixels = image.Width * image.Height;

            // Generate sequence of numbers through seed
            Random r = new Random(key);

            // Generates a set of numbers {0,...,n}, where n = amount of pixels in an image. 
            // Then it randomly selects numbers from this list, which will correspond to a pixel position in that image
            List<int> pixelPositions = Enumerable.Range(0, numberOfPixels).OrderBy(x => r.Next(0, numberOfPixels)).Take(pixelsNeeded).ToList();

            int tempPosX;
            int tempPosY;
            for (int i = 0; i < pixelsNeeded; i++)
            {
                tempPosX = pixelPositions[i] % image.Width;
                tempPosY = pixelPositions[i] / image.Width;
                // Make new pixel 
                Pixel pixel = new Pixel(image.GetPixel(tempPosX, tempPosY), tempPosX, tempPosY);
                pixelList.Add(pixel);
            }
            return pixelList;
        }

        private List<Pixel> GetRandomPixelsAddToList1(Bitmap image, string passphrase, int amount)
        {

            List<Pixel> PixelList = new List<Pixel>();
            // Create array at the lenght of a 'amount of total pixels'
            int[] array = new int[Convert.ToInt32(image.Width * image.Height) * 2];

            int seed = ShortenAndParsePassphraseToInt32(passphrase);

            // Create random-object, based on incoming seed
            Random r = new Random(seed);

            // Load to array
            for (int j = 0; j < array.Length; j++)
            {
                // This interval controls density of selection
                array[j] = r.Next(2, 10);
            }

            // Reset all variables before each run
            int posx = 0;
            int posy = 0;
            int pixels = 0;
            int i = 0;

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

                // Gets selected pixel data; and creates new Pixel-object and stores in list
                Pixel pix = new Pixel(image.GetPixel(posx, posy), posx, posy);
                PixelList.Add(pix);
                if (PixelList.Count >= amount)
                {
                    break;
                }
                i++;
            }

            return PixelList;
        }
        
        private byte[] AddMetaData(byte[] secretMessage)
        {
            int sizeOfEmbeddedData = secretMessage.Length;

            string stringSize = Convert.ToString(sizeOfEmbeddedData);

            byte[] embeddedDataArrayInfo = new byte[] { }; 
            // Every char to byte array decimal value

            embeddedDataArrayInfo = Encoding.UTF8.GetBytes(stringSize);

            // Define always present character, which seperates metadata from message
            // Could be a problem here, but decode part can fix that
            string seperator = "?";

            byte[] seperaterByteArray = new byte[] { };
            seperaterByteArray = Encoding.UTF8.GetBytes(seperator);
            
            return CombineArrays(embeddedDataArrayInfo, seperaterByteArray, secretMessage);
        }

        private byte[] CombineArrays(byte[] array1, byte[] array2, byte[] array3)
        {
            byte[] combinedArray = new byte[array1.Length+array2.Length+array3.Length];

            int localCounter = 0;

            for (int i = 0; i < array1.Length; i++)
            {
                combinedArray[localCounter] = array1[i];
                localCounter++;
            }
            for (int i = 0; i < array2.Length; i++)
            {
                combinedArray[localCounter] = array2[i];
                localCounter++;
            }
            for (int i = 0; i < array3.Length; i++)
            {
                combinedArray[localCounter] = array3[i];
                localCounter++;
            }
            
            return combinedArray;
        }

        
        // Method for getting the value of bitpairs into a list of ints from a byte-array
        public List<byte> ByteArrayToValues(byte[] byteArray)
        {
            List<byte> Values = new List<byte>();

            foreach (byte item in byteArray)
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

        private List<byte> ValuesToByteArray(List<DecodeVertex> input)
        {
            input.Reverse();
            List<byte> byteList = new List<byte>();

            BitArray bitArray = new BitArray(8);
            int i = 0;

            foreach (var item in input)
            {
                if (item.VertexValue == 0)
                {
                    bitArray[i] = false;
                    bitArray[i + 1] = false;
                }
                else if (item.VertexValue == 1)
                {
                    bitArray[i] = true;
                    bitArray[i + 1] = false;
                }
                else if (item.VertexValue == 2)
                {
                    bitArray[i] = false;
                    bitArray[i + 1] = true;
                }
                else if (item.VertexValue == 3)
                {
                    bitArray[i] = true;
                    bitArray[i + 1] = true;
                }

                i += 2;

                if (i % 8 == 0)
                {
                    byteList.Add(ConvertToByte(bitArray));
                    i = 0;
                }
            }
            byteList.Reverse();
            return byteList;
        }

        private byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
        
        private int CalculateRequiredPixels(byte[] byteArray)
        {
            return byteArray.Length * PixelsPerByte;
        }

        private Bitmap EmbedPixelListIntoImage(List<Pixel> pixelList, Bitmap image, int amountOfPixels)
        {
            for (int i = 0; i < amountOfPixels; i++)
            {
                image.SetPixel(pixelList[i].PosX, pixelList[i].PosY,
                    (Color.FromArgb(pixelList[i].Color.A, pixelList[i].Color.R + pixelList[i].ColorDifference, pixelList[i].Color.G, pixelList[i].Color.B)));
            }
            return image;
        }

        public int CalculateImageCapacity(int height, int width)
        {
            return (height*width)/GraphTheoryBased.PixelsPerByte;
        }
    }
}