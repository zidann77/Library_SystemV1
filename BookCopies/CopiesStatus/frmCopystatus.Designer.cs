namespace Library.BookCopies.CopiesStatus
{
    partial class frmCopystatus
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.lbfilter = new System.Windows.Forms.Label();
            this.guna2CheckBox1 = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnOK = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.guna2ImageButton1);
            this.guna2Panel1.Location = new System.Drawing.Point(-20, -25);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1060, 92);
            this.guna2Panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 25F);
            this.label2.ForeColor = System.Drawing.Color.Linen;
            this.label2.Location = new System.Drawing.Point(191, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 38);
            this.label2.TabIndex = 13;
            this.label2.Text = "Update Status";
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Image = global::Library.Properties.Resources.icons8_close_48;
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.Location = new System.Drawing.Point(529, 37);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.Image = global::Library.Properties.Resources.icons8_close_48;
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Size = new System.Drawing.Size(36, 42);
            this.guna2ImageButton1.TabIndex = 12;
            this.guna2ImageButton1.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // lbfilter
            // 
            this.lbfilter.AutoSize = true;
            this.lbfilter.Font = new System.Drawing.Font("Rockwell", 20F);
            this.lbfilter.Location = new System.Drawing.Point(124, 135);
            this.lbfilter.Name = "lbfilter";
            this.lbfilter.Size = new System.Drawing.Size(159, 31);
            this.lbfilter.TabIndex = 18;
            this.lbfilter.Text = "For Now Is :";
            // 
            // guna2CheckBox1
            // 
            this.guna2CheckBox1.AutoSize = true;
            this.guna2CheckBox1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox1.CheckedState.BorderRadius = 0;
            this.guna2CheckBox1.CheckedState.BorderThickness = 0;
            this.guna2CheckBox1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox1.Font = new System.Drawing.Font("Rockwell", 20F);
            this.guna2CheckBox1.Location = new System.Drawing.Point(289, 135);
            this.guna2CheckBox1.Name = "guna2CheckBox1";
            this.guna2CheckBox1.Size = new System.Drawing.Size(151, 35);
            this.guna2CheckBox1.TabIndex = 20;
            this.guna2CheckBox1.Text = "Available";
            this.guna2CheckBox1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox1.UncheckedState.BorderRadius = 0;
            this.guna2CheckBox1.UncheckedState.BorderThickness = 0;
            this.guna2CheckBox1.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox1.CheckedChanged += new System.EventHandler(this.guna2CheckBox1_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.AutoRoundedCorners = true;
            this.btnOK.BorderRadius = 21;
            this.btnOK.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOK.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOK.Enabled = false;
            this.btnOK.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOK.Font = new System.Drawing.Font("Rockwell", 18F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(221, 189);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(121, 44);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 20F);
            this.label1.Location = new System.Drawing.Point(71, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 31);
            this.label1.TabIndex = 22;
            this.label1.Text = "This Copy With ID = ";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Font = new System.Drawing.Font("Rockwell", 20F);
            this.lbID.Location = new System.Drawing.Point(348, 84);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(29, 31);
            this.lbID.TabIndex = 23;
            this.lbID.Text = "1";
            // 
            // frmCopystatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(557, 245);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.guna2CheckBox1);
            this.Controls.Add(this.lbfilter);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCopystatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCopystatus";
            this.Load += new System.EventHandler(this.frmCopystatus_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private System.Windows.Forms.Label lbfilter;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox1;
        private Guna.UI2.WinForms.Guna2Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbID;
    }
}