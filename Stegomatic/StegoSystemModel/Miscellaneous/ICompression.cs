namespace StegomaticProject.StegoSystemModel.Miscellaneous
{
    public interface ICompression
    {
        byte[] Compress(byte[] message);
        byte[] Decompress(byte[] message);
        int ApproxSizeAfterCompression(int sizeBeforeCompression);
    }
}
