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
            throw new NotImplementedException();
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
        
        public void PixelSwap(List<Pixel> pixelList)
        {
            throw new NotImplementedException();
        }
        
        public void PixelModify(List<Pixel> pixelList)
        {
            throw new NotImplementedException();
        }
    }
}
