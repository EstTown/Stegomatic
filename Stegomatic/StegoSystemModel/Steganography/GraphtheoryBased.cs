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

        //these constants can be changed, but according to "hetzlmutzel" article, these values are recommended for truecolor
        public static byte SamplesVertexRatio = 3;
        public static byte Modulo = 4;
        public static byte MaxWeight = 10;

        public byte[] Decode(Bitmap coverImage, string seed)
        {
            throw new NotImplementedException();
        }

        public Bitmap Encode(Bitmap coverImage, string seed, byte[] message)
        {
            throw new NotImplementedException();
        }
    }
}
