namespace StegomaticProject.StegoSystemUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picbox_image = new System.Windows.Forms.PictureBox();
            this.txtbox_input = new System.Windows.Forms.RichTextBox();
            this.label_char = new System.Windows.Forms.Label();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_encode = new System.Windows.Forms.Button();
            this.btn_decode = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.checkBox_encryption = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_filesize = new System.Windows.Forms.Label();
            this.label_width = new System.Windows.Forms.Label();
            this.label_height = new System.Windows.Forms.Label();
            this.label_heighttext = new System.Windows.Forms.Label();
            this.label_widthtext = new System.Windows.Forms.Label();
            this.label_filesizetext = new System.Windows.Forms.Label();
            this.label_capacity = new System.Windows.Forms.Label();
            this.label_about = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_compression = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_image)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picbox_image
            // 
            this.picbox_image.BackColor = System.Drawing.SystemColors.Control;
            this.picbox_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picbox_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbox_image.Image = global::StegomaticProject.Properties.Resources.background;
            this.picbox_image.InitialImage = null;
            this.picbox_image.Location = new System.Drawing.Point(629, 12);
            this.picbox_image.Name = "picbox_image";
            this.picbox_image.Size = new System.Drawing.Size(293, 199);
            this.picbox_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbox_image.TabIndex = 0;
            this.picbox_image.TabStop = false;
            this.picbox_image.Click += new System.EventHandler(this.picbox_image_Click);
            // 
            // txtbox_input
            // 
            this.txtbox_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox_input.Location = new System.Drawing.Point(6, 19);
            this.txtbox_input.Name = "txtbox_input";
            this.txtbox_input.Size = new System.Drawing.Size(599, 341);
            this.txtbox_input.TabIndex = 2;
            this.txtbox_input.Text = "";
            this.txtbox_input.TextChanged += new System.EventHandler(this.txtbox_input_TextChanged);
            // 
            // label_char
            // 
            this.label_char.AutoSize = true;
            this.label_char.Location = new System.Drawing.Point(12, 385);
            this.label_char.Name = "label_char";
            this.label_char.Size = new System.Drawing.Size(70, 13);
            this.label_char.TabIndex = 6;
            this.label_char.Text = "Characters: 0";
            this.label_char.Click += new System.EventHandler(this.label_char_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(629, 326);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(150, 23);
            this.btn_open.TabIndex = 7;
            this.btn_open.Text = "Open image...";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_encode
            // 
            this.btn_encode.Location = new System.Drawing.Point(785, 326);
            this.btn_encode.Name = "btn_encode";
            this.btn_encode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_encode.Size = new System.Drawing.Size(137, 23);
            this.btn_encode.TabIndex = 8;
            this.btn_encode.Text = "Encode";
            this.btn_encode.UseVisualStyleBackColor = true;
            this.btn_encode.Click += new System.EventHandler(this.btn_encode_Click);
            // 
            // btn_decode
            // 
            this.btn_decode.Location = new System.Drawing.Point(785, 355);
            this.btn_decode.Name = "btn_decode";
            this.btn_decode.Size = new System.Drawing.Size(137, 23);
            this.btn_decode.TabIndex = 9;
            this.btn_decode.Text = "Decode";
            this.btn_decode.UseVisualStyleBackColor = true;
            this.btn_decode.Click += new System.EventHandler(this.btn_decode_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(629, 355);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(150, 23);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "Save as...";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // checkBox_encryption
            // 
            this.checkBox_encryption.AutoSize = true;
            this.checkBox_encryption.Location = new System.Drawing.Point(811, 384);
            this.checkBox_encryption.Name = "checkBox_encryption";
            this.checkBox_encryption.Size = new System.Drawing.Size(111, 17);
            this.checkBox_encryption.TabIndex = 11;
            this.checkBox_encryption.Text = "Enable encryption";
            this.checkBox_encryption.UseVisualStyleBackColor = true;
            this.checkBox_encryption.CheckedChanged += new System.EventHandler(this.checkBox_encryption_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.label_about);
            this.panel1.Location = new System.Drawing.Point(629, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 103);
            this.panel1.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.12281F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.87719F));
            this.tableLayoutPanel1.Controls.Add(this.label_filesize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_width, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_height, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_heighttext, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_widthtext, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_filesizetext, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_capacity, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 79);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label_filesize
            // 
            this.label_filesize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_filesize.AutoSize = true;
            this.label_filesize.Location = new System.Drawing.Point(281, 41);
            this.label_filesize.Name = "label_filesize";
            this.label_filesize.Size = new System.Drawing.Size(0, 18);
            this.label_filesize.TabIndex = 5;
            this.label_filesize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_width
            // 
            this.label_width.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_width.AutoSize = true;
            this.label_width.Location = new System.Drawing.Point(281, 21);
            this.label_width.Name = "label_width";
            this.label_width.Size = new System.Drawing.Size(0, 19);
            this.label_width.TabIndex = 4;
            this.label_width.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_height
            // 
            this.label_height.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_height.AutoSize = true;
            this.label_height.Location = new System.Drawing.Point(281, 1);
            this.label_height.Name = "label_height";
            this.label_height.Size = new System.Drawing.Size(0, 19);
            this.label_height.TabIndex = 3;
            this.label_height.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_heighttext
            // 
            this.label_heighttext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_heighttext.AutoSize = true;
            this.label_heighttext.Location = new System.Drawing.Point(4, 1);
            this.label_heighttext.Name = "label_heighttext";
            this.label_heighttext.Size = new System.Drawing.Size(41, 19);
            this.label_heighttext.TabIndex = 0;
            this.label_heighttext.Text = "Height:";
            this.label_heighttext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_widthtext
            // 
            this.label_widthtext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_widthtext.AutoSize = true;
            this.label_widthtext.Location = new System.Drawing.Point(4, 21);
            this.label_widthtext.Name = "label_widthtext";
            this.label_widthtext.Size = new System.Drawing.Size(38, 19);
            this.label_widthtext.TabIndex = 1;
            this.label_widthtext.Text = "Width:";
            this.label_widthtext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_filesizetext
            // 
            this.label_filesizetext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_filesizetext.AutoSize = true;
            this.label_filesizetext.Location = new System.Drawing.Point(4, 41);
            this.label_filesizetext.Name = "label_filesizetext";
            this.label_filesizetext.Size = new System.Drawing.Size(44, 18);
            this.label_filesizetext.TabIndex = 2;
            this.label_filesizetext.Text = "Filesize:";
            this.label_filesizetext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_capacity
            // 
            this.label_capacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_capacity.AutoSize = true;
            this.label_capacity.Location = new System.Drawing.Point(4, 60);
            this.label_capacity.Name = "label_capacity";
            this.label_capacity.Size = new System.Drawing.Size(71, 18);
            this.label_capacity.TabIndex = 6;
            this.label_capacity.Text = "Est. capacity:";
            this.label_capacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_about
            // 
            this.label_about.AutoSize = true;
            this.label_about.Location = new System.Drawing.Point(3, 4);
            this.label_about.Name = "label_about";
            this.label_about.Size = new System.Drawing.Size(69, 13);
            this.label_about.TabIndex = 0;
            this.label_about.Text = "About image:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtbox_input);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 366);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insert text";
            // 
            // checkBox_compression
            // 
            this.checkBox_compression.AutoSize = true;
            this.checkBox_compression.Location = new System.Drawing.Point(629, 384);
            this.checkBox_compression.Name = "checkBox_compression";
            this.checkBox_compression.Size = new System.Drawing.Size(124, 17);
            this.checkBox_compression.TabIndex = 16;
            this.checkBox_compression.Text = "Enable  compression";
            this.checkBox_compression.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 411);
            this.Controls.Add(this.checkBox_compression);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_encryption);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_decode);
            this.Controls.Add(this.btn_encode);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.label_char);
            this.Controls.Add(this.picbox_image);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Steg-o-matic 1000";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_image)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picbox_image;
        private System.Windows.Forms.RichTextBox txtbox_input;
        private System.Windows.Forms.Label label_char;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_encode;
        private System.Windows.Forms.Button btn_decode;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox checkBox_encryption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_about;
        private System.Windows.Forms.Label label_heighttext;
        private System.Windows.Forms.Label label_widthtext;
        private System.Windows.Forms.Label label_filesizetext;
        private System.Windows.Forms.Label label_filesize;
        private System.Windows.Forms.Label label_width;
        private System.Windows.Forms.Label label_height;
        private System.Windows.Forms.Label label_capacity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_compression;
    }
}

