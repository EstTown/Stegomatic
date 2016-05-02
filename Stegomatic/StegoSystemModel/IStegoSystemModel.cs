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
        Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, bool encrypt, bool compress);
        // Output parameter stego-nøgle
        string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey, bool encrypt, bool compress);
    }
}