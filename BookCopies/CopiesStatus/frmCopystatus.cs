using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.BookCopies.CopiesStatus
{
    public partial class frmCopystatus : Form
    {
        int CopyID = -1;
        clsBookCopy copy = null;
        public frmCopystatus(int CopyID)
        {
            InitializeComponent();
            this.CopyID = CopyID;

            lbID.Text = CopyID.ToString();
           
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void frmCopystatus_Load(object sender, EventArgs e)
        {
            if(CopyID == -1)
            {

                frmMessage frm = new frmMessage($"No Copy With ID For this Book = {CopyID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                return;
            }

            copy = clsBookCopy.Find(CopyID);

            guna2CheckBox1.Checked = copy.AvailabilityStatus;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Are You Sure ?", frmMessage.enMode.Question);
            frm.ShowDialog();

            if (frm.Result == frmMessage.enResult.Ok)
            {
                copy.AvailabilityStatus = guna2CheckBox1.Checked;

                copy.Save();
            }
            
        }
    }
}
