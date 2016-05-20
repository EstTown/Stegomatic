using System.Drawing;


namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Pixel
    {
        public Pixel(Color color, int positionX, int positionY) //constructor
        {
            this.Color = color;
            this.PosX = positionX;
            this.PosY = positionY;
            this.ColorDifference = 0;
            // Adds the 3 color-values and use modulo, which corresponds to the embedded value of a pixel
            CalculateEmbeddedValue();
        }

        public Color Color { get; set; }

        public byte EmbeddedValue { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int ColorDifference { get; set; }

        public void CalculateEmbeddedValue()
        {
            byte embeddedValue = (byte)((Color.R + Color.G + Color.B) % GraphTheoryBased.Modulo);
            this.EmbeddedValue = embeddedValue;
        }

        public override string ToString()
        {
            return string.Format("Position X = {0}   Position Y = {1}   R = {2}  G = {3}  B = {4} EmbVal = {5}",
                this.PosX, this.PosY, this.Color.R, this.Color.G, this.Color.B, this.EmbeddedValue);
        }
    }
}
