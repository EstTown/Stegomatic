using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemUI.Config
{
    public class Config : IConfig
    {
        // Initialiseres i starten af controller-klassen og subscribe til alle configbuttonpressevents <- eller hvad end deres navn er
        public bool encrypt { get; set; }
        public bool kompress { get; set; }
        public bool confound { get; set; }
    }
}
