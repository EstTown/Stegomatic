namespace Stegomatic.StegoSystemController
{
    public interface IStegoControl
    {
        void OpenImage();
        void EncodeImage();
        void DecodeImage();
        void SaveImage();



        void Run(string path, string message);
        //void ToggleEmbedOrExtract(ToggleEvent e);  // Skal kunne fjerne streng-indtastnings-feltet og lave et output felt Tror den skal væk den her.
        void OpenOptionsMenu(OpenUIElementEvent e); // Tager imod et delegate, der kan instaitiere WinForms vinduer
        void ToggleOption(ToggleOptionEvent e); // ToggleOptionEvent indeholder et tal fra 0 til maxKnapperDerKanÆndres
        void ShowPicture();
    }
}