using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegomaticUI
{
    public interface IStegomaticUI
    {
        void OpenImage();
        void EncodeImage();
        void DecodeImage();
        void SaveImage();
        void ToggleCompression();
        void ToggleCryptography();
        void ToggleConfounder();
    }
}
