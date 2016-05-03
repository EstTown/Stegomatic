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

        public const int SamplesVertexRatio = 3;
        public const int Modulo = 4;
        public const int MaxEdgeWeight = 10;

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
    }
}
