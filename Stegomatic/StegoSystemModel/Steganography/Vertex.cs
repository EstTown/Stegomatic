using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Vertex
    {
        public Vertex(byte partOfSecretMessage, params Pixel[] pixels)
        {
            //assign unique ID
            this.Id = _id;
            _id++;

            //all vertices will be set to active, no matter what
            this.Active = false;

            //assign "NumberOfSamples" amount of samples (pixels) to this vertex
            PixelsForThisVertex = new Pixel[GraphTheoryBased.SamplesVertexRatio];
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                PixelsForThisVertex[i] = pixels[i];
            }
            
            this.PartOfSecretMessage = partOfSecretMessage;
            CalculateVertexValue();
            CalculateTargetValues();
        }
        private static short _id = 0;
        public short Id { get; }

        public byte LowestEdgeWeight { get; set; }
        public short NumberOfEdges { get; set; }

        public bool Active;
        public byte VertexValue; //value that has to correspond to a certain part of the secret message
        
        public Pixel[] PixelsForThisVertex;
        public byte[] TargetValues;
        public byte PartOfSecretMessage;

        public void CalculateTargetValues()
        {
            TargetValues = new byte[GraphTheoryBased.SamplesVertexRatio];

            //calculate difference. It could be zero, and if it is, the vertex is already matched, and therefore the targetvalues are of no use. Put in if statement probably. if (d == 0) {dont calculate anything, waste of time}
            byte d = (byte) Math.Abs(this.VertexValue - this.PartOfSecretMessage);

            //calculate targetvalues
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                TargetValues[i] = (byte)(PixelsForThisVertex[i].EmbeddedValue + d);
            }
        }
        public void CalculateVertexValue()
        {
            byte temp = 0;
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                temp += PixelsForThisVertex[i].EmbeddedValue;
            }
            this.VertexValue = (byte)(temp%GraphTheoryBased.Modulo);
        }
        public void AssignWeightToVertex(byte weight)
        {
            LowestEdgeWeight = weight;
        }
        public void AssignNumberOfEdgesToVertex(short edges)
        {
            NumberOfEdges = edges;
        }
        public override string ToString()
        {
            return this.Id + "\n" + this.VertexValue + "\n";
        }
    }
}
