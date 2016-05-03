using System;
using System.Collections.Generic;
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

namespace StegomaticProject
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IStegoSystemModel stegoModel = new StegoSystemModelClass();
            IStegoSystemUI stegoUI = new StegoSystemConsole(); // new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);


            //secret message byte array
            byte[] secretMessage = new byte[10] {1, 2, 1, 2, 0, 0, 3, 1, 1, 0};

            //make three colors
            Color color1 = Color.FromArgb(0, 45, 192, 145);
            Color color2 = Color.FromArgb(0, 231, 55, 147);
            Color color3 = Color.FromArgb(0, 11, 44, 198);


            //make three pixel objects
            Pixel pixel1 = new Pixel(color1, 23, 55);
            Pixel pixel2 = new Pixel(color2, 198, 2300);
            Pixel pixel3 = new Pixel(color3, 722, 19);

            //make one new vertex
            Vertex vertex1 = new Vertex(secretMessage, pixel1, pixel2, pixel3);

            //print all pixels
            Console.WriteLine(pixel1.ToString() + pixel2.ToString() + pixel3.ToString());

            //print vertex
            Console.WriteLine(vertex1.ToString());

            stegoUI.Start();   
        }
    }
}




