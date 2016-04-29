using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegomatic.StegoSystemLogic.Steganography
{
    public interface IStegoAlgorithm
    {
        void Encode();
        void Decode();
    }
}
