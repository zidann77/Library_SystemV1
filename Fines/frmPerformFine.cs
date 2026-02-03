using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Global_Classes;
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
using static Guna.UI2.Native.WinApi;

namespace Library.Fines
{
    public partial class frmPerformFine : Form
    {
        int BorrowingID = -1;
        int Total = 0;
        clsBorrowing borrowing = null;

        public class PerformFineResultEventArgs : EventArgs
        {
            public bool IsSuccess { get; set; }
            public int FineID { get; set; }

            public PerformFineResultEventArgs(bool success, int fineId)
            {
                IsSuccess = success;
                FineID = fineId;
            }
        }

        public event EventHandler<PerformFineResultEventArgs> FineCompleted;



        public frmPerformFine(int borrowingID)
        {
            InitializeComponent();
            BorrowingID = borrowingID;
            borrowing = new clsBorrowing(); 
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPerformFine_Load(object sender, EventArgs e)
        {
            borrowing = clsBorrowing.Find(BorrowingID);
            if (borrowing == null)
            {
                frmMessage frm = new frmMessage($"There is No Borrowing with {BorrowingID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                this.Close();
                return; 
            }
            rbcash.Checked = true;
            ctrlCopyInfo1.LoadInfo(BorrowingID);
             Total = ctrlCopyInfo1.LateDays * clsSettings.getSettings().DefualtFinePerDays;
            lbtotal.Text = "$ " + Total.ToString();

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            clsFine Fine = new clsFine();
            Fine.ByUser = clsGlobal.CurrentUser.UserID;
            Fine.Detailes = guna2TextBox1.Text.Trim();
            Fine.NumberOfLateDays = (short)ctrlCopyInfo1.LateDays;
            Fine.PaymentWay = rbcash.Checked;
            Fine.PaymentStatus = true;
            Fine.FineAmount = Total;
            Fine.BorrowingRecordID = BorrowingID;

            borrowing.ActualReturnDate = DateTime.Now;
          
            Fine.BorrowingRecordID = borrowing.ID;

            if (Fine.Save() && borrowing.Save() )
            {
                FineCompleted?.Invoke(this, new PerformFineResultEventArgs(true, Fine.FineID));

                frmMessage frm = new frmMessage($"Fine payment completed successfully. with Fine ID = {Fine.FineID}", frmMessage.enMode.PaymentDone);
                frm.ShowDialog();
                this.Close();
                
            }
            else
            {
                FineCompleted?.Invoke(this, new PerformFineResultEventArgs(false, -1));

                frmMessage frm = new frmMessage("Payment failed. Please try again.", frmMessage.enMode.PaymentFailed);
                frm.ShowDialog();
            }

        }
    }
}
