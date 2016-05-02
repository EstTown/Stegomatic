using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemController;
using StegomaticProject.StegoSystemModel;
using StegomaticProject.StegoSystemUI;

namespace StegomaticProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IStegoSystemModel stegoModel = new StegoSystemModel.StegoSystemModel();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);

            stegoUI.Start();
        }
    }
}


