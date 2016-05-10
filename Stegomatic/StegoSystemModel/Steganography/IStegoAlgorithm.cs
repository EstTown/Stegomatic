using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    public interface IStegoAlgorithm
    {
        Bitmap Encode(Bitmap coverImage, string seed, byte[] message);
        byte[] Decode(Bitmap coverImage, string seed);
        int CalculateImageCapacity(int height, int width);
    }
}
