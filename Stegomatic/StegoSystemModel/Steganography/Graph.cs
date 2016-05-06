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
        public Graph(List<Pixel> pixelList, int pixelsNeeded)
        {
            this.PixelList = pixelList;
            this.PixelsNeeded = pixelsNeeded;
        }
        public List<Pixel> PixelList;
        public List<Vertex> VertexList = new List<Vertex>();
        public List<Edge> EdgeList = new List<Edge>();
        public List<Edge> MatchedEdges = new List<Edge>();
        public int PixelsNeeded { get; }

        public void ConstructGraph(int pixelsNeeded, byte[] secretMessage)
        {
            ConstructVertices(pixelsNeeded, secretMessage);
            CheckIfMatched();
            ConstructEdges();
            CheckIfMatched();
            CalcGraphMatching();
            
        }

        public List<Pixel> ModifyGraph()
        {
            
            return PixelList;
        }
        private void ConstructVertices(int pixelsNeeded, byte[] secretMessage)
        {
            int counter = 0;
            for (int i = 0; i < pixelsNeeded; i+=GraphTheoryBased.SamplesVertexRatio)
            {
                Vertex vertex = new Vertex(secretMessage[counter], PixelList[i], PixelList[i+1], PixelList[i+2]); //this is hardcoded and can maybe rewritten by using a delegate.
                VertexList.Add(vertex);
                counter++;
            }
        }
        private void ConstructEdges()
        {
            byte lowestWeight = GraphTheoryBased.MaxEdgeWeight;
            byte edgeWeight;
            short amountOfEdges = 0;

            //need double for loop, to check every vertex with every other vertex
            for (int i = 0; i < VertexList.Count; i++)
            {
                for (int j = 0; j < VertexList.Count; j++)
                {
                    if (i != j && VertexList[i].Active == true && VertexList[j].Active == true) //don't want to compare a vertex with itself
                    {
                        bool b = ConstructASingleEdge(VertexList[i], VertexList[j], out edgeWeight); //return true if an edge was created
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
                VertexList[i].LowestEdgeWeight = lowestWeight;
                VertexList[i].NumberOfEdges = amountOfEdges;
                VertexList[i].Active = false; //after examining a single vertex, it will be deactivated since all of the possible edges already have been evaluated, and therefore there is no need to look at this particular vertex again.
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
        public void CheckIfMatched() //this will be called multiple times. 
        {
            for (int i = 0; i < VertexList.Count; i++)
            {
                if (VertexList[i].PartOfSecretMessage != VertexList[i].VertexValue)
                {
                    VertexList[i].Active = true;
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
                    List<Edge> SortedInternalList = InternalEdgeList.OrderBy(o => o.EdgeWeight).ToList();

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
        public void PixelSwap(List<Edge> matchedEdges)
        {
            for (int i = 0; i < matchedEdges.Count; i++)
            {
                TradePixelValues(matchedEdges[i].VertexPixelOne, matchedEdges[i].VertexPixelTwo);

            }
        }

        //Method for helping pixels trade values
        public void TradePixelValues(Pixel pixelOne, Pixel pixelTwo)
        {
            int tempPosX = pixelOne.PosX;
            int tempPosY = pixelOne.PosY;

            pixelOne.PosX = pixelTwo.PosX;
            pixelOne.PosY = pixelTwo.PosY;

            pixelTwo.PosX = tempPosX;
            pixelTwo.PosY = tempPosY;
        }
        public void PixelModify()
        {
            bool checker = true;

            while (checker)
            {
                for (int i = 0; i < VertexList.Count; i++)
                {
                    if (unmatchedVertex.TargetValues[i] == unmatchedVertex.CalculateVertexValue())
                    {
                        checker = false;
                    }
                    else
                    {
                        //UnmatchedVert.PixelsForThisVertex[i] = Bitmap.SetPixel(UnmatchedVert.PixelsForThisVertex[i].PosX,
                        //UnmatchedVert.PixelsForThisVertex[i].PosY, UnmatchedVert.PixelsForThisVertex[i].Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }
    }
}
