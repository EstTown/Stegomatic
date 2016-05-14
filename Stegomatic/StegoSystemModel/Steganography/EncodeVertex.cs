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
        
        public int LowestEdgeWeight { get; set; }
        public short NumberOfEdges { get; set; }

        public bool Active;
        public int[] TargetValues;
        public byte PartOfSecretMessage;

        public void CalculateTargetValues()
        {
            TargetValues = new int[GraphTheoryBased.SamplesVertexRatio];

            //calculate difference. It could be zero, and if it is, the vertex is already matched, and therefore the targetvalues are of no use. Put in if statement probably. if (d == 0) {dont calculate anything, waste of time}
            //int d =  Math.Abs(this.VertexValue - this.PartOfSecretMessage);

            int d;
            if (this.VertexValue < this.PartOfSecretMessage)
            {
                d = (this.PartOfSecretMessage - this.VertexValue);
            }
            else
            {
                d = (this.PartOfSecretMessage - this.VertexValue);
            }
            //calculate targetvalues
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                TargetValues[i] = GraphTheoryBased.Mod((PixelsForThisVertex[i].EmbeddedValue + d), GraphTheoryBased.Modulo);
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
