namespace Library.BookCopies.ControlsCopies
{
    partial class ctrlReservedCopyInfo
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
            this.ReserveDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbtitle = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReserveDate
            // 
            this.ReserveDate.BorderRadius = 18;
            this.ReserveDate.Checked = true;
            this.ReserveDate.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.ReserveDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ReserveDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.ReserveDate.Location = new System.Drawing.Point(279, 308);
            this.ReserveDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.ReserveDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.ReserveDate.Name = "ReserveDate";
            this.ReserveDate.Size = new System.Drawing.Size(363, 38);
            this.ReserveDate.TabIndex = 62;
            this.ReserveDate.Value = new System.DateTime(2025, 11, 8, 9, 42, 12, 549);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 18F);
            this.label2.Location = new System.Drawing.Point(85, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 27);
            this.label2.TabIndex = 60;
            this.label2.Text = "Reserved";
            // 
            // lbID
            // 
            this.lbID.Font = new System.Drawing.Font("Rockwell", 18F);
            this.lbID.Location = new System.Drawing.Point(274, 267);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(255, 27);
            this.lbID.TabIndex = 58;
            this.lbID.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 18F);
            this.label1.Location = new System.Drawing.Point(99, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 27);
            this.label1.TabIndex = 57;
            this.label1.Text = "Copy ID";
            // 
            // lbtitle
            // 
            this.lbtitle.Font = new System.Drawing.Font("Rockwell", 22F);
            this.lbtitle.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lbtitle.Location = new System.Drawing.Point(17, 19);
            this.lbtitle.Name = "lbtitle";
            this.lbtitle.Size = new System.Drawing.Size(364, 195);
            this.lbtitle.TabIndex = 56;
            this.lbtitle.Text = "label1";
            this.lbtitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.Font = new System.Drawing.Font("Rockwell", 18F);
            this.lbStatus.Location = new System.Drawing.Point(274, 366);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(255, 27);
            this.lbStatus.TabIndex = 64;
            this.lbStatus.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 18F);
            this.label4.Location = new System.Drawing.Point(120, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 27);
            this.label4.TabIndex = 63;
            this.label4.Text = "Status ";
            // 
            // statusImage
            // 
            this.statusImage.FillColor = System.Drawing.Color.Transparent;
            this.statusImage.Image = global::Library.Properties.Resources.check;
            this.statusImage.ImageRotate = 0F;
            this.statusImage.Location = new System.Drawing.Point(209, 356);
            this.statusImage.Name = "statusImage";
            this.statusImage.Size = new System.Drawing.Size(46, 37);
            this.statusImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.statusImage.TabIndex = 65;
            this.statusImage.TabStop = false;
            // 
            // guna2PictureBox4
            // 
            this.guna2PictureBox4.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox4.Image = global::Library.Properties.Resources.calendar;
            this.guna2PictureBox4.ImageRotate = 0F;
            this.guna2PictureBox4.Location = new System.Drawing.Point(209, 308);
            this.guna2PictureBox4.Name = "guna2PictureBox4";
            this.guna2PictureBox4.Size = new System.Drawing.Size(46, 38);
            this.guna2PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox4.TabIndex = 61;
            this.guna2PictureBox4.TabStop = false;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox2.Image = global::Library.Properties.Resources.book__3_;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(209, 257);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(46, 37);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 59;
            this.guna2PictureBox2.TabStop = false;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::Library.Properties.Resources.book__1_;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(395, 10);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(253, 231);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 55;
            this.guna2PictureBox1.TabStop = false;
            // 
            // ctrlReservedCopyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusImage);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReserveDate);
            this.Controls.Add(this.guna2PictureBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guna2PictureBox2);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbtitle);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "ctrlReservedCopyInfo";
            this.Size = new System.Drawing.Size(668, 410);
            this.Load += new System.EventHandler(this.ctrlReservedCopyInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker ReserveDate;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbtitle;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox statusImage;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label4;
    }
}
