using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    /*Klasse for kanter*/
    class Edge
    {
        /*Constructor for klassen*/
        public Edge(Vertex VertOne, Vertex VertTwo, Pixel VertPixOne, Pixel VertPixTwo)
        {
            /*Tildeler kanten et ID*/
            this.EdgeID = edgecounter;
            edgecounter++;

            this.VertexOne = VertOne;
            this.VertexTwo = VertTwo;
            this.VertexPixelOne = VertPixOne;
            this.VertexPixelTwo = VertPixTwo;
        }

        /*Property til EdgeID samt counter*/
        public int EdgeID { get; private set; }
        public static int edgecounter = 1;

        /*To knuder hvor en kant er mellem*/
        public Vertex VertexOne { get; private set; }
        public Vertex VertexTwo { get; private set; }

        /*To pixler som er i knuderne*/
        public Pixel VertexPixelOne { get; private set; }
        public Pixel VertexPixelTwo { get; private set; }

        /*variabel til vægten på kanten*/
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
            }
        }

        /*Metode til at beregne vægten af kanten*/
        public short CalculateVertexWeight(Pixel VertPixelOne, Pixel VertPixelTwo)
        {
            short weight = 0;
            return weight;
        }
    }
}
