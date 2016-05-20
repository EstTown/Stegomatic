using System;
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
    }
}
