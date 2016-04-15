namespace StegomaticProject
{
    public interface IStegoControl
    {
        public void Run(string path, string message);
        public void ToggleEmbedOrExtract(ToggleEvent e);  // Skal kunne fjerne streng-indtastnings-feltet og lave et output felt
        public void OpenOptionsMenu(OpenUIElementEvent e); // Tager imod et delegate, der kan instaitiere WinForms vinduer
        public void ToggleOption(ToggleOptionEvent e); // ToggleOptionEvent indeholder et tal fra 0 til maxKnapperDerKanÆndres
        public void CloseWindow(); 
        public void SaveFile(string path);
        public void ShowPicture();
    }
}