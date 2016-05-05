using StegomaticProject.StegoSystemUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using StegomaticProject.StegoSystemUI.Events;
using StegomaticProject.StegoSystemUI;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        private Form1 _mainMenu { get; set; } // LAV ET INTERFACE HERTIL, DOG FØRST TIL SIDST.

        public StegoSystemWinForm()
        {
            //Creates new WinForms-window of Form1
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainMenu = new Form1();
            Application.Run(_mainMenu);
        }

        public IConfig Config { get; private set; }
        public string Message { get; private set; }
        public string PathOfCoverImage { get; private set; }
        public Bitmap DisplayImage { get; private set; }

        public event DisplayNotificationEventHandler NotifyUser;
        public event BtnEventHandler DecodeImageBtn;
        public event BtnEventHandler EncodeImageBtn;
        public event BtnEventHandler SaveImageBtn;

        public void SetDisplayImage(Bitmap newImage)
        {
            // VERIFY AND DISPLAY IT ON UI
            DisplayImage = newImage;
            throw new NotImplementedException();
        }

        public void ShowNotification(string notification)
        {
            // Initialize a popup window and show the message!

            throw new NotImplementedException();
        }

        public void Start()
        {
            Console.ReadKey();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public string GetEncryptionKey()
        {
            // ASK USER FOR ENCRYPTION KEY. MAKE THAT POPUPWINDOW AND RETURN THE STRING.
            throw new NotImplementedException();
        }

        public string GetStegoSeed()
        {
            //SAME AS ABOVE
            throw new NotImplementedException();
        }
    }
}