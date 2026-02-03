using BusinessLogicLayer.BusinessLogicLayer;
using BusinessLogicLayer;
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
using Library.People;

namespace Library.Reservations
{
    public partial class frmReserveBook : Form
    {
        int BookID = -1;
        int CopyID = -1;
        clsPeople person;
        clsBook Book;
        clsBookCopy copy;

        public frmReserveBook(int bookID)
        {

            InitializeComponent();
            BookID = bookID;
          
        }

       

        public frmReserveBook(int bookID, int copyID)
        {
            InitializeComponent();
            BookID = bookID;
            CopyID = copyID;
            copy = clsBookCopy.Find(copyID);
            person = new clsPeople();
        }



        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void GetPersonID(object sender, int ID)
        {
            person = clsPeople.Find(ID);
            if (person == null)
                return;

            btnBorrow.Enabled = true;
        }

       

        private void btnReverse_Click(object sender, EventArgs e)
        {

            frmMessage frm = new frmMessage($"You Want Reverse {Book.Title}", frmMessage.enMode.Error);
            frm.ShowDialog();

            if (frm.Result == frmMessage.enResult.Ok)
            {
                
                if (copy == null)
                    copy = clsBookCopy.FindAvailableCopyForReservation(Book.BookID);

                else  if (copy.Reserved ?? false)
                {
                    frmMessage frm1 = new frmMessage($"Copy Reserved with Id {copy.CopyID} \nWe Will Choose Any Copy , And if There is Any Available Copy , You Should Borrow it", frmMessage.enMode.Error);
                    frm1.ShowDialog();
                    copy = clsBookCopy.FindAvailableCopyForReservation(Book.BookID);
                }


                if (copy != null)
                {
                   clsReservation Reservation = new clsReservation();

                    Reservation.ReservationStatus = clsReservation.enReservationStatus.New;
                    Reservation.CopyInfo = copy;
                    Reservation.CopyID = copy.CopyID;
                    Reservation.ByUser = clsGlobal.CurrentUser.UserID;
                    Reservation.PersonID = person.Id;
                    Reservation.Person = person;
                    Reservation.ReservationDate = DateTime.Now;
                    Reservation.Detailes = guna2TextBox1.Text.Trim();
                  
                    copy.Reserved = true;

                    if (Reservation.Save() && copy.Save())

                    {
                        frmMessage frm1 = new frmMessage($"You Reserve {Book.Title} successfully and Your Copy ID {copy.CopyID}", frmMessage.enMode.Info);
                        frm1.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        frmMessage frm1 = new frmMessage($"Failed to Save , Tyy Again", frmMessage.enMode.Error);
                        frm1.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    frmMessage frmm = new frmMessage($"No Available Copies For Reservation , if There Available Copy For Borrowing , you should go borrow it ", frmMessage.enMode.Error);
                    frmm.ShowDialog();
                }

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           frmSearchPerson frmSearchPerson1 = new frmSearchPerson();
            frmSearchPerson1.DataBack += GetPersonID;
            frmSearchPerson1.ShowDialog();
        }

        private void frmReserveBook_Load(object sender, EventArgs e)
        {
            Book = clsBook.Find(BookID);

            if (Book == null)
            {
                frmMessage frm = new frmMessage($"No Book With ID = {BookID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                this.Close();
            }

            ctrlBookInfo1.LoadInfo(BookID);
        }
    }
}
