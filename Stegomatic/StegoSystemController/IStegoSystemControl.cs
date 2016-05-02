namespace StegomaticProject.StegoSystemController
{
    public interface IStegoSystemControl
    {
        void OpenImage();
        void EncodeImage();
        void DecodeImage();
        void SaveImage();
        void ShowPicture();
    }
}