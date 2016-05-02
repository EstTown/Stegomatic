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
        public Edge(Vertex VertOne, Vertex VertTwo, Pixel VertPixOne, Pixel VertPixTwo)
        {
            /*Gives the edge a unique ID*/
            this.EdgeID = edgecounter;
            edgecounter++;

            this.VertexOne = VertOne;
            this.VertexTwo = VertTwo;
            this.VertexPixelOne = VertPixOne;
            this.VertexPixelTwo = VertPixTwo;
        }

        /*Property for EdgeID and a counter*/
        public int EdgeID { get; private set; }
        public static int edgecounter = 1;

        /*Properties for two vertices*/
        public Vertex VertexOne { get; private set; }
        public Vertex VertexTwo { get; private set; }

        /*Properties for two pixels within the vertices*/
        public Pixel VertexPixelOne { get; private set; }
        public Pixel VertexPixelTwo { get; private set; }

        /*Property for the weight of the edge*/
        public short _edgeWeight;   
        public short EdgeWeight 
        {
            get
            {
                return this._edgeWeight;
            }
            private set
            {
                this._edgeWeight = CalculateVertexWeight(VertexPixelOne, VertexPixelTwo);

                if(_edgeWeight < 0 || _edgeWeight > GraphtheoryBased.MaxWeight)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /*Method for calculating the weight of the edge*/
        public short CalculateVertexWeight(Pixel VertPixelOne, Pixel VertPixelTwo)
        {
            short weight = 0;
            return weight;
        }
    }
}
