using StegomaticProject.StegoSystemModel;
using System.Drawing;
using StegomaticProject.StegoSystemUI;

namespace StegomaticProject.StegoSystemController
{
    public class StegoSystemControl : IStegoSystemControl
    {
        private IStegoSystemModel stegoLogic;
        private IStegoSystemUI stegoUI;
        private Bitmap _image;

        public StegoSystemControl(IStegoSystemModel stegoLogic, IStegoSystemUI stegoUI)
        {
            // INITILISERER CONFIG OG SÆTTER OUTPUT IND I RUN FUNKTIONEN

            this.stegoLogic = stegoLogic;
            this.stegoUI = stegoUI;
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