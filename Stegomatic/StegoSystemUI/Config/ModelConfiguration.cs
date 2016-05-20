namespace StegomaticProject.StegoSystemUI.Config
{
    public class ModelConfiguration : IConfig
    {
        public bool Encrypt { get; private set; }
        public bool Compress { get; private set; }

        public ModelConfiguration(bool encrypt, bool compress)
        {
            Encrypt = encrypt;
            Compress = compress;
        }
    }
}
