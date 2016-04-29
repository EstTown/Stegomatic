using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Stegomatic.StegoSystemLogic.Miscellaneous;

namespace Stegomatic.StegoSystemLogic
{
    public class StegoSystem : IStegoSystem
    {
        public string DecodeMessageFromImage(Bitmap coverImage, string encryptionKey)
        {
            bool decrypt = true;
            if (encryptionKey == null)
            {
                decrypt = false;
            }
            throw new NotImplementedException();
        }

        public Bitmap EncodeMessageToImage(Bitmap coverImage, bool encrypt = true, bool compress = true, bool confound = true)
        {
            throw new NotImplementedException();
        }
    }
}
