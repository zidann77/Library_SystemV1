using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Properties;
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
    public partial class ctrlReservedCopyInfo : UserControl
    {
        clsReservation reservation = null;
        public ctrlReservedCopyInfo()
        {
            InitializeComponent();
          
        }

       public void LoadInfo(clsReservation reservation)
        {
            

            if (reservation != null)
            {
                clsBookCopy Copy = reservation.CopyInfo;
                lbtitle.Text = Copy.BookInfo.Title;
                lbID.Text = Copy.CopyID.ToString();

                ReserveDate.Value = reservation.ReservationDate;

                switch (reservation.ReservationStatus)
                {
                    case clsReservation.enReservationStatus.New:
                        statusImage.Image = Resources._new;
                        lbStatus.Text = "New"; // أو "جديد"
                        break;

                    case clsReservation.enReservationStatus.Cancelled:
                        statusImage.Image = Resources.icons8_close_48;
                        lbStatus.Text = "Cancelled"; // أو "ملغى"
                        lbStatus.ForeColor = Color.Red;
                        break;

                    case clsReservation.enReservationStatus.Complete:
                        statusImage.Image = Resources.check;
                        lbStatus.Text = "Complete"; // أو "مكتمل"
                        lbStatus.ForeColor = Color.MediumSeaGreen;
                        break;
                }

            }


        }
        private void ctrlReservedCopyInfo_Load(object sender, EventArgs e)
        {
           
        }
    }
}
