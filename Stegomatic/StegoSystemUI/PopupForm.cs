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
        public UserInputPopup(string title = "Title", string textBoxTitle = "TextBoxTitle")
        {
            Title = title;
            TextBoxTitle = textBoxTitle;
            InitializeComponent();
        }

        public string Title { get; private set; }
        public string TextBoxTitle { get; private set; }

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
