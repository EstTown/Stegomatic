using StegomaticProject.CustomExceptions;
using StegomaticProject.StegoSystemUI.Events;
using System;
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
            checkBox_compression.Checked = true;
            checkBox_encryption.Checked = false;
        }

        public event DisplayNotificationEventHandler NotifyUser;
        public event BtnEventHandler DecodeBtnClick;
        public event BtnEventHandler EncodeBtnClick;
        public event BtnEventHandler OpenImageBtnClick;
        public event BtnEventHandler CompressionCheckToggle;

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

        public void ForceUpdateCapacityBar()
        {
            txtbox_input_TextChanged(this, new EventArgs());
        }

        private bool _previouslyOverLimit = false;

        private void txtbox_input_TextChanged(object sender, EventArgs e)
        {
            // Updates capacity bar whenever text in the textbox of the UI is changed.

            try
            {
                // Update character-count when change is happening
                if (label_capacity.Text == String.Empty)
                {
                    CapacityBarUpdateNoValidImage();
                }
                else
                {
                    CapacityBarUpdateValidImage();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                if (NotifyUser != null)
                {
                    NotifyUser(new DisplayNotificationEvent(new NotifyUserException("Too many characters, the message might not fit inside the image.")));
                }
            }
        }

        private void CapacityBarUpdateValidImage()
        {
            // Updates the capacity bar character count and then tries to update the capacity bar visually. 
            // If this fails then an exception is thrown and the bar's visual value is set to maximum, though only
            // if it has not previously been done. This causes the input of too many characters to only throw one
            // exception for each time it crosses the maximum threshhold. 

            double input = txtbox_input.Text.Length;
            double capacity = Convert.ToDouble(label_capacity.Text);
            progressBar1.Visible = true;
            label_char.Text = "Characters: " + input + " / " + capacity;

            try
            {
                progressBar1.Value = Convert.ToInt32((input / capacity) * 100);
                _previouslyOverLimit = false;        
            }
            catch (ArgumentOutOfRangeException)
            {
                if (!_previouslyOverLimit)
                {
                    progressBar1.Value = progressBar1.Maximum;
                    _previouslyOverLimit = true;
                    throw;
                }
            }
        }

        private void CapacityBarUpdateNoValidImage()
        {
            progressBar1.Visible = false;
            label_char.Text = "Characters: " + txtbox_input.Text.Length;
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

        private void checkBox_compression_CheckedChanged(object sender, EventArgs e)
        {
            if (CompressionCheckToggle != null)
            {
                CompressionCheckToggle(new BtnEvent());
            }
        }
    }
}
