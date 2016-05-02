using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemModel.Steganography
{
    class Vertex
    {
        public Vertex()
        {

            this.Id = _id;
            _id++;
        }

        private static short _id = 1;
        public short Id { get; }

        public short LowestEdgeWeight { get; set; }
        

        public void AssignWeightToVertex(short weight)
        {
            LowestEdgeWeight = weight;
        }

    }
}
