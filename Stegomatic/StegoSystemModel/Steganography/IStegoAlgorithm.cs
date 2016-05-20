using System.Drawing;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    public interface IStegoAlgorithm
    {
        Bitmap Encode(Bitmap coverImage, string seed, byte[] message);
        byte[] Decode(Bitmap coverImage, string seed);
        int CalculateImageCapacity(int height, int width);
    }
}
