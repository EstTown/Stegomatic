using StegomaticProject.CustomExceptions;
using StegomaticProject.StegoSystemUI.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegomaticProject.StegoSystemUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            //Event, listening to changes in textbox - used for updating char-count
            txtbox_input.TextChanged += new EventHandler(this.txtbox_input_TextChanged);
            //btn_encode.Click += new EventHandler(StegoSystemModelClass.EncodeWasCalled);
            //btn_decode.Click += new EventHandler(StegoSystemModelClass.DecodeWasCalled);
        }
        public event DisplayNotificationEventHandler NotifyUser;
        public event BtnEventHandler DecodeBtnClick;
        public event BtnEventHandler EncodeBtnClick;
        //public event BtnEventHandler SaveImageBtnClick;
        public event BtnEventHandler OpenImageBtnClick;

        public string EnteredText
        {
            get { return this.txtbox_input.Text; }
            set { this.txtbox_input.Text = value; }
        }

        public bool CompressChecked
        {
            get { return checkBox_compression.Checked; }
        }

        public bool EncryptChecked
        {
            get { return checkBox_encryption.Checked; }
        }

        public string ImageDescriptionAbout
        {
            get { return this.label_about.Text; }
            set { this.label_about.Text = value; }
        }

        public string ImageDescriptionWidth
        {
            get { return this.label_width.Text; }
            set { this.label_width.Text = value; }
        }

        public string ImageDescriptionHeight
        {
            get { return this.label_height.Text; }
            set { this.label_height.Text = value; }
        }

        public string ImageDescriptionFilesize
        {
            get { return this.label_filesize.Text; }
            set { this.label_filesize.Text = value; }
        }

        public string ImageDescriptionCapacity
        {
            get { return this.label_capacity.Text; }
            set { this.label_capacity.Text = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (OpenImageBtnClick != null)
            {
                OpenImageBtnClick(new BtnEvent());
            }

        }

        private void btn_encode_Click(object sender, EventArgs e)
        {
            if (EncodeBtnClick != null)
            {
                EncodeBtnClick(new BtnEvent());
            }
        }

        //private void btn_save_Click(object sender, EventArgs e)
        //{
        //    if (SaveImageBtnClick != null)
        //    {
        //        SaveImageBtnClick(new BtnEvent());
        //    }
        //}

        public void ForceUpdateProgressBar()
        {
            try
            {
                txtbox_input_TextChanged(this, new EventArgs());
            }
            catch (NotifyUserException)
            {
                throw;
            }
        }

        private void txtbox_input_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Update character-count when change is happening
                if (label_capacity.Text == String.Empty)
                {
                    label_char.Text = "Characters: " + txtbox_input.Text.Length;
                }
                else
                {
                    progressBar1.Visible = true;

                    label_char.Text = "Characters: " + txtbox_input.Text.Length + " / " + label_capacity.Text;

                    double capacity = Convert.ToDouble(label_capacity.Text);
                    double text = txtbox_input.Text.Length;

                    progressBar1.Value = Convert.ToInt32((text / capacity) * 100);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                if (NotifyUser != null)
                {
                    NotifyUser(new DisplayNotificationEvent(new NotifyUserException("Too many characters")));
                }
            }
        }

        private void btn_decode_Click(object sender, EventArgs e)
        {
            if (DecodeBtnClick != null)
            {
                DecodeBtnClick(new BtnEvent());
            }
        }

        private void checkBox_encryption_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void picbox_image_Click(object sender, EventArgs e)
        {

        }

        private void label_char_Click(object sender, EventArgs e)
        {
            
        }

    }
}
