using StegomaticProject.StegoSystemUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
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
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}