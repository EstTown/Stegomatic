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
using System.Runtime.InteropServices;
using System.Threading;

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


        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }




        public void btn_open_Click(object sender, EventArgs e)
        {
            Stream stream = null;

            // Define dialog-object
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Title = "Select an image";
            OpenFileDialog.DefaultExt = ".png";
            OpenFileDialog.Filter = "PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|" +
                                    "JPEG Files (*.jpg)|*.jpg|TIFF Files (*.tif)|*.tiff";

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = OpenFileDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            // Read image here
                            Image image = Image.FromStream(stream);
                            
                            string filename = OpenFileDialog.FileName;

                            // Display image
                            picbox_image.Image = image;

                            // Get image info
                            string[] imageinfo = ImageData.GetImageInfo(image, filename);

                            // Set labels to imageinfo
                            label_about.Text = "About image: " + imageinfo[3];
                            label_width.Text = imageinfo[0];
                            label_height.Text = imageinfo[1];
                            label_filesize.Text = imageinfo[2] + " B";

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file. Original error: " + ex.Message);
                }
            }

        }

        private void btn_encode_Click(object sender, EventArgs e)
        {

            //If user wanted 'enable encryption', show dialog
            if (checkBox_encryption.Checked == true)
            {
                //Prompt for encryption key/password/whatever
                EncyptionkeyPopup popup = new EncyptionkeyPopup();
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    popup.Close();
                }
                if (dialogresult == DialogResult.Cancel)
                {
                    popup.Close();
                }
                popup.Dispose();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
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
            label_char.Text = "Characters: " + (txtbox_input.Text.Length).ToString();
        }

        private void btn_decode_Click(object sender, EventArgs e)
        {

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
