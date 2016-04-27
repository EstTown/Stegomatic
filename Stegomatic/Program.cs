using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegomaticLogic;
using StegomaticProject.StegomaticUI;
using StegomaticProject.StegomaticController;

namespace StegomaticProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IStegomatic stegoLogic = new Stegomatic();
            IStegomaticUI stegoUI = new StegomaticWinForm();
            IStegoControl stegoController = new StegoControl(stegoLogic, stegoUI); // Fjern interface maybe? Vi kommer alligevel til at have implementationsdetailer i det.
        }
    }
}

//whatever

