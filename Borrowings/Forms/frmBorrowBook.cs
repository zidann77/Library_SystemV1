using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Global_Classes;
using Library.Message;
using Library.People;
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
    public partial class frmBorrowBook : Form
    {
        int BookID = -1;
        int CopyID = -1;
        clsPeople person;
        clsBook Book;
        clsBookCopy copy;

        public frmBorrowBook(int bookID)
        {
            InitializeComponent();
            BookID = bookID;
            person = new clsPeople();
            copy = new clsBookCopy();
        }

        public frmBorrowBook(int bookID , int copyID)
        {
            InitializeComponent();
            BookID = bookID;
            CopyID =  copyID;
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

        private void btnSave_Click(object sender, EventArgs e)
        {

            frmMessage frm = new frmMessage($"You Want Borrow {Book.Title}", frmMessage.enMode.Error);
            frm.ShowDialog();

            if(frm.Result == frmMessage.enResult.Ok)
            {
                if(copy == null )
                 copy = clsBookCopy.FindAvailableCopyForBorrowing(Book.BookID);
                 if(!copy.IsAvailable)
                    copy = clsBookCopy.FindAvailableCopyForBorrowing(Book.BookID);

                if (copy != null)
                {
                    if (copy.Reserved ?? false)
                    {
                        clsReservation  Reservation  = clsReservation.FindByBookCopy(copy.CopyID);
                        if(Reservation != null && Reservation.PersonID != person.Id)
                        {
                            frmMessage frm1 = new frmMessage($"This Copy with id {copy.CopyID} for {Book.Title} Reserved For Another Person", frmMessage.enMode.Info);
                            frm1.ShowDialog();
                           
                        }
                    }
                    clsBorrowing Borrowing = new clsBorrowing();
                    Borrowing.PersonID = person.Id;
                    Borrowing.BorrowingDate = DateTime.Now;
                    Borrowing.EndDate = DateTime.Now.AddDays(clsSettings.getSettings().DefualtBorrrowDays);
                    Borrowing.CopyID = copy.CopyID;
                    Borrowing.CopyInfo = copy;
                    Borrowing.ByUser = clsGlobal.CurrentUser.UserID;
                    Borrowing.Detailes = guna2TextBox1.Text;

                    copy.AvailabilityStatus = false;
                    copy.Reserved = false;  

                    if (Borrowing.Save() && copy.Save())

                    {
                        frmMessage frm1 = new frmMessage($"You Borrow {Book.Title} successfully", frmMessage.enMode.Info);
                        frm1.ShowDialog();
                       
                    }
                }
                else
                {
                    frmMessage frmm = new frmMessage($"No Available Copy For {Book.Title}", frmMessage.enMode.Error);
                    frmm.ShowDialog();
                   
                }
                
            }
        }

        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            Book = clsBook.Find(BookID);

            if(Book == null)
            {
                frmMessage frm = new frmMessage($"No Book With ID = {BookID}", frmMessage.enMode.Error);
                frm.ShowDialog();
                btnClose.PerformClick();
            }

            ctrlBookInfo1.LoadInfo(BookID);
            

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmSearchPerson frmSearchPerson = new frmSearchPerson();
            frmSearchPerson.DataBack += GetPersonID;
            frmSearchPerson.ShowDialog();
        }
    }
}
