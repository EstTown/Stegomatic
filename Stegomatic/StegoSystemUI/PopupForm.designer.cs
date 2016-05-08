namespace StegomaticProject.StegoSystemUI
{
    partial class UserInputPopup
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
            this.btn_popup_submit = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_popuplabel = new System.Windows.Forms.Label();
            this.btn_popup_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_popup_submit
            // 
            this.btn_popup_submit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_popup_submit.Location = new System.Drawing.Point(206, 38);
            this.btn_popup_submit.Name = "btn_popup_submit";
            this.btn_popup_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_popup_submit.TabIndex = 0;
            this.btn_popup_submit.Text = "Submit";
            this.btn_popup_submit.UseVisualStyleBackColor = true;
            this.btn_popup_submit.Click += new System.EventHandler(this.btn_popup_submit_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label_popuplabel
            // 
            this.label_popuplabel.AutoSize = true;
            this.label_popuplabel.Location = new System.Drawing.Point(6, 15);
            this.label_popuplabel.Name = "label_popuplabel";
            this.label_popuplabel.Size = new System.Drawing.Size(28, 13);
            this.label_popuplabel.TabIndex = 2;
            this.label_popuplabel.Text = "label_popuplabel";
            // 
            // btn_popup_cancel
            // 
            this.btn_popup_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_popup_cancel.Location = new System.Drawing.Point(125, 38);
            this.btn_popup_cancel.Name = "btn_popup_cancel";
            this.btn_popup_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_popup_cancel.TabIndex = 3;
            this.btn_popup_cancel.Text = "Cancel";
            this.btn_popup_cancel.UseVisualStyleBackColor = true;
            this.btn_popup_cancel.Click += new System.EventHandler(this.btn_popup_cancel_Click);
            // 
            // UserInputPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(294, 71);
            this.ControlBox = false;
            this.Controls.Add(this.btn_popup_cancel);
            this.Controls.Add(this.label_popuplabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_popup_submit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInputPopup";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UserInputPopup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_popup_submit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_popuplabel;
        private System.Windows.Forms.Button btn_popup_cancel;
    }
}