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
        public byte[] Decompress(byte[] message)
        {
            MemoryStream compressedStream = new MemoryStream(message);
            GZipStream zipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            MemoryStream resultStream = new MemoryStream();

            try
            {
                zipStream.CopyTo(resultStream);
            }
            catch (InvalidDataException)
            {
                // Message was not compressed by GZipStream, result: nothing happens
                return message;
            }
            return ToBytes(resultStream);
        }

        public byte[] Compress(byte[] message)
        {
            //  Constructs and writes gzipstream with the compressed or decompressed message.

            MemoryStream compressedStream = new MemoryStream();
            GZipStream zipStream = new GZipStream(compressedStream, CompressionMode.Compress);
            zipStream.Write(message, 0, message.Length);
            zipStream.Close();

            return ToBytes(compressedStream);
        }

        private MemoryStream ToStream(byte[] message)
        {
            return new MemoryStream(message);
        }

        private byte[] ToBytes(MemoryStream messageStream)
        {
            return messageStream.ToArray();
        }

        public int ApproxSizeAfterCompression(int sizeBeforeCompression)
        {
            return sizeBeforeCompression / 4;
        }
    }
}
