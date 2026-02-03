using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.People
{
    public partial class frmSearchPerson : Form
    {

        // =========================
        // Events
        // =========================
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        public frmSearchPerson()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(guna2TextBox1.Text);
            ctrlPersonDetails1.LoadInfo(ID);
            DataBack?.Invoke(this, ID);
            btnOK.Enabled = true;
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == (char)Keys.Enter) 
               btnSearch.PerformClick();
            

            // Allow digits + control keys (Backspace, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void frmSearchPerson_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Focus();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           guna2ImageButton1.PerformClick();
        }

        private void btnOK_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
