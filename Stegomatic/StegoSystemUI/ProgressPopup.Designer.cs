namespace StegomaticProject.StegoSystemUI
{
    partial class ProgressPopup
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_cancelProgress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(442, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // btn_cancelProgress
            // 
            this.btn_cancelProgress.Location = new System.Drawing.Point(379, 47);
            this.btn_cancelProgress.Name = "btn_cancelProgress";
            this.btn_cancelProgress.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelProgress.TabIndex = 1;
            this.btn_cancelProgress.Text = "Abort";
            this.btn_cancelProgress.UseVisualStyleBackColor = true;
            // 
            // ProgressPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 82);
            this.Controls.Add(this.btn_cancelProgress);
            this.Controls.Add(this.progressBar1);
            this.Name = "ProgressPopup";
            this.Text = "Encoding...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_cancelProgress;
    }
}