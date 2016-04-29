using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemUI.Config;

namespace StegomaticProject.StegoSystemUI
{
    public interface IStegoSystemUI
    {
        // Get info from UI
        string message { get; }
        string pathOfCoverImage { get; }
        IConfig config { get; }

        // Modify UI
        void SetDisplayImage(Bitmap newImage);
        void ShowNotification(/*notifyUserEvent e*/);

        // Start/End
        void Start();
        void Terminate();
    }
}
