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
using static Guna.UI2.Native.WinApi;

namespace Library.BookCopies
{
    public partial class frmReservedCopy : Form
    {
        clsReservation Reservation;
        public frmReservedCopy(int ID)
        {
            InitializeComponent();
            Reservation = clsReservation.Find(ID);


        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReservedCopy_Load(object sender, EventArgs e)
        {
            if(Reservation != null)
            {
                ctrlReservedCopyInfo1.LoadInfo(Reservation);

            }
            else
            {
                frmMessage frmm = new frmMessage($"No Data Founded", frmMessage.enMode.Error);
                frmm.ShowDialog();
                this.Close();
            }

        }
    }
}
