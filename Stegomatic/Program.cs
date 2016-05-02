using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemController;
using StegomaticProject.StegoSystemLogic;
using StegomaticProject.StegoSystemUI;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace StegomaticProject
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IStegoSystem stegoLogic = new StegoSystem();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoControl stegoController = new StegoControl(stegoLogic, stegoUI); 
            
            //Creates WinForms-window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            stegoUI.Start();
        }
    }
}




