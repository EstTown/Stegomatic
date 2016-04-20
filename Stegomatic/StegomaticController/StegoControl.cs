using StegomaticProject.StegomaticLogic;
using StegomaticProject.StegomaticUI;
using System.Drawing;

namespace StegomaticProject.StegomaticController
{
    public class StegoControl : IStegoControl
    {
        private IStegomatic stegoLogic;
        private IStegomaticUI stegoUI;
        private Bitmap _image;

        public StegoControl(IStegomatic stegoLogic, IStegomaticUI stegoUI)
        {
            // INITILISERER CONFIG OG SÆTTER OUTPUT IND I RUN FUNKTIONEN

            this.stegoLogic = stegoLogic;
            this.stegoUI = stegoUI;
        }
    }
}