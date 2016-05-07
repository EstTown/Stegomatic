using StegomaticProject.StegoSystemModel;
using System.Drawing;
using StegomaticProject.StegoSystemUI;
using System;
using StegomaticProject.StegoSystemUI.Events;
using StegomaticProject.StegoSystemUI.Config;
using StegomaticProject.CustomExceptions;

namespace StegomaticProject.StegoSystemController
{
    public class StegoSystemControl : IStegoSystemControl
    {
        private IStegoSystemModel _stegoModel;
        private IStegoSystemUI _stegoUI;
        private Bitmap _image;
        private IVerifyUserInput _verifyUserInput;

        public StegoSystemControl(IStegoSystemModel stegoModel, IStegoSystemUI stegoUI)
        {
            this._stegoModel = stegoModel;
            this._stegoUI = stegoUI;
            this._verifyUserInput = new VerifyUserInput();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _stegoUI.NotifyUser += new DisplayNotificationEventHandler(this.ShowNotification);
            _stegoUI.EncodeBtn += new BtnEventHandler(this.EncodeImage);
            _stegoUI.DecodeBtn += new BtnEventHandler(this.DecodeImage);
            _stegoUI.SaveImageBtn += new BtnEventHandler(this.SaveImage); // MAYBE WE DON'T NEED THIS ONE??
            _stegoUI.OpenImageBtn += new BtnEventHandler(this.OpenImage);
        }

        public void ShowNotification(DisplayNotificationEvent e)
        {
            _stegoUI.ShowNotification(e.Notification, e.Title);
        }

        public void OpenImage(BtnEvent e)
        {
            try
            {
                _stegoUI.OpenImage();
            }
            catch (NotifyUserException exception)
            {
                new DisplayNotificationEvent(exception);
            }
        }

        public void EncodeImage(BtnEvent e)
        {
            try
            {
                IConfig config = _stegoUI.Config;
                string message = _stegoUI.Message;
                Bitmap coverImage = _stegoUI.DisplayImage;
                string encryptionKey = _stegoUI.GetEncryptionKey();
                string stegoSeed = _stegoUI.GetStegoSeed();

                _verifyUserInput.Message(message);
                _verifyUserInput.EncryptionKey(encryptionKey);
                _verifyUserInput.StegoSeed(stegoSeed);

                Bitmap stegoObject = _stegoModel.EncodeMessageInImage(coverImage, message, encryptionKey, stegoSeed, config.Encrypt, config.Compress);

                _stegoUI.SetDisplayImage(stegoObject);
                _stegoUI.ShowNotification("Message encoded successfully.\n" + 
                                         $"EncryptionKey = {encryptionKey}\n" +
                                         $"StegoSeed = {stegoSeed}", "Success");
            }
            catch (NotifyUserException exception)
            {
                ShowNotification(new DisplayNotificationEvent(exception /* ADD STACK TRACE?? */));
            }

            // CATCH ABORTACTIONEXCEPTIONS AND CLEAN UP HERE? 
        }

        public void DecodeImage(BtnEvent btnEvent)
        {
            try
            {
                IConfig config = _stegoUI.Config;
                Bitmap coverImage = _stegoUI.DisplayImage;
                string encryptionKey = _stegoUI.GetEncryptionKey();
                string stegoSeed = _stegoUI.GetStegoSeed();

                _verifyUserInput.EncryptionKey(encryptionKey);
                _verifyUserInput.StegoSeed(stegoSeed);

                string message = _stegoModel.DecodeMessageFromImage(coverImage, encryptionKey, stegoSeed, config.Encrypt, config.Compress);

                _stegoUI.ShowNotification($"Message decoded successfully: \n \"{message}\"", "Success");
            }
            catch (NotifyUserException exception)
            {
                ShowNotification(new DisplayNotificationEvent(exception.Message, exception.Title));
            }

            throw new System.NotImplementedException();
        }

        public void SaveImage(BtnEvent btnEvent)
        {
            _stegoUI.SaveImage();
        }
    }
}