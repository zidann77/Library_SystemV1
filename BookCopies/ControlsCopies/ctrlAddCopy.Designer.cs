namespace Library.BookCopies.ControlsCopies
{
    partial class ctrlAddCopy
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lbtitle = new System.Windows.Forms.Label();
            this.btnAddCopy = new Guna.UI2.WinForms.Guna2Button();
            this.lbCopiesLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.ErrorImage = global::Library.Properties.Resources.book__1_;
            this.guna2PictureBox1.Image = global::Library.Properties.Resources.book__1_;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(443, 13);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(218, 269);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;
            // 
            // lbtitle
            // 
            this.lbtitle.Font = new System.Drawing.Font("Rockwell", 22F);
            this.lbtitle.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lbtitle.Location = new System.Drawing.Point(22, 44);
            this.lbtitle.Name = "lbtitle";
            this.lbtitle.Size = new System.Drawing.Size(364, 195);
            this.lbtitle.TabIndex = 5;
            this.lbtitle.Text = "label1";
            this.lbtitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAddCopy
            // 
            this.btnAddCopy.AutoRoundedCorners = true;
            this.btnAddCopy.BorderRadius = 20;
            this.btnAddCopy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCopy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCopy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddCopy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddCopy.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAddCopy.Font = new System.Drawing.Font("Rockwell", 18F);
            this.btnAddCopy.ForeColor = System.Drawing.Color.White;
            this.btnAddCopy.Location = new System.Drawing.Point(91, 296);
            this.btnAddCopy.Name = "btnAddCopy";
            this.btnAddCopy.Size = new System.Drawing.Size(230, 42);
            this.btnAddCopy.TabIndex = 16;
            this.btnAddCopy.Text = "Add New Copy";
            this.btnAddCopy.Click += new System.EventHandler(this.btnAddCopy_Click);
            // 
            // lbCopiesLink
            // 
            this.lbCopiesLink.AutoSize = true;
            this.lbCopiesLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbCopiesLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbCopiesLink.Location = new System.Drawing.Point(492, 296);
            this.lbCopiesLink.Name = "lbCopiesLink";
            this.lbCopiesLink.Size = new System.Drawing.Size(123, 20);
            this.lbCopiesLink.TabIndex = 31;
            this.lbCopiesLink.TabStop = true;
            this.lbCopiesLink.Text = "Show All Copies";
            this.lbCopiesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbCopiesLink_LinkClicked);
            // 
            // ctrlAddCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.Controls.Add(this.lbCopiesLink);
            this.Controls.Add(this.btnAddCopy);
            this.Controls.Add(this.lbtitle);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "ctrlAddCopy";
            this.Size = new System.Drawing.Size(677, 363);
            this.Load += new System.EventHandler(this.ctrlBookCopyInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label lbtitle;
        private Guna.UI2.WinForms.Guna2Button btnAddCopy;
        private System.Windows.Forms.LinkLabel lbCopiesLink;
    }
}
