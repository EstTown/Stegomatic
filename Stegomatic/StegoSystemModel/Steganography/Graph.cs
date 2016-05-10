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

        public void ConstructGraph(int pixelsNeeded, List<byte> secretMessage)
        {
            //Console.WriteLine("Starting with constructing vertices");
            //Console.ReadKey();
            ConstructVertices(pixelsNeeded, secretMessage);
            //Console.WriteLine("Constructed a bunch of vertices      -       " + VertexList.Count);
            //Console.ReadKey();
            CheckIfMatched();
            ConstructEdges();
            CheckIfMatched();
            //Console.WriteLine("Before calcGraph");
            //Console.WriteLine("VertexList:    " + VertexList.Count);
            //Console.WriteLine("Edgelist:    " + EdgeList.Count);
            //Console.WriteLine("MatchedEdgelist:    " + MatchedEdges.Count);
            //Console.ReadKey();
            CalcGraphMatching();
            //Console.WriteLine("SUCCESS!!!!");
            //Console.WriteLine("VertexList:    " + VertexList.Count);
            //Console.WriteLine("Edgelist:    " + EdgeList.Count);
            //Console.WriteLine("MatchedEdgelist:    " + MatchedEdges.Count);

            CheckIfMatched();
        }

        public List<Pixel> ModifyGraph()
        {
            //Console.WriteLine("Before swapping pixels");
            //Console.ReadKey();
            PixelSwap();
            //Console.WriteLine("Before modifying");
            //Console.ReadKey();
            PixelModify();

            return PixelList;
        }

        private void ConstructVertices(int pixelsNeeded, List<byte> secretMessage)
        {
            int counter = 0;
            for (int i = 0; i < pixelsNeeded; i += GraphTheoryBased.SamplesVertexRatio)
            {
                Vertex vertex = new Vertex(secretMessage[counter], PixelList[i], PixelList[i + 1], PixelList[i + 2]); //this is hardcoded and can maybe rewritten by using a delegate.
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
                            //if (amountOfEdges == 10000) //Hardcoded limit for edges per vert, for computational reasons. Although, this limit WILL decrease the effectiveness of the algorithm
                            //{
                            //    break;
                            //}
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

            if (tempEdge.EdgeWeight != 0) //edgeweight will never be zero, because a pixel cannot have an embeddedvalue that's equivalent with it's targetvalue
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

        public void CheckIfMatched()
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
            SortVertexListByEdgeAndWeight();

            List<Edge> tempMatched = new List<Edge>();

            foreach (Vertex vert in VertexList)
            {
                if (vert.Active == true && vert.NumberOfEdges > 0)
                {
                    List<Edge> InternalEdgeList = new List<Edge>();
                    foreach (Edge edge in EdgeList)
                    {
                        if (edge.VertexOne == vert || edge.VertexTwo == vert)
                        {
                            InternalEdgeList.Add(edge);
                        }
                    }

                    List<Edge> SortedInternalList = InternalEdgeList.OrderBy(o => o.EdgeWeight).ToList();
                    Edge M = SortedInternalList.First();
                    tempMatched.Add(M);
                }
            }

            MatchedEdges = DeleteDuplicatesInList(tempMatched);
        }

        private List<Edge> DeleteDuplicatesInList(List<Edge> list)
        {
            return list.Distinct().ToList();
        }


        private void PixelSwap()
        {
            foreach (Edge edge in MatchedEdges)
            {
                TradePixelValues(edge.VertexPixelOne, edge.VertexPixelTwo);

                edge.VertexOne.Active = false;
                edge.VertexTwo.Active = false;
            }

        }

        private void TradePixelValues(Pixel pixelOne, Pixel pixelTwo)
        {
            int tempPosX = pixelOne.PosX;
            int tempPosY = pixelOne.PosY;

            pixelOne.PosX = pixelTwo.PosX;
            pixelOne.PosY = pixelTwo.PosY;

            pixelTwo.PosX = tempPosX;
            pixelTwo.PosY = tempPosY;
        }

        private void PixelModify()
        {
            for (int i = 0; i < VertexList.Count; i++)
            {
                if (VertexList[i].Active == true)
                {
                    HelpMethodPixelModify(VertexList[i]);
                }
            }
        }

        private void HelpMethodPixelModify(Vertex vertex) //always uses first sample in vertex
        {
            Console.WriteLine(vertex.PixelsForThisVertex[0].EmbeddedValue + "\n" + vertex.TargetValues[0] + "\n\n");
            int localDifference = 0;
            while ((Math.Abs(vertex.PixelsForThisVertex[0].EmbeddedValue + localDifference)) % GraphTheoryBased.Modulo != vertex.TargetValues[0])
            {
                if (vertex.PixelsForThisVertex[0].Color.R <= 127) //always red channel
                {
                    localDifference++;
                }
                else
                {
                    localDifference--;
                }
                Console.WriteLine(localDifference);

            }

            vertex.PixelsForThisVertex[0].ColorDifference = localDifference;
        }

    }
}