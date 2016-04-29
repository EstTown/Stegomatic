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
        Bitmap EncodeMessageToImage(Bitmap coverImage, bool encrypt, bool compress, bool confound); 
        string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey);
    }
}