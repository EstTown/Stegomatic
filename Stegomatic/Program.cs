using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegomaticLogic;
using StegomaticProject.StegomaticUI;

namespace StegomaticProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IStegomatic stegoLogic = new Stegomatic();
            IStegomaticUI stegoUI = new StegomaticWinForm();
            IStegoControl stegoController = new IStegoControl(stegoLogic, stegoUI);
        }
    }
}
