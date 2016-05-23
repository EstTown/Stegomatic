using System;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemUI
{
    public partial class UserInputPopup : Form
    {
        public UserInputPopup()
        {
            InitializeComponent();
        }
        
        public string TextBoxTitle
        {
            get { return this.label_popuplabel.Text; }
            set { this.label_popuplabel.Text = value; }
        }

        public string TextContents
        {
            get { return textBox1.Text; }
        }


        private void btn_popup_submit_Click(object sender, EventArgs e)
        {
        }

        private void btn_popup_cancel_Click(object sender, EventArgs e)
        {
        }
    }
}
