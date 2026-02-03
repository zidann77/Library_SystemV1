namespace Library.BookCopies
{
    partial class frmReservedCopy
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
            this.ctrlReservedCopyInfo1 = new Library.BookCopies.ControlsCopies.ctrlReservedCopyInfo();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.guna2ImageButton1);
            this.guna2Panel1.Location = new System.Drawing.Point(-20, -3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(730, 78);
            this.guna2Panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 25F);
            this.label2.ForeColor = System.Drawing.Color.Linen;
            this.label2.Location = new System.Drawing.Point(230, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 38);
            this.label2.TabIndex = 13;
            this.label2.Text = "Reserved Copy";
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Image = global::Library.Properties.Resources.icons8_close_48;
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.Location = new System.Drawing.Point(665, 21);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.Image = global::Library.Properties.Resources.icons8_close_48;
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Size = new System.Drawing.Size(42, 42);
            this.guna2ImageButton1.TabIndex = 12;
            this.guna2ImageButton1.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // ctrlReservedCopyInfo1
            // 
            this.ctrlReservedCopyInfo1.Location = new System.Drawing.Point(12, 94);
            this.ctrlReservedCopyInfo1.Name = "ctrlReservedCopyInfo1";
            this.ctrlReservedCopyInfo1.Size = new System.Drawing.Size(668, 410);
            this.ctrlReservedCopyInfo1.TabIndex = 3;
            // 
            // frmReservedCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(699, 516);
            this.Controls.Add(this.ctrlReservedCopyInfo1);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReservedCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReservedCopy";
            this.Load += new System.EventHandler(this.frmReservedCopy_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private ControlsCopies.ctrlReservedCopyInfo ctrlReservedCopyInfo1;
    }
}