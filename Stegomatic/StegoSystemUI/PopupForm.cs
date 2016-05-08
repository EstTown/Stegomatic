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
            //Send key to algoritm
        }

        private void btn_popup_cancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
