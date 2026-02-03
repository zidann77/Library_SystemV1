namespace Library.Books.controls
{
    partial class ctrlAddUpdateBookImg
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDeleteImg = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddImg = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDeleteImg
            // 
            this.btnDeleteImg.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteImg.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteImg.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteImg.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteImg.FillColor = System.Drawing.Color.Transparent;
            this.btnDeleteImg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeleteImg.ForeColor = System.Drawing.Color.White;
            this.btnDeleteImg.Image = global::Library.Properties.Resources.icons8_delete_96;
            this.btnDeleteImg.ImageSize = new System.Drawing.Size(30, 30);
            this.btnDeleteImg.Location = new System.Drawing.Point(88, 253);
            this.btnDeleteImg.Name = "btnDeleteImg";
            this.btnDeleteImg.Size = new System.Drawing.Size(48, 37);
            this.btnDeleteImg.TabIndex = 2;
            this.btnDeleteImg.Click += new System.EventHandler(this.btnDeleteImg_Click);
            // 
            // btnAddImg
            // 
            this.btnAddImg.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImg.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAddImg.BorderThickness = 2;
            this.btnAddImg.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddImg.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddImg.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddImg.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddImg.FillColor = System.Drawing.Color.Transparent;
            this.btnAddImg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddImg.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddImg.Image = global::Library.Properties.Resources.plus;
            this.btnAddImg.Location = new System.Drawing.Point(89, 102);
            this.btnAddImg.Name = "btnAddImg";
            this.btnAddImg.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnAddImg.Size = new System.Drawing.Size(47, 45);
            this.btnAddImg.TabIndex = 1;
            this.btnAddImg.UseTransparentBackground = true;
            this.btnAddImg.Click += new System.EventHandler(this.btnAddImg_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.ErrorImage = global::Library.Properties.Resources.book__1_1;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(3, 3);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(216, 244);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;
            // 
            // ctrlAddUpdateBookImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnDeleteImg);
            this.Controls.Add(this.btnAddImg);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "ctrlAddUpdateBookImg";
            this.Size = new System.Drawing.Size(221, 291);
            this.Load += new System.EventHandler(this.ctrlAddUpdateBookImg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2CircleButton btnAddImg;
        private Guna.UI2.WinForms.Guna2Button btnDeleteImg;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
