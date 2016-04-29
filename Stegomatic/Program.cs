using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stegomatic.StegoSystemController;
using Stegomatic.StegoSystemLogic;
using Stegomatic.StegoSystemUI;

namespace Stegomatic
{
    class Program
    {
        static void Main(string[] args)
        {
            IStegoSystem stegoLogic = new StegoSystem();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoControl stegoController = new StegoControl(stegoLogic, stegoUI); // Fjern interface maybe? Vi kommer alligevel til at have implementationsdetailer i det.
        }
    }
}


