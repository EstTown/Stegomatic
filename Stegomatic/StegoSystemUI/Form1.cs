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
using StegomaticProject.StegoSystemModel;

namespace StegomaticProject.StegoSystemUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Event, listening to changes in textbox - used for updating char-count
            txtbox_input.TextChanged += new EventHandler(this.txtbox_input_TextChanged);
            btn_encode.Click += new EventHandler(StegoSystemModelClass.EncodeWasCalled);
            btn_decode.Click += new EventHandler(StegoSystemModelClass.DecodeWasCalled);
        }

        //These are accesed by the algorithms
        public string text { get; private set; }
        public Bitmap bitmap { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_open_Click(object sender, EventArgs e)
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
                            //Sets image, to be accessed by algortimes, to input image
                            this.bitmap = (Bitmap)Bitmap.FromStream(stream);

                            string filename = OpenFileDialog.FileName;

                            //Display image
                            picbox_image.Image = this.bitmap;

                            //Get image info
                            string[] imageinfo = ImageData.GetImageInfo(this.bitmap, filename);

                            //Set labels to imageinfo
                            label_about.Text = "About image: " + imageinfo[3];
                            label_width.Text = imageinfo[0];
                            label_height.Text = imageinfo[1];
                            label_filesize.Text = imageinfo[2] + " Bytes";
                            label_capacity.Text = Convert.ToString((this.bitmap.Height*this.bitmap.Width*0.18)/12);

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
            this.text = txtbox_input.Text;
            //Output text to console
            Console.WriteLine("Text: \n" + this.text);
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

            //CALL ALGOTHIME HERE!!!

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
            try
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

                    progressBar1.Value = Convert.ToInt32((text / capacity) * 100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
