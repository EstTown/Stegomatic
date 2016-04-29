using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stegomatic.StegoSystemUI.Config;

namespace Stegomatic.StegoSystemUI
{
    public interface IStegoSystemUI
    {
        string GetMessage();
        Bitmap GetCoverImage();
        IConfig GetConfig();
    }
}
