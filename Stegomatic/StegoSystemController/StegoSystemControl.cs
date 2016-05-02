using StegomaticProject.StegoSystemModel;
using System.Drawing;
using StegomaticProject.StegoSystemUI;
using System;
using StegomaticProject.StegoSystemUI.Events;

namespace StegomaticProject.StegoSystemController
{
    public class StegoSystemControl : IStegoSystemControl
    {
        private IStegoSystemModel _stegoLogic;
        private IStegoSystemUI _stegoUI;
        private Bitmap _image;
        



        public StegoSystemControl(IStegoSystemModel stegoLogic, IStegoSystemUI stegoUI)
        {
            // INITILISERER CONFIG OG SÆTTER OUTPUT IND I RUN FUNKTIONEN

            this._stegoLogic = stegoLogic;
            this._stegoUI = stegoUI;

            SubscribeToAllEvents(_stegoLogic, stegoUI);
        }

        private void SubscribeToAllEvents(IStegoSystemModel _stegoLogic, IStegoSystemUI stegoUI)
        {
            stegoUI.EnterPressed += new DisplayNotificationEventHandler(stegoUI.ShowNotification);
        }

        public void OpenImage()
        {
            throw new System.NotImplementedException();
        }

        public void EncodeImage()
        {
            throw new System.NotImplementedException();
        }

        public void DecodeImage()
        {
            throw new System.NotImplementedException();
        }

        public void SaveImage()
        {
            throw new System.NotImplementedException();
        }

        public void Run(string path, string message)
        {
            throw new System.NotImplementedException();
        }

        public void ShowPicture()
        {
            throw new System.NotImplementedException();
        }
    }
}