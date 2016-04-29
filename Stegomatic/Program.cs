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
            IStegoSystem stegoLogic = new StegoSystem();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoControl stegoController = new StegoControl(stegoLogic, stegoUI);

            stegoUI.Start();
        }
    }
}


