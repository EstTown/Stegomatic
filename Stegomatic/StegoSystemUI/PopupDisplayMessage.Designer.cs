namespace StegomaticProject.StegoSystemUI
{
    partial class PopupDisplayMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupDisplayMessage));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.txtbox_display = new System.Windows.Forms.RichTextBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox.Controls.Add(this.txtbox_display);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(611, 387);
            this.groupBox.TabIndex = 16;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Decoded message";
            // 
            // txtbox_display
            // 
            this.txtbox_display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbox_display.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtbox_display.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox_display.Location = new System.Drawing.Point(6, 19);
            this.txtbox_display.Name = "txtbox_display";
            this.txtbox_display.ReadOnly = true;
            this.txtbox_display.Size = new System.Drawing.Size(599, 362);
            this.txtbox_display.TabIndex = 2;
            this.txtbox_display.Text = "";
            // 
            // PopupDisplayMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PopupDisplayMessage";
            this.Text = "Decoded";
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.RichTextBox txtbox_display;
    }
}