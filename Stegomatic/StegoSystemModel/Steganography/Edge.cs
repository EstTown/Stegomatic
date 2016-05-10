using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    /*Class for edges*/
    class Edge
    {
        /*Constructor for the class*/
        public Edge(EncodeVertex vertOne, EncodeVertex vertTwo, Pixel vertPixOne, Pixel vertPixTwo, byte edgeweight)
        {
            /*Gives the edge a unique ID*/
            this.EdgeID = _edgecounter;
            _edgecounter++;

            
            this.VertexOne = vertOne;
            this.VertexTwo = vertTwo;
            this.VertexPixelOne = vertPixOne;
            this.VertexPixelTwo = vertPixTwo;
            this.EdgeWeight = edgeweight;
        }

        /*Property for EdgeID and a counter*/
        public int EdgeID { get; private set; }
        private static int _edgecounter = 1; //might not need this, but keep for now

        /*Properties for two vertices*/
        public EncodeVertex VertexOne { get; set; }
        public EncodeVertex VertexTwo { get; set; }

        /*Properties for two pixels within the vertices*/
        public Pixel VertexPixelOne { get; set; }
        public Pixel VertexPixelTwo { get; set; }

        /*Property for the weight of the edge*/
        private byte _edgeWeight;   
        public byte EdgeWeight 
        {
            get
            {
                return this._edgeWeight;
            }
            private set
            {
                if (value < 0 || value > GraphTheoryBased.MaxEdgeWeight)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this._edgeWeight = value;
            }
        }

        public override string ToString()
        {
            return EdgeWeight + "\n" + VertexPixelOne.ToString() + "\n" + VertexPixelTwo.ToString() + "\n" +
                   VertexOne.ToString() + "\n" + VertexTwo.ToString();
        }
    }
}
