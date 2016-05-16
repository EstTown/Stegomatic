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
        /*
        public List<Pixel> PixelList;
        public List<EncodeVertex> EncodeVertexList = new List<EncodeVertex>();
        public List<DecodeVertex> DecodeVertexList = new List<DecodeVertex>();
        public List<Edge> EdgeList = new List<Edge>();
        public List<Edge> MatchedEdges = new List<Edge>();
        public int PixelsNeeded { get; }
        */
        
        public List<DecodeVertex> ConstructGraph(List<Pixel> pixelList, int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = ConstructVertices(pixelList, pixelsNeeded);
            return decodeVertexList;
        }

        public List<Edge> ConstructGraph(List<Pixel> pixelList, int pixelsNeeded, List<byte> secretMessage, out List<EncodeVertex> vertexList)
        {
            List<EncodeVertex> encodeVertexList = ConstructVertices(pixelList, pixelsNeeded, secretMessage);
            CheckIfMatched(encodeVertexList);
            List<Edge> listOfEdges = ConstructEdges(encodeVertexList);
            CheckIfMatched(encodeVertexList);
            List<Edge> matchedEdges = CalcGraphMatching(encodeVertexList, listOfEdges);
            CheckIfMatched(encodeVertexList);
            vertexList = encodeVertexList; //
            return matchedEdges;
        }

        public void ModifyGraph(List<Edge> matchedEdges, List<EncodeVertex> encodeVertexList)
        {


            //these methods change pixels, which they do through edges and vertices, which have references to pixels.
            PixelSwap(matchedEdges);
            //Console.WriteLine("Matched edge [0]:        " + matchedEdges[0].ToString());
            //CheckIfMatched(encodeVertexList);
            PixelModify(encodeVertexList);
        }

        private List<DecodeVertex> ConstructVertices(List<Pixel> pixelList, int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = new List<DecodeVertex>();
            for (int i = 0; i < pixelList.Count; i += GraphTheoryBased.SamplesVertexRatio)
            {
                DecodeVertex vertex = new DecodeVertex(pixelList[i], pixelList[i + 1], pixelList[i + 2]); //this is hardcoded and can maybe rewritten by using a delegate.
                decodeVertexList.Add(vertex);
            }
            return decodeVertexList;
        }
        private List<EncodeVertex> ConstructVertices(List<Pixel> pixelList, int pixelsNeeded, List<byte> secretMessage)

        {
            List<EncodeVertex> encodeVertexList = new List<EncodeVertex>();
            int counter = 0;
            for (int i = 0; i < pixelList.Count; i += GraphTheoryBased.SamplesVertexRatio)
            {
                EncodeVertex encodeVertex = new EncodeVertex(secretMessage[counter], pixelList[i], pixelList[i + 1], pixelList[i + 2]); //this is hardcoded and can maybe rewritten by using a delegate.
                encodeVertexList.Add(encodeVertex);
                counter++;
            }
            return encodeVertexList;
        }
        private List<Edge> ConstructEdges(List<EncodeVertex> encodeVertexList)
        {
            List<Edge> listOfEdges = new List<Edge>();
            short amountOfEdges = 0;
            int lowestWeight = GraphTheoryBased.MaxEdgeWeight;
            int edgeWeight;

            foreach (EncodeVertex item1 in encodeVertexList)
            {
                foreach (EncodeVertex item2 in encodeVertexList)
                {
                    if (item1.Active == true && item2.Active == true)
                    {
                        bool b = ConstructASingleEdge(item1, item2, listOfEdges, out edgeWeight);
                        if (b == true)
                        {
                            //if (amountOfEdges >= 32000)
                            //{
                            //    break;
                            //}
                            if (edgeWeight <= lowestWeight)
                            {
                                lowestWeight = edgeWeight;
                            }
                            amountOfEdges++; 
                        }
                    }
                }
                item1.LowestEdgeWeight = lowestWeight;
                item1.NumberOfEdges = amountOfEdges;
                item1.Active = false; //after examining a single vertex, it will be deactivated since all of the possible edges already have been evaluated, and therefore there is no need to look at this particular vertex again.
            }
            return listOfEdges;

        }

        private bool ConstructASingleEdge(EncodeVertex vertex1, EncodeVertex vertex2,List<Edge> listOfEdges, out int lowestWeight)
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
                listOfEdges.Add(tempEdge);
                lowestWeight = weight;
                return true;
            }
            lowestWeight = 11;
            return false;
        }

        private int CalculateWeightForOneEdge(Pixel pixel1, Pixel pixel2)
        {
            int weight = (Math.Abs(pixel1.Color.R - pixel2.Color.R) + Math.Abs(pixel1.Color.G - pixel2.Color.G) +
                     Math.Abs(pixel1.Color.B - pixel2.Color.B));
            return weight;
        }

        public void CheckIfMatched(List<EncodeVertex> encodeVertexList) //this will be called multiple times. 
        {
            for (int i = 0; i < encodeVertexList.Count; i++)
            {
                if (encodeVertexList[i].PartOfSecretMessage != encodeVertexList[i].VertexValue)
                {
                    encodeVertexList[i].Active = true;
                }
            }
        }

        private void SortVertexListByEdgeAndWeight(List<EncodeVertex> encodeVertexList)
        {
            encodeVertexList.OrderBy(x => x.NumberOfEdges).ThenBy(x => x.LowestEdgeWeight);
        }

        private List<Edge> CalcGraphMatching(List<EncodeVertex> encodeVertexList, List<Edge> listOfEdges)
        {
            SortVertexListByEdgeAndWeight(encodeVertexList);

            List<Edge> tempMatched = new List<Edge>();

            foreach (EncodeVertex vert in encodeVertexList)
            {
                if (vert.Active == true && vert.NumberOfEdges > 0)
                {
                    List<Edge> InternalEdgeList = new List<Edge>();
                    foreach (Edge edge in listOfEdges)
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

                        //for (int i = 0; i < listOfEdges.Count; i++)
                        //{
                        //    if (listOfEdges[i].VertexOne.Id == M.VertexOne.Id || listOfEdges[i].VertexTwo.Id == M.VertexOne.Id ||
                        //           listOfEdges[i].VertexOne.Id == M.VertexTwo.Id || listOfEdges[i].VertexTwo.Id == M.VertexTwo.Id)
                        //    {
                        //        listOfEdges.Remove(listOfEdges[i]);
                        //    }
                        //}

                    }

                }
            }
            //Console.WriteLine("Verts:   " + encodeVertexList.Count);
            //Console.WriteLine("Before -     " + tempMatched.Count);

            List<Edge> matchedEdges = DeleteDuplicatesInList(tempMatched);
            ////List<Edge> matchedEdges = tempMatched;
            //Console.WriteLine("After -     " + matchedEdges.Count);

            //foreach (var item in matchedEdges)
            //{
            //    Console.WriteLine(item.VertexOne.Id + " " + item.VertexTwo.Id);
            //}

            return matchedEdges;
        }

        private List<Edge> DeleteDuplicatesInList(List<Edge> list)
        {
            return list.Where(x => CheckIfEdgeIsUnique(list, x) == true).ToList();
        }

        private bool CheckIfEdgeIsUnique(List<Edge> list, Edge edge)
        {
            bool b = true;

            //If it's not unique, it's set to 'false'
            foreach (Edge item in list)
            {
                if (edge.EdgeID != item.EdgeID)
                {
                    if (edge.VertexOne.Id == item.VertexTwo.Id)
                    {
                        b = false;
                        break;
                    }

                    if (edge.VertexTwo.Id == item.VertexOne.Id)
                    {
                        b = false;
                        break;
                    }

                    //if (edge.VertexTwo.Id == item.VertexTwo.Id)
                    //{
                    //    b = false;
                    //    break;
                    //}

                    //if (edge.VertexOne.Id == item.VertexOne.Id)
                    //{
                    //    b = false;
                    //    break;
                    //}

                }

            }
            return b;
        }
        /*
        public List<Edge> CalcGraphMatching2(List<EncodeVertex> encodeVertexList, List<Edge> listOfEdges)
        {
            List<Edge> matchedEdges = new List<Edge>();

            SortVertexListByEdgeAndWeight(encodeVertexList);

            for (int i = 0; i < encodeVertexList.Count/2; i++)
            {
                if (encodeVertexList[i].Active && encodeVertexList[i].NumberOfEdges > 0)
                {
                    Edge matchedEdge = (Edge) listOfEdges.Where(x => (x.VertexOne.Id == encodeVertexList[i].Id ||
                                                                      x.VertexTwo.Id == encodeVertexList[i].Id) &&
                                                                     encodeVertexList[i].LowestEdgeWeight ==
                                                                     x.EdgeWeight);
                    matchedEdges.Add(matchedEdge);
                }
                
            }




            return matchedEdges;
        } 
        */

        private void PixelSwap(List<Edge> matchedEdges)
        {
            //Console.WriteLine(matchedEdges.Count);
            //Console.ReadKey();
            foreach (Edge edge in matchedEdges)
            {
                if (edge.VertexOne.Active == true && edge.VertexTwo.Active == true)
                {
                    TradePixelValues(edge.VertexPixelOne, edge.VertexPixelTwo);
                    edge.VertexOne.Active = false;
                    edge.VertexTwo.Active = false;
                }  
            }
        }

        //int tmp = 1;
        private void TradePixelValues(Pixel pixelOne, Pixel pixelTwo)
        {
            //Console.WriteLine(tmp);
            //tmp++;

            //Console.WriteLine("Before swapped");
            //Console.WriteLine("pix 1: " + pixelOne.ToString());
            //Console.WriteLine("pix 2: " + pixelTwo.ToString());
            int tempPosX = pixelOne.PosX;
            int tempPosY = pixelOne.PosY;

            pixelOne.PosX = pixelTwo.PosX;
            pixelOne.PosY = pixelTwo.PosY;

            pixelTwo.PosX = tempPosX;
            pixelTwo.PosY = tempPosY;


            //Console.WriteLine("After swapped");
            //Console.WriteLine("pix 1: " + pixelOne.ToString());
            //Console.WriteLine("pix 2: " + pixelTwo.ToString());

        }

        public void PixelModify(List<EncodeVertex> encodeVertexList)
        {
            /*
            foreach (var item in encodeVertexList)
            {
                Console.WriteLine("in pixModify");
                Console.WriteLine(item.Active + "   " + item.VertexValue);
            }
            */
            foreach (var item in encodeVertexList)
            {
                if (item.Active == true)
                {
                    HelpMethodPixelModify(item);
                }
            }
        }

        private void HelpMethodPixelModify(EncodeVertex vertex)
        {
            for (int i = 0; i < 1; i++)
            {
                int localDifference = 0;

                if (vertex.PixelsForThisVertex[i].Color.R <= 127)
                {
                    while (GraphTheoryBased.Mod((vertex.PixelsForThisVertex[i].EmbeddedValue + localDifference), GraphTheoryBased.Modulo) != vertex.TargetValues[i])
                    {
                        localDifference++;
                    }
                }

                else if (vertex.PixelsForThisVertex[i].Color.R > 127)
                {
                    while (GraphTheoryBased.Mod((vertex.PixelsForThisVertex[i].EmbeddedValue + localDifference), GraphTheoryBased.Modulo) != vertex.TargetValues[i])
                    {
                        localDifference--;
                    }

                }
                vertex.PixelsForThisVertex[i].ColorDifference = localDifference;
            }
        }

        private void HelpMethodPixelModify2(EncodeVertex vertex)
        {
            //only need to change 1 pixels color 
            int vertexValue = 3; //secret message value = 0
            int pixelvalue = 2;
            int targetValue = 3;

            //calculate difference
        }


        

    }
}