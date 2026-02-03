using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
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
    public partial class ctrlBookInfo : UserControl
    {
        clsBook Book;
        public ctrlBookInfo()
        {
            InitializeComponent();
        }

        void _HandleImages()
        {
            btnRight.Enabled = false;
            btnLeft.Enabled = false;
            if (Book.Images != null && Book.Images.Count>0)
            {

                pbimages.ImageLocation = Book.Images[0].ImagePath;

                if (Book.Images.Count > 1)
                    btnRight.Enabled = true;

            }
            else
            {
                btnRight.Visible  = true;
                btnLeft. Visible  = true;
            }

        }

        public void LoadInfo(int bookId)
        {
            Book = clsBook.Find(bookId);

            if (Book != null)
            {
                lbtitle.Text = Book.Title;
                ISBN.Text = Book.ISBN;
                AvaCopy.Text = (clsBookCopy.getCountAvailableCopies(Book.BookID) + " / " + clsBookCopy.getCountAllCopies(Book.BookID)).ToString();
                PublicationDate.Text = Book.PublicationDate.ToString("d");
                _HandleImages();

                if (Book.HasDetails)
                {
                    txtDetails.Text = Book.Detailes;
                }
                else
                {
                    txtDetails.Visible = false;
                    lbdetails.Visible = false;
                }

   
            }
        }

        int Counter = 0;

        void HandleMoveButtonsAvailabilty()
        {

            if (Counter == Book.Images.Count-1)
                btnRight.Enabled = false;
            else
                btnRight.Enabled = true;

            if (Counter == 0)
                btnLeft.Enabled = false;
            else
                btnLeft.Enabled = true;

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
             pbimages.ImageLocation = Book.Images[++Counter].ImagePath;
            HandleMoveButtonsAvailabilty();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            pbimages.ImageLocation = Book.Images[--Counter].ImagePath;
            HandleMoveButtonsAvailabilty();
        }

        private void ctrlBookInfo_Load(object sender, EventArgs e)
        {
           
        }

        private void lbtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
