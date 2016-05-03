using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemUI.Events
{
    public delegate void DisplayNotificationEventHandler(DisplayNotificationEvent e);

    public class DisplayNotificationEvent : EventArgs
    {
        public string Notification { get; private set; }

        public DisplayNotificationEvent(string notification)
        {
            Notification = notification;
        }
    }
}
