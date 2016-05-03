using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
