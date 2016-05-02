using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class GraphtheoryBased : IStegoAlgorithm
    {
        public GraphtheoryBased(Bitmap coverImage, string seed) //constructor
        {
            this.CoverImage = coverImage;
            this.Seed = seed;
        }
        public Bitmap CoverImage { get; }
        public string Seed { get; }




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
