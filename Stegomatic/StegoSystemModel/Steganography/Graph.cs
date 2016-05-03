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

        List<Pixel> PixelList = new List<Pixel>();
        List<Vertex> VertexList = new List<Vertex>();
        List<Edge> EdgeList = new List<Edge>();
        List<Edge> MatchedEdges = new List<Edge>();

        //Method for constructing vertices
        public void ConstructVertices(List<Pixel> pixelList)
        {
            throw new NotImplementedException();
        }

        //Method for constructing edges
        public void ConstructEdges(List<Vertex> vertexList)
        {
            throw new NotImplementedException();
        }
    }
}
