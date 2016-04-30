using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemLogic.Miscellaneous
{
    public interface ICompression
    {
        byte[] Compress(byte[] uncompressedMessage);
        byte[] Decompress(byte[] compressedMessage);
    }
}
