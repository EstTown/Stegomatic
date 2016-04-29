using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegomatic.StegoSystemUI.Config
{
    public interface IConfig
    {
        bool encrypt { get; set; }
        bool kompress { get; set; }
        bool confound { get; set; }
    }
}
