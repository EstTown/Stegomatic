using StegomaticProject.StegoSystemUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        public StegoSystemWinForm()
        {
            //Creates new WinForms-window of Form1
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public IConfig config { get; private set; }
        public string message { get; private set; }
        public string pathOfCoverImage { get; private set; }

        public void SetDisplayImage(Bitmap newImage)
        {
            throw new NotImplementedException();
        }

        public void ShowNotification()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            Console.ReadKey();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}