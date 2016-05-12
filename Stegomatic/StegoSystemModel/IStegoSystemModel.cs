using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StegomaticProject.StegoSystemModel
{
    public interface IStegoSystemModel
    {
        Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, string stegoSeed, bool encrypt, bool compress);
        string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey, string stegoSeed, bool encrypt, bool compress);
        Func<int, int, bool, int> CalculateImageCapacity { get; set; }
    }
}