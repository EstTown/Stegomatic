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
        public IConfig Config { get; private set; }
        public string Message { get; private set; }
        public string PathOfCoverImage { get; private set; }
        public Bitmap DisplayImage { get; private set; }

        public Func<int, int, bool, int> ImageCapacityCalculator { get; set; }

        public event DisplayNotificationEventHandler NotifyUser;
        public event BtnEventHandler DecodeBtn;
        public event BtnEventHandler EncodeBtn;
        public event BtnEventHandler SaveImageBtn;
        public event BtnEventHandler OpenImageBtn;

        public void SetDisplayImage(Bitmap newImage)
        {
            // VERIFY SOMEHOW? 
            // PUT INTO UI
            DisplayImage = newImage;
            throw new NotImplementedException();
        }

        public void ShowNotification(string notification, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(notification);
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
                    if (NotifyUser != null)
                    {
                        NotifyUser(new DisplayNotificationEvent("You've pressed enter", "Success"));
                    }
                }
            } while (detectedKey.Key != ConsoleKey.Escape);
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public string GetEncryptionKey()
        {
            throw new NotImplementedException();
        }

        public string GetStegoSeed()
        {
            throw new NotImplementedException();
        }

        public void OpenImage()
        {
            throw new NotImplementedException();
        }

        public void SaveImage(Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
