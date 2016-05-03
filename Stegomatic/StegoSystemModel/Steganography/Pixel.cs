using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography;


namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Pixel
    {
        public Pixel(Color color, short positionX, short positionY) //constructor
        {
            this.Color = color;
            this.PosX = positionX;
            this.PosY = positionY;

            // Adds the 3 color-values and use modulo, which corresponds to the embedded value of a pixel
            this.EmbeddedValue = (byte)((Color.R + Color.G + Color.B)%GraphTheoryBased.Modulo);
        }
        
        public Color Color { get; set; }

        public byte EmbeddedValue { get; set; }
        public short PosX;
        public short PosY; 

        void GetPixelPos (short PosX, short PosY)
        {

        }

        void GetPixelValue(short R, short G, short B)
        {
            
        }

        
        

        public override string ToString()
        {
            return string.Format("Position X = {0}   Position Y = {1}   R = {2}  G = {3}  B = {4}", this.PosX, this.PosY, this.Color.R, this.Color.G, this.Color.B);
        }
    }
}
