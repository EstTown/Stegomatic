using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Graph
    {
        //Constructor for the class
        public Graph(List<Pixel> pixelList)
        {
            this.PixelList = pixelList;
        }

        public List<Pixel> PixelList;
        public List<Vertex> VertexList = new List<Vertex>();
        public List<Edge> EdgeList = new List<Edge>();
        public List<Edge> MatchedEdges = new List<Edge>();
        
        public void ConstructVertices(int pixelsNeeded, byte[] secretMessage)
        {
            int counter = 0;
            for (int i = 0; i < pixelsNeeded; i+=GraphTheoryBased.SamplesVertexRatio)
            {
                Vertex vertex = new Vertex(secretMessage[counter], PixelList[i], PixelList[i+1], PixelList[i+2]); //this is hardcoded and can maybe rewritten by using a delegate.
                VertexList.Add(vertex);
                counter++;
            }
        }
        
        public void ConstructEdges(List<Vertex> vertexList)
        {
            //need double for loop, to check all pixels in a vertex, triple for loop to check all vertices
            int k = 0;

            for (int i = 0; i < vertexList.Count; i++)
            {
                for (int j = 0; i < GraphTheoryBased.SamplesVertexRatio; j++)
                {
                    for (int k = 0; j < GraphTheoryBased.SamplesVertexRatio; k++)
                    {
                        if (vertexList[i].PixelsForThisVertex[j].EmbeddedValue == vertexList[i].TargetValues[k] && ) 
                    }
                }
            }
        }

        public void HelpMethodConstructEdges(Vertex vertex1, Vertex vertex2)
        {

            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                for (int j = 0; j < GraphTheoryBased.SamplesVertexRatio; j++)
                {
                    if (vertex1.PixelsForThisVertex[i].EmbeddedValue == vertex2.TargetValues[j] &&
                        vertex2.PixelsForThisVertex[j].EmbeddedValue == vertex1.TargetValues[i] &&
                        Math.Abs(vertex1.PixelsForThisVertex[i].Color.R - vertex2.PixelsForThisVertex[j].Color.R) < GraphTheoryBased.MaxEdgeWeight
                        )
                    }
            }
        }
        
        public void CheckIfMatched(List<Vertex> vertexList) //this will be called multiple times
        {
            for (int i = 0; i < VertexList.Count; i++)
            {
                if (vertexList[i].PartOfSecretMessage == vertexList[i].VertexValue)
                {
                    vertexList[i].Active = false;
                }
            }
        }

        public void SortListByEdgeAndWeight()
        {
            throw new NotImplementedException();
        }

        public void CalcGraphMatching()
        {
            throw new NotImplementedException();
        }
    }
}
