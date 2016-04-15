namespace StegomaticProject
{
    public interface IStegoControl
    {
        void Run(string path, string message);
        void ToggleEmbedOrExtract(ToggleEvent e);  // Skal kunne fjerne streng-indtastnings-feltet og lave et output felt
        void OpenOptionsMenu(OpenUIElementEvent e); // Tager imod et delegate, der kan instaitiere WinForms vinduer
        void ToggleOption(ToggleOptionEvent e); // ToggleOptionEvent indeholder et tal fra 0 til maxKnapperDerKanÆndres
        void CloseWindow(); 
        void SaveFile(string path);
        void ShowPicture();
    }
}