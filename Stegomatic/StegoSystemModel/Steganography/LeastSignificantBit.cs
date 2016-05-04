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
            throw new NotImplementedException();
        }

        public byte[] Decode(Bitmap coverImage, string seed)
        {
            throw new NotImplementedException();
        }
    }
}
