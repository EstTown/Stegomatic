using StegomaticProject.CustomExceptions;
using System;

namespace StegomaticProject.StegoSystemUI.Events
{
    public delegate void DisplayNotificationEventHandler(DisplayNotificationEvent e);

    public class DisplayNotificationEvent : EventArgs
    {
        public string Notification { get; private set; }
        public string Title { get; private set; }

        public DisplayNotificationEvent(string notification, string title)
        {
            Notification = notification;
            Title = title;
        }

        public DisplayNotificationEvent(NotifyUserException e)
        {
            Notification = e.Message;
            Title = e.Title;
        }
    }
}
