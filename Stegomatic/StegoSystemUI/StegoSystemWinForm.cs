using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stegomatic.StegoSystemLogic;
using Stegomatic.StegoSystemUI.Config;

namespace Stegomatic.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        public string GetMessage()
        {
            throw new NotImplementedException();
        }

        public Bitmap GetCoverImage()
        {
            throw new NotImplementedException();
        }

        public IConfig GetConfig()
        {
            throw new NotImplementedException();
        }
    }




    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
    }
}
