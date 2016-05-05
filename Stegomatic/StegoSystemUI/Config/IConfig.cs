using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemUI.Config
{
    public interface IConfig
    {
        bool encrypt { get; }
        bool compress { get; }
    }
}
