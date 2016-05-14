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
            //return Convert.FromBase64String(text);
            return Encoding.UTF8.GetBytes(text);
            //return Encoding.Unicode.GetBytes(text);
        }

        static public string ByteArrayToString(byte[] byteArray)
        {
            //return Convert.ToBase64String(byteArray).TrimEnd('\0');
            return Encoding.UTF8.GetString(byteArray).TrimEnd('\0');
            //return Encoding.Unicode.GetString(byteArray);
        }
    }
}
    

    





//    static public byte[] ConvertToByteArray(Bitmap picture)
//    {
//    }



//    static public Bitmap ConvertToBitmap(byte[] byteArray)
//    {
//    }
//}
