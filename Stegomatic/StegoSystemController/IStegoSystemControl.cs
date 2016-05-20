using StegomaticProject.StegoSystemUI.Events;

namespace StegomaticProject.StegoSystemController
{
    public interface IStegoSystemControl
    {
        void OpenImage(BtnEvent e);
        void EncodeImage(BtnEvent e);
        void DecodeImage(BtnEvent e);
        //void SaveImage(BtnEvent e);
    }
}