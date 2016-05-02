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
    public class StegoSystemConsole : IStegoSystemUI
    {
        public IConfig config { get; private set; }
        public string message { get; private set; }
        public string pathOfCoverImage { get; private set; }

        public event DisplayNotificationEventHandler EnterPressed;

        public void SetDisplayImage(Bitmap newImage)
        {
            throw new NotImplementedException();
        }

        public void ShowNotification(DisplayNotificationEvent e)
        {
            Console.WriteLine(e.Notification);
        }

        public void Start()
        {
            Console.WriteLine("Press ENTER to show success notification, ESCAPE to quit.");
            ConsoleKeyInfo detectedKey;

            do
            {
                detectedKey = Console.ReadKey();
                if (detectedKey.Key == ConsoleKey.Enter)
                {
                    if (EnterPressed != null)
                    {
                        EnterPressed(new DisplayNotificationEvent("You've pressed enter"));
                    }
                }
            } while (detectedKey.Key != ConsoleKey.Escape);
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
