using Stegomatic.StegoSystemUI.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stegomatic.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        public string message { get; private set; }
        public string imagePath { get; private set; }
        public IConfig config { get; private set; }

        public void Start()
        {
            Form s = new MainMenu();
            Console.ReadKey();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public void UpdateConfig(IConfig newConfig)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisplayedImage(Bitmap newImage)
        {
            throw new NotImplementedException();
        }
    }
}
