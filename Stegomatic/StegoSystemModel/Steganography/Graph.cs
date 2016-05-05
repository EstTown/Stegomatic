using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            byte lowestWeight = GraphTheoryBased.MaxEdgeWeight;
            byte edgeWeight;
            short amountOfEdges = 0;

            //need double for loop, to check every vertex with every other vertex
            for (int i = 0; i < vertexList.Count; i++)
            {
                for (int j = 0; j < vertexList.Count; j++)
                {
                    if (i != j && vertexList[i].Active == true && vertexList[j].Active == true) //don't want to compare a vertex with itself
                    {
                        bool b = ConstructASingleEdge(vertexList[i], vertexList[j], out edgeWeight); //return true if an edge was created
                        if (b == true)
                        {
                            amountOfEdges++;
                            if (edgeWeight <= lowestWeight)
                            {
                                lowestWeight = edgeWeight;
                            }
                        }
                        
                    }
                }
                vertexList[i].LowestEdgeWeight = lowestWeight;
                vertexList[i].NumberOfEdges = amountOfEdges;
                vertexList[i].Active = false; //after examining a single vertex, it will be deactivated since all of the possible edges already have been evaluated, and therefore there is no need to look at this particular vertex again.
            }
        }

        private bool ConstructASingleEdge(Vertex vertex1, Vertex vertex2, out byte lowestWeight)
        {
            byte weight = GraphTheoryBased.MaxEdgeWeight;
            Edge tempEdge = new Edge(null, null, null, null, 0);
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                for (int j = 0; j < GraphTheoryBased.SamplesVertexRatio; j++)
                {
                    if (vertex1.PixelsForThisVertex[i].EmbeddedValue == vertex2.TargetValues[j] &&
                        vertex2.PixelsForThisVertex[j].EmbeddedValue == vertex1.TargetValues[i] &&
                        CalculateWeightForOneEdge(vertex1.PixelsForThisVertex[i], vertex2.PixelsForThisVertex[j]) <= GraphTheoryBased.MaxEdgeWeight)
                    {
                        //Only have to make 1 edge, for two vertices, but there could potentially be more than 1 pr. 2 vertices
                        if (CalculateWeightForOneEdge(vertex1.PixelsForThisVertex[i],
                                vertex2.PixelsForThisVertex[j]) <= weight)
                        {
                            weight = CalculateWeightForOneEdge(vertex1.PixelsForThisVertex[i],
                                vertex2.PixelsForThisVertex[j]);
                            tempEdge = new Edge(vertex1, vertex2, vertex1.PixelsForThisVertex[i], vertex2.PixelsForThisVertex[j], weight);
                        }
                    }
                }
            }

            if (tempEdge.EdgeWeight != 0 ) //edgeweight will never be zero, because a pixel cannot have an embeddedvalue that's equivalent with it's targetvalue
            {
                EdgeList.Add(tempEdge);
                lowestWeight = weight;
                return true;
            }
            lowestWeight = 0;
            return false;
        }

        private byte CalculateWeightForOneEdge(Pixel pixel1, Pixel pixel2)
        {
            byte weight = (byte)(Math.Abs(pixel1.Color.R - pixel2.Color.R) + Math.Abs(pixel1.Color.G - pixel2.Color.G) +
                     Math.Abs(pixel1.Color.B - pixel2.Color.B));
            return weight;
        }
        
        public void CheckIfMatched(List<Vertex> vertexList) //this will be called multiple times. 
        {
            for (int i = 0; i < VertexList.Count; i++)
            {
                if (vertexList[i].PartOfSecretMessage != vertexList[i].VertexValue)
                {
                    vertexList[i].Active = true;
                }
            }
        }

        private void SortVertexListByEdgeAndWeight()
        {
            VertexList.OrderBy(x => x.NumberOfEdges).ThenBy(x => x.LowestEdgeWeight);
        }

        
        private void CalcGraphMatching()
        {
            //This will calculate a match for each vertex in the graph
            
            //Sort list
            SortVertexListByEdgeAndWeight();

            //Process each vertex in sorted list
            foreach (Vertex x in VertexList)
            {
                if (x.Active == true && x.NumberOfEdges > 0)
                {
                    //Today we're finding the shortest edge for the vertex 'x'
                    List<Edge> InternalEdgeList = new List<Edge>();
                    foreach (Edge edge in EdgeList)
                    {
                        if (edge.VertexOne == x || edge.VertexTwo == x)
                        {
                            //Look! Look! We caught one!
                            InternalEdgeList.Add(edge);
                        }
                    }

                    //Connected edges are now found, and sorted. Yeay!
                    List<Edge> SortedInternalList = InternalEdgeList.OrderBy(x => x.EdgeWeight).ToList();

                    //Create new Edge, from the loweset weighted edge
                    Edge M = SortedInternalList.FirstOrDefault();
                    //Add to list
                    MatchedEdges.Add(M);

                    //Now we're done...

                    //I was lying... we're not...

                    //Now, lets handle the "dirty work"
                    //Killingspree and hiding the evidence!

                    //Deactive both verts connected to selected edge
                    M.VertexOne.Active = false;
                    M.VertexTwo.Active = false;

                    //Now, let's kill ANY edge who knows of these two verts!
                    foreach (Edge edge in EdgeList)
                    {
                        if (edge.VertexOne == M.VertexOne || edge.VertexTwo == M.VertexOne ||
                            edge.VertexOne == M.VertexTwo || edge.VertexTwo == M.VertexTwo)
                        {
                            //KILL! KILL! KILL!
                            EdgeList.Remove(edge);
                        }
                    }
                }
            }
            //Now we should have a list of all selected edges, and no two verts should be connected by more than one edge.
            //Congratulations! You're now a certified killer! :D
        }
    }
}
