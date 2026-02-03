using BusinessLogicLayer;
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
using static Guna.UI2.Native.WinApi;

namespace Library.BookCopies
{
    public partial class frmAddCopy : Form
    {
        int bookID = -1;

        public frmAddCopy(int id)
        {
            InitializeComponent();
            this.bookID = id;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddCopy_Load(object sender, EventArgs e)
        {
           clsBook Book = clsBook.Find(bookID);

            if (Book == null)
            {
                frmMessage frm = new frmMessage($"No Book With ID = {bookID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                return;
            }
            else
          ctrlAddCopy1.LoadBookInfo(bookID);
        }
    }
}
