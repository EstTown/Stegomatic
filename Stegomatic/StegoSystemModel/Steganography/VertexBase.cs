using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class VertexBase
    {
        public VertexBase(params Pixel[] pixels)
        {
            //assign unique ID
            this.Id = _id;
            _id++;
            
            //assign "NumberOfSamples" amount of samples (pixels) to this vertex
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
        public int VertexValue; //value that has to correspond to a certain part of the secret message

        public void CalculateVertexValue()
        {
            int temp = 0;
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                temp += PixelsForThisVertex[i].EmbeddedValue;
                //Console.WriteLine(PixelsForThisVertex[i].EmbeddedValue + "              " + temp );
                //Console.ReadKey();
            }
            this.VertexValue = (temp % GraphTheoryBased.Modulo);
        }
    }
}
