using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class DecodeVertex : VertexBase
    {
        public DecodeVertex(params Pixel[] pixels) : base(pixels) { }
    }
}
