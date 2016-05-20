using System.Text;

namespace StegomaticProject.StegoSystemModel.Miscellaneous
{
    static public class ByteConverter
    {
        static public byte[] StringToByteArray(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        static public string ByteArrayToString(byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray).TrimEnd('\0');
        }
    }
}