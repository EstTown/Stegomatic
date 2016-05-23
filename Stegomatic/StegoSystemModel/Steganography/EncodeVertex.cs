namespace StegomaticProject.StegoSystemModel.Steganography
{
    class EncodeVertex : VertexBase
    {
        public EncodeVertex(byte partOfSecretMessage, params Pixel[] pixels) : base(pixels) 
        {
            this.PartOfSecretMessage = partOfSecretMessage;
            CalculateTargetValues();
            
            // All vertices will be set to active no matter what
            this.Active = true;
            if (partOfSecretMessage != VertexValue)
            {
                this.Active = true;
            }
            else
            {
                this.Active = false;
            }
            
        }
        
        public int LowestEdgeWeight { get; set; }
        public int NumberOfEdges { get; set; }

        public bool Active;
        public int[] TargetValues;
        public byte PartOfSecretMessage;

        public void CalculateTargetValues()
        {
            TargetValues = new int[GraphTheoryBased.SamplesVertexRatio];

            int d;
            if (this.VertexValue < this.PartOfSecretMessage)
            {
                d = (this.PartOfSecretMessage - this.VertexValue);
            }
            else
            {
                d = (this.PartOfSecretMessage - this.VertexValue);
            }

            // Calculate target values
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                TargetValues[i] = GraphTheoryBased.Mod((PixelsForThisVertex[i].EmbeddedValue + d), GraphTheoryBased.Modulo);
            }
        }

        public void AssignWeightToVertex(byte weight)
        {
            LowestEdgeWeight = weight;
        }

        public void AssignNumberOfEdgesToVertex(int edges)
        {
            NumberOfEdges = edges;
        }

        public override string ToString()
        {
            return "ID: " + this.Id + "\n"
                + "Value: " + this.VertexValue + "\n"
                + "Targets: " + TargetValues[0] + " " + TargetValues[1] + " " + TargetValues[2] + "\n"
                + "PartofSMsg: "+ + PartOfSecretMessage +"\n"
                + "Weight: " + LowestEdgeWeight + "\n"
                + "No. edges: " + NumberOfEdges + "\n";
        }
    }
}
