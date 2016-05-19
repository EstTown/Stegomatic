using StegomaticProject.StegoSystemUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using StegomaticProject.StegoSystemUI.Events;
using StegomaticProject.StegoSystemUI;
using StegomaticProject.CustomExceptions;
using System.IO;
using System.Drawing.Imaging;

namespace StegomaticProject.StegoSystemUI
{
    public class StegoSystemWinForm : IStegoSystemUI
    {
        private Form1 _mainMenu { get; set; } // LAV ET INTERFACE HERTIL, DOG FØRST TIL SIDST.

        public Func<int, int, bool, int> ImageCapacityCalculator { get; set; }

        public string Message
        {
            get { return _mainMenu.EnteredText; }
            private set { _mainMenu.EnteredText = value; }
        }

        public string PathOfCoverImage
        {
            get { return _mainMenu.picbox_image.ImageLocation; }
            private set { _mainMenu.picbox_image.ImageLocation = value; }
        }

        public bool Enable
        {
            get { return _mainMenu.Enabled; }
            set { _mainMenu.Enabled = value; }
        }

        public Bitmap DisplayImage
        {
            get { return ImageToBitmap(_mainMenu.picbox_image.Image) ; }
            private set { _mainMenu.picbox_image.Image = value; }
        }

        public IConfig Config
        {
            get { return new ModelConfiguration(_mainMenu.EncryptChecked, _mainMenu.CompressChecked); }
        }

        public event DisplayNotificationEventHandler NotifyUser; 
        public event BtnEventHandler DecodeBtn;
        public event BtnEventHandler EncodeBtn;
        public event BtnEventHandler OpenImageBtn;

        public StegoSystemWinForm()
        {
            //Creates new WinForms-window of Form1
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainMenu = new Form1();
            ImageCapacityCalculator = StandardCapacityCalculator;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _mainMenu.NotifyUser += new DisplayNotificationEventHandler(this.ShowNotification);
            _mainMenu.EncodeBtnClick += new BtnEventHandler(this.EncodeBtnClick);
            _mainMenu.DecodeBtnClick += new BtnEventHandler(this.DecodeBtnClick);
            _mainMenu.OpenImageBtnClick += new BtnEventHandler(this.OpenImageBtnClick);
            _mainMenu.CompressionCheckToggle += new BtnEventHandler(this.ForceUpdateImageCapacity);
        }

        private void OpenImageBtnClick(BtnEvent e)
        {
            if (OpenImageBtn != null)
            {
                OpenImageBtn(new BtnEvent());
            }
        }

        private void DecodeBtnClick(BtnEvent e)
        {
            if (DecodeBtn != null)
            {
                DecodeBtn(new BtnEvent());
            }
        }

        private void EncodeBtnClick(BtnEvent e)
        {
            if (EncodeBtn != null)
            {
                EncodeBtn(new BtnEvent());
            }
        }

        public void SetDisplayImage(Bitmap newImage)
        {
            DisplayImage = newImage;
        }

        public void ShowNotification(DisplayNotificationEvent e)
        {
            ShowNotification(e.Notification, e.Title);
        }

        public void ShowNotification(string notification, string title = "")
        {
            // Initialize a popup window and show the message!
            MessageBox.Show(notification, title);

            //NotificationWindow userNotificationWindow = new NotificationWindow();
            //userNotificationWindow.Text = title;
            //userNotificationWindow.LabelText = notification;
            //userNotificationWindow.ShowDialog();
        }

        public void ShowMessageNotification(string notification, string text, string title = "")
        {
            // Initialize a popup window and show the message!
            //MessageBox.Show(notification, title);

            PopupDisplayMessage popup = new PopupDisplayMessage();
            popup.Text = title;
            popup.groupBox.Text = notification;
            popup.txtbox_display.Text = text;
            popup.ShowDialog();

            //NotificationWindow userNotificationWindow = new NotificationWindow();
            //userNotificationWindow.Text = title;
            //userNotificationWindow.LabelText = notification;
            //userNotificationWindow.ShowDialog();
        }

        public void Start()
        {
            Application.Run(_mainMenu);
        }

        public void Terminate()
        {
            _mainMenu.Dispose();
        }

        public string GetEncryptionKey()
        {
            try
            {
                return GetUserStringPopup("Encryption key", "Key:");
            }
            catch (AbortActionException)
            {
                throw;
            }
        }

