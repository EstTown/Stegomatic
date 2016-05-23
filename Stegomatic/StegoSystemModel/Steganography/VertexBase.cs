using System.Linq;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class VertexBase
    {
        public VertexBase()
        {
        }

        public VertexBase(params Pixel[] pixels)
        {
            // Assign unique ID
            this.Id = _id;
            _id++;
            
            // Assign "NumberOfSamples" amount of samples (pixels) to this vertex
            PixelsForThisVertex = new Pixel[GraphTheoryBased.SamplesVertexRatio];
            for (int i = 0; i < pixels.Count(); i++)
            {
                PixelsForThisVertex[i] = pixels[i];
            }
            CalculateVertexValue();
        }
        private static short _id = 0;
        public short Id { get; set; }
        public Pixel[] PixelsForThisVertex;
        public int VertexValue; // Value that has to correspond to a certain part of the secret message

        public void CalculateVertexValue()
        {
            int temp = 0;
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                temp += PixelsForThisVertex[i].EmbeddedValue;
            }
            this.VertexValue = (temp % GraphTheoryBased.Modulo);
        }
    }
}
