using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemController;
using StegomaticProject.StegoSystemLogic;
using StegomaticProject.StegoSystemUI;

namespace StegomaticProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IStegoSystemModel stegoModel = new StegoSystemModel();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);

            stegoUI.Start();
        }
    }
}


