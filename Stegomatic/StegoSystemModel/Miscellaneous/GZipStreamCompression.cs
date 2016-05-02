using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace StegomaticProject.StegoSystemModel.Miscellaneous
{
    public class GZipStreamCompression : ICompression
    {
        public byte[] Compress(byte[] uncompressedMessage)
        {
            return Execute(CompressionMode.Compress, uncompressedMessage);
        }

        public byte[] Decompress(byte[] compressedMessage)
        {
            return Execute(CompressionMode.Compress, compressedMessage);
        }

        private byte[] Execute(CompressionMode cm, byte[] message)
        {
            //  Constructs and writes gzipstream with the compressed or decompressed message.

            // NOT SURE WHETHER I'M DOING SHIT DOUBLE HERE!! FEELS LIKE I CAN GET AWAY WITH AN EMPTY MEMORYSTREAM, NO??

            MemoryStream memStreamMessage = ToStream(message);
            GZipStream zipStream = new GZipStream(memStreamMessage, cm, false);
            zipStream.Write(message, 0, message.Length);
            return ToBytes(memStreamMessage);
        }

        private MemoryStream ToStream(byte[] message)
        {
            return new MemoryStream(message);
        }

        private byte[] ToBytes(MemoryStream messageStream)
        {
            return messageStream.ToArray();
            // Check whether there is any loss of data here!! 
        }
    }
}
