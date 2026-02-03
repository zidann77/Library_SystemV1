using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.BookCopies.ControlsCopies
{
    public partial class ctrlCopyInfo : UserControl
    {

       public int LateDays { get { return (DateTime.Now - EndDate.Value).Days > 0 ? (DateTime.Now - EndDate.Value).Days : 0; } }

        public ctrlCopyInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(int ID)
        {
            clsBorrowing Borrowing = clsBorrowing.Find(ID);

            if (Borrowing != null)
            {
                lbID.Text = Borrowing.CopyID.ToString();
                lbtitle.Text = Borrowing.CopyInfo.BookInfo.Title;
                StartDate.Value = Borrowing.BorrowingDate;


                EndDate.Value = Borrowing.EndDate;

                lbTotal.Text = (EndDate.Value - StartDate.Value).Days.ToString();
                lbLate.Text = LateDays.ToString();
                guna2PictureBox1.ImageLocation = Borrowing.CopyInfo.BookInfo.Images.Count != 0 ? Borrowing.CopyInfo.BookInfo.Images[0].ImagePath : "";
            }

            else
            {
                frmMessage frm = new frmMessage("No Data Founded ", frmMessage.enMode.Error);
                frm.ShowDialog();
            }

        }

        private void lbtitle_Click(object sender, EventArgs e)
        {

        }

        private void ctrlCopyInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void lbLate_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
