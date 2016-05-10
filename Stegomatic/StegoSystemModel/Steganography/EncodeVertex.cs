using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class EncodeVertex : VertexBase
    {
        public EncodeVertex(byte partOfSecretMessage, params Pixel[] pixels) : base(pixels) 
        {
            //all vertices will be set to active, no matter what
            this.Active = false;

            this.PartOfSecretMessage = partOfSecretMessage;

            CalculateTargetValues();
        }
        
        public byte LowestEdgeWeight { get; set; }
        public short NumberOfEdges { get; set; }

        public bool Active;
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
                TargetValues[i] = (byte)((PixelsForThisVertex[i].EmbeddedValue + d) % GraphTheoryBased.Modulo);
            }
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
