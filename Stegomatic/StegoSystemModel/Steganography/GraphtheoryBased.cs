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
        public GraphTheoryBased(/*Bitmap coverImage, string seed*/) //constructor
        {
            /*
            this.CoverImage = coverImage;
            this.Seed = seed;
            */
        }
        /*
        public Bitmap CoverImage { get; }
        public string Seed { get; }
        */

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

        /*Method for calculating the weight of the edge*/
        public byte CalculateEdgeWeight(Vertex vertOne, Vertex vertTwo)
        {
            byte weight = 0;
            return weight;
        }
    }
}
