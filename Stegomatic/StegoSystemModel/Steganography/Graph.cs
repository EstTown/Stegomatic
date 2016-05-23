using System;
using System.Collections.Generic;
using System.Linq;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Graph
    {
        public List<DecodeVertex> ConstructGraph(List<Pixel> pixelList, int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = ConstructVertices(pixelList, pixelsNeeded);
            return decodeVertexList;
        }

        public List<Edge> ConstructGraph(List<Pixel> pixelList, int pixelsNeeded, List<byte> secretMessage, out List<EncodeVertex> vertexList)
        {
            List<EncodeVertex> encodeVertexList = ConstructVertices(pixelList, pixelsNeeded, secretMessage);
            CheckIfMatched(encodeVertexList);

            int countActiveVerts = 0;
            foreach (var item in encodeVertexList)
            {
                if (item.Active == true)
                {
                    countActiveVerts++;
                }
            }

            List<Edge> listOfEdges = ConstructEdges(encodeVertexList);
            
            CheckIfMatched(encodeVertexList);
            List<Edge> matchedEdges = CalcGraphMatching(encodeVertexList, listOfEdges);
            CheckIfMatched(encodeVertexList);
            vertexList = encodeVertexList;
            
            return matchedEdges;
        }
        
        public void ModifyGraph(List<Edge> matchedEdges, List<EncodeVertex> encodeVertexList)
        {
            // These methods change pixels through the edges and vertices, which contain references to the pixels.
            PixelSwap(matchedEdges);
            PixelModify(encodeVertexList);
        }

        public int _trades = 0;
        private List<DecodeVertex> ConstructVertices(List<Pixel> pixelList, int pixelsNeeded)
        {
            List<DecodeVertex> decodeVertexList = new List<DecodeVertex>();
            for (int i = 0; i < pixelList.Count; i += GraphTheoryBased.SamplesVertexRatio)
            {
                DecodeVertex vertex = new DecodeVertex(pixelList[i], pixelList[i + 1], pixelList[i + 2]); 
                // This should perhaps be rewritten by using a delegate.
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

            foreach (EncodeVertex item1 in encodeVertexList)
            {
                int lowestWeight = GraphTheoryBased.MaxEdgeWeight;
                int edgeWeight;
                int amountOfEdges = 0;

                foreach (EncodeVertex item2 in encodeVertexList)
                {
                    if (item1.Active == true && item2.Active == true)
                    {
                        bool b = ConstructASingleEdge(item1, item2, listOfEdges, out edgeWeight);
                        if (b == true)
                        {
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
                item1.Active = false;
                // After examining a single vertex it will be deactivated,  since all of the possible edges already have 
                // been evaluated, and therefore there is no need to look at this particular vertex again.
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
                        //Only have to make one edge for two vertices, but there could potentially be more than 1 pr. 2 vertices
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

            if (tempEdge.EdgeWeight != 0)
                // Edgeweight will never be zero, because a pixel cannot have an embeddedvalue that is 
                // equivalent with it's targetvalue
            {
                listOfEdges.Add(tempEdge);
                lowestWeight = weight;
                return true;
            }
            lowestWeight = 11; 
            // Random value
            return false;
        }

        public int CalculateWeightForOneEdge(Pixel pixel1, Pixel pixel2)
        {
            int weight = (Math.Abs(pixel1.Color.R - pixel2.Color.R) + Math.Abs(pixel1.Color.G - pixel2.Color.G) +
                     Math.Abs(pixel1.Color.B - pixel2.Color.B));
            return weight;
        }

        public void CheckIfMatched(List<EncodeVertex> encodeVertexList) 
        {
            for (int i = 0; i < encodeVertexList.Count; i++)
            {
                if (encodeVertexList[i].PartOfSecretMessage != encodeVertexList[i].VertexValue)
                {
                    encodeVertexList[i].Active = true;
                }
            }
        }

        private List<EncodeVertex> SortVertexListByEdgeAndWeight(List<EncodeVertex> encodeVertexList)
        {
            return encodeVertexList.OrderBy(x => x.NumberOfEdges).ThenBy(x => x.LowestEdgeWeight).ToList();
        }

        private List<Edge> CalcGraphMatching(List<EncodeVertex> encodeVertexList, List<Edge> listOfEdges)
        {
            encodeVertexList = SortVertexListByEdgeAndWeight(encodeVertexList);

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
                        // In case of this, skip
                    }
                    else
                    {
                        Edge M = SortedInternalList.First();
                        tempMatched.Add(M);
                    }
                }
            }
            List<Edge> matchedEdges = tempMatched;

            return matchedEdges;
        }

        private List<Edge> DeleteDuplicatesInList(List<Edge> list)
        {
            return list.Where(x => CheckIfEdgeIsUnique(list, x) == true).ToList();
        }

        private bool CheckIfEdgeIsUnique(List<Edge> list, Edge edge)
        {
            bool b = true;

            // If it's not unique, it's set to 'false'
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
                }
            }
            return b;
        }

        private void PixelSwap(List<Edge> matchedEdges)
        {
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

        private void TradePixelValues(Pixel pixelOne, Pixel pixelTwo)
        {
            _trades++; 
            //This is used to count number of times we trade pixelvalues

            int tempPosX = pixelOne.PosX;
            int tempPosY = pixelOne.PosY;

            pixelOne.PosX = pixelTwo.PosX;
            pixelOne.PosY = pixelTwo.PosY;

            pixelTwo.PosX = tempPosX;
            pixelTwo.PosY = tempPosY;
        }

        private void PixelModify(List<EncodeVertex> encodeVertexList)
        {
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
    }
}