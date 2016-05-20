using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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