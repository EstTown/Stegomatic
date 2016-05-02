using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Pixel
    {
        public short R { get; set; }
        public short G { get; set; }
        public short B { get; set; }
        public int EmbeddedValue;
        public short PosX;
        public short PosY; 

        void GetPixelPos (short PosX, short PosY)
        {

        }

        void GetPixelValue(short R, short G, short B)
        {

        }

        // Adds the 3 color-values and use modulo
        void AssignEmbeddedValue(short R, short G, short B)
        {
            EmbeddedValue = R + G + B % 4;
        }

        public override string ToString()
        {
            return string.Format("Position X = {0}   Position Y = {1}   R = {2}  G = {3}  B = {4}", this.PosX, this.PosY, this.R, this.G, this.B);
        }
    }
}
