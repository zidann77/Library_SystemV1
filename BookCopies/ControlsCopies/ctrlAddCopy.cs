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

namespace Library.BookCopies.ControlsCopies
{
    public partial class ctrlAddCopy : UserControl
    {

        public clsBook Book {  get; set; }

        int BookID = -1;
        public ctrlAddCopy()
        {
            InitializeComponent();
        }

        private void ctrlBookCopyInfo_Load(object sender, EventArgs e)
        {

        }

        public void LoadBookInfo(int bookID)
        {
            Book = clsBook.Find(bookID);

            if(Book == null)
            {
                frmMessage frm = new frmMessage($"No Book With ID = {bookID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                return;
            }

            guna2PictureBox1.ImageLocation = Book.Images.Count > 0 ? Book.Images[0].ImagePath : "";
            lbtitle.Text = Book.Title;

            
        }

        private void btnAddCopy_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Are You Sure ?", frmMessage.enMode.Question);
            frm.ShowDialog();

            if (frm.Result == frmMessage.enResult.Ok)
            {
                clsBookCopy copy = new clsBookCopy();

                copy.AvailabilityStatus = true;
                copy.BookID = Book.BookID;
                copy.Reserved = false;

                copy.Save();

                if(copy != null)
                {
                    frmMessage formm = new frmMessage("New Copy added", frmMessage.enMode.Info);
                    formm.ShowDialog();
                }
            }

        }

        private void lbCopiesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmManageBookCopies frm = new frmManageBookCopies(Book.BookID);
            frm.ShowDialog();
        }
    }
}