        public string GetStegoSeed()
        {
            try
            {
                return GetUserStringPopup("Set password", "Password:");
            }
            catch (AbortActionException)
            {
                throw;
            }
        }

        private string GetUserStringPopup(string popupTitle, string popupTextBoxTitle)
        {
            // Create a popup window and return the entered string. 

            UserInputPopup popupWindow = new UserInputPopup();
            popupWindow.Text = popupTitle;
            popupWindow.TextBoxTitle = popupTextBoxTitle;
            string userInput = string.Empty;

            DialogResult userResponse = popupWindow.ShowDialog();
            if (userResponse == DialogResult.OK)
            {
                userInput = popupWindow.TextContents;
            }
            else
            {
                throw new AbortActionException();
            }

            popupWindow.Close();
            popupWindow.Dispose();
            return userInput;
        }

        private Bitmap ImageToBitmap(Image image)
        {
            // Surpresses exceptions as data verification is not to be done in this class.

            Bitmap bitmapImage;
            try
            {
                bitmapImage = new Bitmap(image);
            }
            catch (Exception)
            {
                return null;
            }
            return bitmapImage;
        }

        public void OpenImage()
        {
            Stream stream = null;

            // Define dialog-object
            OpenFileDialog openFileWindow = new OpenFileDialog();
            openFileWindow.Title = "Select an image";
            openFileWindow.DefaultExt = ".png";
            openFileWindow.Filter = "PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|" +
                                    "JPEG Files (*.jpg)|*.jpg|TIFF Files (*.tif)|*.tiff";

            if (openFileWindow.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openFileWindow.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            // Read image here
                            Image image = Image.FromStream(stream);
                            string filename = openFileWindow.FileName;

                            // Display image
                            this.SetDisplayImage(ImageToBitmap(image));
                            DisplayImageInfo(image, filename);
                        }
                    }
                }
                catch (NotifyUserException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NotifyUserException("Could not read file. Original error: " + e.Message, "Error");
                }
            }
        }

        private void DisplayImageInfo(Image image, string filename)
        {
            // Get image info
            string[] imageinfo = ImageData.GetImageInfo(image, filename);

            // Set labels to imageinfo
            _mainMenu.ImageDescriptionAbout = "About image: " + imageinfo[3];
            _mainMenu.ImageDescriptionWidth = imageinfo[0];
            _mainMenu.ImageDescriptionHeight = imageinfo[1];
            _mainMenu.ImageDescriptionFilesize = imageinfo[2] + " Bytes";
            _mainMenu.ImageDescriptionCapacity = Convert.ToString(ImageCapacityCalculator(image.Width, image.Height, _mainMenu.CompressChecked));

            _mainMenu.ForceUpdateCapacityBar();
        }

        private void ForceUpdateImageCapacity(BtnEvent e)
        {
            if (DisplayImage != null)
            {
                _mainMenu.ImageDescriptionCapacity = Convert.ToString(
                    ImageCapacityCalculator(Convert.ToInt32(_mainMenu.ImageDescriptionWidth), Convert.ToInt32(_mainMenu.ImageDescriptionHeight), _mainMenu.CompressChecked));
                _mainMenu.ForceUpdateCapacityBar();
            }
        }

        public void SaveImage(Bitmap file)
        {
            SaveFileDialog saveFileWindow = new SaveFileDialog();

            //Image to be saved, goes here
            //Image should be handled by an outside non-form class

            saveFileWindow.Title = "Save image as...";
            saveFileWindow.DefaultExt = ".png";
            saveFileWindow.Filter = "PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
            DialogResult userResponse = saveFileWindow.ShowDialog();

            if (saveFileWindow.FileName != "")
            {
                //Filestream is saved here, from manipulated image.
                //Switch determines which format the image will be saved in.

                switch (saveFileWindow.FilterIndex)
                {
                    case 1:
                        file.Save(saveFileWindow.FileName, ImageFormat.Png);
                        break;
                    case 2:
                        file.Save(saveFileWindow.FileName, ImageFormat.Bmp);
                        break;
                }
            }

            if (userResponse != DialogResult.OK)
            {
                throw new NotifyUserException("Image not modified.", "Action cancelled");
            }
            else
            {
                DisplayImageInfo(file, saveFileWindow.FileName);
            }
        }

        private int StandardCapacityCalculator(int width, int height, bool compress)
        {
            return Convert.ToInt32((height * width * 0.18) / 12);
        }
    }
}