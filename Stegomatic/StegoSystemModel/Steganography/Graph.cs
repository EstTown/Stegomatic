using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Graph
    {
        public Graph(List<Pixel> pixelList, int pixelsNeeded)
        {
            this.PixelList = pixelList;
            this.PixelsNeeded = pixelsNeeded;
        }

        public List<Pixel> PixelList;
        public List<EncodeVertex> EncodeVertexList = new List<EncodeVertex>();
        public List<DecodeVertex> DecodeVertexList = new List<DecodeVertex>();
        public List<Edge> EdgeList = new List<Edge>();
        public List<Edge> MatchedEdges = new List<Edge>();
        public int PixelsNeeded { get; }

        public List<DecodeVertex> ConstructGraph(int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = ConstructVertices(pixelsNeeded);

            return decodeVertexList;
        }
        public void ConstructGraph(int pixelsNeeded, List<byte> secretMessage)
        {

            ConstructVertices(pixelsNeeded, secretMessage);

            CheckIfMatched();
            ConstructEdges();
            CheckIfMatched();

            CalcGraphMatching();

            CheckIfMatched();
        }

        public List<Pixel> ModifyGraph()
        {
            PixelSwap(); 

            CheckIfMatched();
            PixelModify();

            return PixelList;
        }

        private List<DecodeVertex> ConstructVertices(int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = new List<DecodeVertex>();
            for (int i = 0; i < pixelsNeeded; i += GraphTheoryBased.SamplesVertexRatio)
            {
                DecodeVertex vertex = new DecodeVertex(PixelList[i], PixelList[i + 1], PixelList[i + 2]); //this is hardcoded and can maybe rewritten by using a delegate.
                decodeVertexList.Add(vertex);
            }
            return decodeVertexList;
        }

        private void ConstructVertices(int pixelsNeeded, List<byte> secretMessage)

        {
            int counter = 0;
            for (int i = 0; i < pixelsNeeded; i += GraphTheoryBased.SamplesVertexRatio)
            {
                EncodeVertex encodeVertex = new EncodeVertex(secretMessage[counter], PixelList[i], PixelList[i + 1], PixelList[i + 2]); //this is hardcoded and can maybe rewritten by using a delegate.
                EncodeVertexList.Add(encodeVertex);
                counter++;
            }
        }

        private void ConstructEdges()
        {
            byte lowestWeight = GraphTheoryBased.MaxEdgeWeight;
            byte edgeWeight;
            short amountOfEdges = 0;

            //need double for loop, to check every vertex with every other vertex
            for (int i = 0; i < EncodeVertexList.Count; i++)
            {
                for (int j = 0; j < EncodeVertexList.Count; j++)
                {
                    if (i != j && EncodeVertexList[i].Active == true && EncodeVertexList[j].Active == true) //don't want to compare a vertex with itself
                    {
                        bool b = ConstructASingleEdge(EncodeVertexList[i], EncodeVertexList[j], out edgeWeight); //return true if an edge was created
                        if (b == true)
                        {
                            amountOfEdges++;
                            if (amountOfEdges == 32000) //Hardcoded limit for edges per vert
                            {
                                break;
                            }
                            if (edgeWeight <= lowestWeight)
                            {
                                lowestWeight = edgeWeight;
                            }
                        }
                    }
                }
                EncodeVertexList[i].LowestEdgeWeight = lowestWeight;
                EncodeVertexList[i].NumberOfEdges = amountOfEdges;
                EncodeVertexList[i].Active = false; //after examining a single vertex, it will be deactivated since all of the possible edges already have been evaluated, and therefore there is no need to look at this particular vertex again.
            }
        }

        private bool ConstructASingleEdge(EncodeVertex vertex1, EncodeVertex vertex2, out byte lowestWeight)
        {
            int weight = GraphTheoryBased.MaxEdgeWeight;
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
                            tempEdge = new Edge(vertex1, vertex2, vertex1.PixelsForThisVertex[i], vertex2.PixelsForThisVertex[j], (byte)weight);
                        }
                    }
                }
            }

            if (tempEdge.EdgeWeight != 0) //edgeweight will never be zero, because a pixel cannot have an embeddedvalue that's equivalent with it's targetvalue
            {
                EdgeList.Add(tempEdge);
                lowestWeight = (byte)weight;
                return true;
            }
            lowestWeight = 0;
            return false;
        }

        private int CalculateWeightForOneEdge(Pixel pixel1, Pixel pixel2)
        {
            int weight = (Math.Abs(pixel1.Color.R - pixel2.Color.R) + Math.Abs(pixel1.Color.G - pixel2.Color.G) +
                     Math.Abs(pixel1.Color.B - pixel2.Color.B));
            return weight;
        }

        public void CheckIfMatched() //this will be called multiple times. 
        {
            for (int i = 0; i < EncodeVertexList.Count; i++)
            {
                if (EncodeVertexList[i].PartOfSecretMessage != EncodeVertexList[i].VertexValue)
                {
                    EncodeVertexList[i].Active = true;
                }
            }
        }

        private void SortVertexListByEdgeAndWeight()
        {
            EncodeVertexList.OrderBy(x => x.NumberOfEdges).ThenBy(x => x.LowestEdgeWeight);
        }

        private void CalcGraphMatching()
        {
            SortVertexListByEdgeAndWeight();

            List<Edge> tempMatched = new List<Edge>();

            foreach (EncodeVertex vert in EncodeVertexList)
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

                    if (SortedInternalList.FirstOrDefault() == null)
                    {
                        
                    }
                    else
                    {
                        Edge M = SortedInternalList.First();
                        tempMatched.Add(M);
                    }

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
            foreach (var item in EncodeVertexList)
            {
                Console.WriteLine("in pixModify");
                Console.WriteLine(item.Active + "   " + item.VertexValue);
            }

            foreach (var item in EncodeVertexList)
            {
                if (item.Active == true)
                {
                    HelpMethodPixelModify(item);
                }
            }
        }

        private void HelpMethodPixelModify(EncodeVertex vertex)
        {
            for (int i = 0; i < 3; i++)
            {
                int localDifference = 0;

                if (vertex.PixelsForThisVertex[i].Color.R <= 127)
                {
                    while (mod((vertex.PixelsForThisVertex[i].EmbeddedValue + localDifference), GraphTheoryBased.Modulo) != vertex.TargetValues[i])
                    {
                        localDifference++;
                    }
                }

                else if (vertex.PixelsForThisVertex[i].Color.R > 127)
                {
                    while (mod((vertex.PixelsForThisVertex[i].EmbeddedValue + localDifference), GraphTheoryBased.Modulo) != vertex.TargetValues[i])
                    {
                        localDifference--;
                    }

                }
                vertex.PixelsForThisVertex[i].ColorDifference = localDifference;
            }
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

    }
}