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
        public Edge(Vertex vertOne, Vertex vertTwo, Pixel vertPixOne, Pixel vertPixTwo)
        {
            /*Gives the edge a unique ID*/
            this.EdgeID = _edgecounter;
            _edgecounter++;

            this.VertexOne = vertOne;
            this.VertexTwo = vertTwo;
            this.VertexPixelOne = vertPixOne;
            this.VertexPixelTwo = vertPixTwo;
        }

        /*Property for EdgeID and a counter*/
        public int EdgeID { get; private set; }
        private static int _edgecounter = 1; //might not need this, but keep for now

        /*Properties for two vertices*/
        public Vertex VertexOne { get; private set; }
        public Vertex VertexTwo { get; private set; }

        /*Properties for two pixels within the vertices*/
        public Pixel VertexPixelOne { get; private set; }
        public Pixel VertexPixelTwo { get; private set; }

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
    }
}
