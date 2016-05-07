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
            txtbox_input.TextChanged += new System.EventHandler(this.txtbox_input_TextChanged);
        }

        public event BtnEventHandler DecodeBtnClick;
        public event BtnEventHandler EncodeBtnClick;
        public event BtnEventHandler SaveImageBtnClick;
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

        public void btn_open_Click(object sender, EventArgs e)
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (SaveImageBtnClick != null)
            {
                SaveImageBtnClick(new BtnEvent());
            }

            SaveFileDialog SaveFileDialog = new SaveFileDialog();

            //Image to be saved, goes here
            //Image should be handled by an outside non-form class
            Image file = null;

            SaveFileDialog.Title = "Save image as...";
            SaveFileDialog.DefaultExt = ".png";
            SaveFileDialog.Filter = "PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
            SaveFileDialog.ShowDialog();

            if (SaveFileDialog.FileName != "")
            {

                //Filestream is saved here, from manipulated image.
                //Switch determines which format the image will be saved in.

                switch (SaveFileDialog.FilterIndex)
                {
                    case 1:
                        file.Save(SaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 2:
                        file.Save(SaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }

            }

        }

        private void txtbox_input_TextChanged(object sender, EventArgs e)
        {
            // Update character-count when change is happening
            if (label_capacity.Text == String.Empty)
            {
                label_char.Text = "Characters: " + (txtbox_input.Text.Length).ToString();
            }
            else
            {
                progressBar1.Visible = true;

                label_char.Text = "Characters: " + (txtbox_input.Text.Length).ToString() + " / " + label_capacity.Text;

                double capacity = Convert.ToDouble(label_capacity.Text);
                double text = txtbox_input.Text.Length;

                progressBar1.Value = Convert.ToInt32((text/capacity)*100);
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
