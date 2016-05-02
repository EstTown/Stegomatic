using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemUI.Config;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemConsole : IStegoSystemUI
    {
        public IConfig config
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string message
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string pathOfCoverImage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
