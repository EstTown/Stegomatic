using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StegomaticProject.StegoSystemLogic
{
    public interface IStegoSystem
    {
        Bitmap EncodeMessageInImage(Bitmap coverImage, string message, bool encrypt, bool compress, bool confound); 
        // Output parameter stego-nøgle
        string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey);
    }
}