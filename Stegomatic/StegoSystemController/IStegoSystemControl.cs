using StegomaticProject.StegoSystemUI.Config;
using StegomaticProject.StegoSystemUI.Events;

namespace StegomaticProject.StegoSystemController
{
    public interface IStegoSystemControl
    {
        void OpenImage();
        void EncodeImage(BtnEvent e);
        void DecodeImage(BtnEvent e);
        void SaveImage(BtnEvent e);
        void ShowPicture();
    }
}