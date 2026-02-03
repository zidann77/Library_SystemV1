using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Borrowings.Forms
{
    public partial class frmBorrowedCopy : Form
    {
        int BorrowingID = -1;

        public frmBorrowedCopy(int ID)
        {
            InitializeComponent();
            BorrowingID = ID;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            ctrlCopyInfo1.LoadInfo(BorrowingID);
        }
    }
}
