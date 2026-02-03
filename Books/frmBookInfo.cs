using BusinessLogicLayer;
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

namespace Library.Books.controls
{
    public partial class frmBookInfo : Form
    {
        int BookID = -1;
        public frmBookInfo(int ID)
        {
            InitializeComponent();
            BookID = ID;
        }


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBookInfo_Load(object sender, EventArgs e)
        {
            if (clsBook.Exists(BookID))
            {
                ctrlBookInfo1.LoadInfo(BookID);
            }
            else
            {
                this.Close();
                frmMessage frm = new frmMessage("This ID does not exist!", frmMessage.enMode.Error);
                frm.ShowDialog();
            }
            
        }

        private void ctrlBookInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
