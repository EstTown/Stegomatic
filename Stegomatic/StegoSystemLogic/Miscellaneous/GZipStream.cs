using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemLogic.Miscellaneous
{
    public class GZipStream : ICompression
    {
        public byte[] Compress(byte[] uncompressedMessage)
        {
            throw new NotImplementedException();
        }

        public byte[] Decompress(byte[] compressedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
