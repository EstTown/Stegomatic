using StegomaticProject.StegomaticLogic;
using StegomaticProject.StegomaticUI;

namespace StegomaticProject
{
    public class StegoControl : IStegoControl
    {
        private IStegomatic stegoLogic;
        private IStegomaticUI stegoUI;

        public StegoControl(IStegomatic stegoLogic, IStegomaticUI stegoUI)
        {
            this.stegoLogic = stegoLogic;
            this.stegoUI = stegoUI;
        }
    }
}