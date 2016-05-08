using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemUI
{
    public partial class NotificationWindow : Form
    {
        public NotificationWindow()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }


        private void NotificationWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
