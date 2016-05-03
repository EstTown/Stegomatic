using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegomaticProject.StegoSystemUI.Config;
using StegomaticProject.StegoSystemUI.Events;

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
        void ShowNotification(DisplayNotificationEvent e);

        // Start/End
        void Start();
        void Terminate();

        // Events
        event DisplayNotificationEventHandler NotifyUser;
    }
}
