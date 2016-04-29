namespace StegomaticProject.StegoSystemController
{
    public interface IStegoControl
    {
        void OpenImage();
        void EncodeImage();
        void DecodeImage();
        void SaveImage();
        void ShowPicture();
    }
}