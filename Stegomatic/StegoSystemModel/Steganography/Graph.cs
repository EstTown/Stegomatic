﻿using System;
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
            //need double for loop, to check every vertex with every other vertex

            for (int i = 0; i < vertexList.Count; i++)
            {
                for (int j = 0; j < vertexList.Count; j++)
                {
                    if (i != j) //don't want to compare a vertex with itself
                    {
                        HelpMethodConstructEdges(vertexList[i], vertexList[j]);
                    }
                }
            }
        }

        private void HelpMethodConstructEdges(Vertex vertex1, Vertex vertex2)
        {
            byte weight = GraphTheoryBased.MaxEdgeWeight;
            Edge tempEdge;
            for (int i = 0; i < GraphTheoryBased.SamplesVertexRatio; i++)
            {
                for (int j = 0; j < GraphTheoryBased.SamplesVertexRatio; j++)
                {
                    if (vertex1.PixelsForThisVertex[i].EmbeddedValue == vertex2.TargetValues[j] &&
                        vertex2.PixelsForThisVertex[j].EmbeddedValue == vertex1.TargetValues[i] &&
                        Math.Abs(vertex1.PixelsForThisVertex[i].Color.R - vertex2.PixelsForThisVertex[j].Color.R) <
                        GraphTheoryBased.MaxEdgeWeight &&
                        Math.Abs(vertex1.PixelsForThisVertex[i].Color.G - vertex2.PixelsForThisVertex[j].Color.G) <
                        GraphTheoryBased.MaxEdgeWeight &&
                        Math.Abs(vertex1.PixelsForThisVertex[i].Color.B - vertex2.PixelsForThisVertex[j].Color.B) <
                        GraphTheoryBased.MaxEdgeWeight
                        )
                    {
                        //Only have to make 1 edge, for two vertices, but there could potentially be more than 1 pr. 2 vertices
                        if (HelpMethodCalculateWeight(vertex1.PixelsForThisVertex[i],
                                vertex2.PixelsForThisVertex[j]) < weight)
                        {
                            weight = HelpMethodCalculateWeight(vertex1.PixelsForThisVertex[i],
                                vertex2.PixelsForThisVertex[j]);
                            tempEdge = new Edge(vertex1, vertex2, vertex1.PixelsForThisVertex[i], vertex2.PixelsForThisVertex[j], weight);
                        }
                    }
                }
            }
            if (tempEdge != null)
            {
                EdgeList.Add(tempEdge);
            }
        }

        private byte HelpMethodCalculateWeight(Pixel pixel1, Pixel pixel2)
        {
            byte weight = (byte)(Math.Abs(pixel1.Color.R - pixel2.Color.R) + Math.Abs(pixel1.Color.G - pixel2.Color.G) +
                     Math.Abs(pixel1.Color.B - pixel2.Color.B));
            return weight;
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
