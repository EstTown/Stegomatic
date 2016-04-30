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
        Bitmap EncodeMessageInImage(Bitmap coverImage, string message, string encryptionKey, bool encrypt, bool compress, bool confound); 
        string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey);
    }
}