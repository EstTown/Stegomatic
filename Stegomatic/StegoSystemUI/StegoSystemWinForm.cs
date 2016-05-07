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
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        private Form1 _mainMenu { get; set; } // LAV ET INTERFACE HERTIL, DOG FØRST TIL SIDST.
        public string Message { get; private set; }
        public string PathOfCoverImage { get; private set; }
        public Bitmap DisplayImage { get; private set; }
        public IConfig Config
        {
            get { return new ModelConfiguration(_mainMenu.EncryptChecked, _mainMenu.CompressChecked); }
        }

        public event DisplayNotificationEventHandler NotifyUser;
        public event BtnEventHandler DecodeBtn;
        public event BtnEventHandler EncodeBtn;
        public event BtnEventHandler SaveImageBtn;
        public event BtnEventHandler OpenImageBtn;

        public StegoSystemWinForm()
        {
            //Creates new WinForms-window of Form1
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainMenu = new Form1();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _mainMenu.EncodeBtnClick += new BtnEventHandler(this.EncodeBtnClick);
            _mainMenu.DecodeBtnClick += new BtnEventHandler(this.DecodeBtnClick);
            _mainMenu.SaveImageBtnClick += new BtnEventHandler(this.SaveImageBtnClick);
            _mainMenu.OpenImageBtnClick += new BtnEventHandler(this.OpenImageBtnClick);
            // Either we don't need SaveImage here, or we need OpenImage here as well. They may be fine to be handled in the form itself. 
        }

        private void SaveImageBtnClick(BtnEvent e)
        {
            if (SaveImageBtn != null)
            {
                SaveImageBtn(new BtnEvent());
            }
        }

        private void OpenImageBtnClick(BtnEvent e)
        {
            if (OpenImageBtn != null)
            {
                OpenImageBtn(new BtnEvent());
            }
        }

        private void DecodeBtnClick(BtnEvent e)
        {
            if (DecodeBtn != null)
            {
                DecodeBtn(new BtnEvent());
            }
        }

        private void EncodeBtnClick(BtnEvent e)
        {
            if (EncodeBtn != null)
            {
                EncodeBtn(new BtnEvent());
            }
        }

        public void SetDisplayImage(Bitmap newImage)
        {
            // VERIFY AND DISPLAY IT ON UI
            DisplayImage = newImage;
            throw new NotImplementedException();
        }

        public void ShowNotification(string notification, string title = "")
        {
            // Initialize a popup window and show the message!

            NotificationWindow userNotificationWindow = new NotificationWindow(notification, title);
            userNotificationWindow.ShowDialog();
        }

        public void Start()
        {
            Application.Run(_mainMenu);
        }

        public void Terminate()
        {
            throw new NotImplementedException();
            // Har vi overhovedet brug for dette eller er krydset i hjørnet nok? 
            // Hvis ikke, så gør det til en NotSupportedException();
        }

        public string GetEncryptionKey()
        {
            try
            {
                return GetUserStringPopup("Encryption key", "Key:");
            }
            catch (NotifyUserException)
            {
                throw;
            }
        }

        public string GetStegoSeed()
        {
            try
            {
                return GetUserStringPopup("Steganography seed", "Seed:");
            }
            catch (NotifyUserException)
            {
                throw;
            }
        }

        private string GetUserStringPopup(string popupTitle, string popupTextBoxTitle)
        {
            // Create a popup window and return the entered string. 

            UserInputPopup popupWindow = new UserInputPopup(popupTitle, popupTextBoxTitle);
            string userInput = string.Empty;

            DialogResult userResponse = popupWindow.ShowDialog();
            if (userResponse == DialogResult.OK)
            {
                userInput = popupWindow.TextContents;
            }
            else
            {
                throw new NotifyUserException("Action aborted."); // MAKE THIS AN ABORTACTIONEXCEPTION AND CATCH IT?
            }

            popupWindow.Close();
            popupWindow.Dispose();
            return userInput;
        }
    }
}