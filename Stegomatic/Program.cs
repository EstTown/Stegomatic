using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemController;
using StegomaticProject.StegoSystemModel;
using StegomaticProject.StegoSystemUI;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using StegomaticProject.StegoSystemModel.Steganography;
using System.Drawing.Imaging;

namespace StegomaticProject
{
    public class Program
    {
        private static byte[] ConvertTextToASCIIValue(string message)
        {

            byte[] arrayBytes = Encoding.ASCII.GetBytes(message);

            return arrayBytes;
        }

        private static int ImageCompare(Bitmap image1, Bitmap image2)
        {
            int localCounter = 0;
            for (int i = 0; i < image1.Width; i++)
            {
                for (int j = 0; j < image1.Height; j++)
                {
                    if (image1.GetPixel(i, j) != image2.GetPixel(i, j))
                    {
                        localCounter++;
                    }
                }
            }
            return localCounter;
        }

        [STAThread]
        static void Main(string[] args)
        {
            
            IStegoSystemModel stegoModel = new StegoSystemModelClass();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);

            

            //Bitmap imageOriginal = new Bitmap(@"C:\Users\EstTown\Desktop\white100.png");
            //Bitmap stegoObject = new Bitmap(@"C:\Users\EstTown\Desktop\MODDED.png");
            //Console.WriteLine(ImageCompare(imageOriginal, stegoObject));

            
            stegoUI.Start();







            //Test Code, for testing Bytearraytovalues and valuestobytearray
            //GraphTheoryBased algorithm = new GraphTheoryBased();

            //byte array message
            /*
            string message = "1900?";
            Console.WriteLine(message);
            
            byte[] messageByte = Encoding.UTF8.GetBytes(message);

            foreach (var b in messageByte)
            {
                Console.Write(b+" ");
            }
            Console.WriteLine();

            List<byte> byteList = algorithm.ByteArrayToValues(messageByte);

            int counter = 1;
            foreach (byte b in byteList)
            {
                Console.Write(b);
                if (counter%4 == 0)
                {
                    Console.WriteLine();
                }
                counter++;
            }
            Console.WriteLine("After printing byteList");
            Console.ReadKey();

            
            List<DecodeVertex> decodeVertexList = new List<DecodeVertex>();
            for (int i = 0; i < byteList.Count; i++)
            {
                DecodeVertex vertex = new DecodeVertex(null, null, null);
                vertex.VertexValue = byteList[i];
                decodeVertexList.Add(vertex);
            }
            Console.WriteLine("after decodeVertexList");
            Console.ReadKey();

            List<byte> byteList2 = algorithm.ValuesToByteArray(decodeVertexList);

            foreach (byte b in byteList2)
            {
                Console.Write(b +" ");                                
            }

            Console.ReadKey();

    */
        }
    }
}