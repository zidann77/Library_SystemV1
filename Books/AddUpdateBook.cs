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

namespace Library.Books
{
    public partial class AddUpdateBook : Form
    {
        enum enMode { add, Update }
        enMode Mode = enMode.add;

        int BookID = 0;
        clsBook Book = null;
        List<string> ImagesPath = new List<string>();

      //public  enum enImgMode { add, update }

      //public  enImgMode _ImgMode = enImgMode.add;

      //  public enImgMode ImgButtonMode
      //  {
      //      get { return _ImgMode; }
      //      set { _ImgMode = value; 
      //      if(value == enImgMode.add)
      //          {
                   
      //          }
      //      }
      //  }


        public AddUpdateBook()
        {
            InitializeComponent();
        }

        void UpdateBookMainImg()
        {
            if (Book.Images.Count == 0)
            {
                //btnManageImg.Visible = false;
                return;
            }

            Book.Images.OrderBy(n => n.ImageOrder);
            GetImagesPathFromObject();
            pbBook.ImageLocation = Book.Images[0].ImagePath;
            btnManageImg.Visible = true;
        }


        public AddUpdateBook(int BookID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            Book = clsBook.Find(BookID);
        }

        private void Frm_DataBack(object sender, List<string> Images)
        {
            if(Images.Count == 0) { return; }
            pbBook.ImageLocation = Images.FirstOrDefault().ToString();
            ImagesPath = Images;
          //  MessageBox.Show(string.Join(",", Images));
        }

      

        void GetImagesPathFromObject()
        {
            foreach(var item in Book.Images)
            {
                ImagesPath.Add(item.ImagePath); 
            }
        }

        void RefreshImages()
        {
            Book.Images = clsBookImage.GetImagesByBookId(Book.BookID);
            pbBook.ImageLocation = Book.Images[0].ImagePath;    
        }

        void SendImagesToObject()
        {
            ImagesPath.OrderBy(n => n);
            for(int i =0; i< ImagesPath.Count;i++)
            {
                clsBookImage image = new clsBookImage();
                image.ImagePath = ImagesPath[i];
                image.ImageOrder = (short)(i + 1);
                image.BookID = Book.BookID;
                image.Save();
            }

            RefreshImages();
        }


        private void AddUpdateBook_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {

                if (Book != null)
                {
                    txtTitle.Text = Book.Title;
                    txtISBN.Text = Book.ISBN;
                    BookDateTimePicker1.Value = Book.PublicationDate;
                    txtDetails.Text = Book.Detailes;

                    if (Book.Images.Count > 0)
                    {
                        UpdateBookMainImg();
                    }
                }
                else
                {
                    frmMessage frm = new frmMessage("There are No Data to Show", frmMessage.enMode.Error);
                    frm.ShowDialog();
                }
            }
            else
            {
                //    btnManageImg.Enabled = false;
                Book = new clsBook();
            }
        }
        private void btnManageImg_Click(object sender, EventArgs e)
        {
            frmHandleBookImages frm = new frmHandleBookImages(ImagesPath);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage("Are Your Sure " , frmMessage.enMode.Question);
            frmMessage.ShowDialog();


            if( frmMessage.Result == frmMessage.enResult.Ok)
            {
                
                Book .ISBN = txtISBN.Text;
                Book.Title = txtTitle.Text;
                Book .PublicationDate = BookDateTimePicker1.Value;
                Book .Detailes = txtDetails.Text;
                Book.Save();

                if(Mode == enMode.Update)
                clsBookImage.DeleteImagesByBookId(Book.BookID);



                SendImagesToObject();

              
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDetails_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
