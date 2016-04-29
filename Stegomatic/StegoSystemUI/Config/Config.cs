using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegomaticProject.StegoSystemUI.Config
{
    public class Config : IConfig
    {
        public bool encrypt { get; private set; }
        public bool compress { get; private set; }
        public bool confound { get; private set; }

        // Initialiseres i starten af controller-klassen og subscriber til alle configbuttonpressevents <- eller hvad end deres navn er
    }
}
