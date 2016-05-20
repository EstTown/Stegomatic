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
        [STAThread]
        static void Main(string[] args)
        {
            
            IStegoSystemModel stegoModel = new StegoSystemModelClass();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);

            stegoUI.Start();
        }
    }
}