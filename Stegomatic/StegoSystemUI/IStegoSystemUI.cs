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
        // Get info from UI
        string message { get; }
        string imagePath { get; }
        IConfig config { get; }

        // Update the UI
        void UpdateDisplayedImage(Bitmap newImage);
        void UpdateConfig(IConfig newConfig);

        // Start/end
        void Start();
        void Terminate();
    }
}
