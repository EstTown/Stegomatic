using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemUI
{
    public class ImageData
    {

        public static string[] GetImageInfo(Image image, string file)
        {
            /*
            0 - Width
            1 - Height
            2 - Filesize
            */

            var fileLength = new FileInfo(file).Length;
            var filename = new FileInfo(file).Name;
            string[] tmp = { image.Width.ToString(), image.Height.ToString(), fileLength.ToString(), filename};

            return tmp;
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }


    }
}
