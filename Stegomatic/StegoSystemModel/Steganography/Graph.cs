using System;
using System.Collections.Generic;
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
        
        public void ConstructVertices(List<Pixel> pixelList)
        {
            throw new NotImplementedException();
        }
        
        public void ConstructEdges(List<Vertex> vertexList)
        {
            throw new NotImplementedException();
        }
        
        public void CheckIfMatched(List<Vertex> vertexList)
        {
            throw new NotImplementedException();
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
