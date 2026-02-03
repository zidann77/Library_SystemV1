using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsBookImage
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public short ImageOrder { get; set; }
        public int BookID { get; set; }

        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode = enMode.Add;

        public clsBookImage()
        {
            ID = -1;
            ImagePath = "";
            ImageOrder = 0;
            BookID = -1;
            // BookInfo = new clsBook();
            Mode = enMode.Add;
        }

        public clsBookImage(int id, string imagePath, short imageOrder, int bookID)
        {
            ID = id;
            ImagePath = imagePath;
            ImageOrder = imageOrder;
            BookID = bookID;
            // BookInfo = clsBook.Find(bookID); // If you have a Book class
            Mode = enMode.Update;


        }

        // CRUD Operations
        public static clsBookImage Find(int imageId)
        {
            string imagePath = "";
            short imageOrder = 0;
            int bookID = -1;

            if (clsBookImagesData.getImageById(imageId, ref imagePath, ref imageOrder, ref bookID))
            {
                return new clsBookImage(imageId, imagePath, imageOrder, bookID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }

        private bool _AddNew()
        {
            int newId = clsBookImagesData.addNewImage(ImagePath, ImageOrder, BookID);

            if (newId > 0)
            {
                ID = newId;
                // BookInfo = clsBook.Find(BookID); // If you have a Book class
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            bool updated = clsBookImagesData.updateImage(ID, ImagePath, ImageOrder, BookID);

            if (updated)
            {
                // BookInfo = clsBook.Find(BookID); // If you have a Book class
            }

            return updated;
        }

        public bool Delete()
        {
            return clsBookImagesData.deleteImage(ID);
        }

        public static bool Delete(int imageId)
        {
            return clsBookImagesData.deleteImage(imageId);
        }

        public static DataTable GetAllBookImages()
        {
            return clsBookImagesData.getAllImages();
        }



        public static bool Exists(int imageId)
        {
            return clsBookImagesData.isImageExist(imageId);
        }

        public static bool Exists(string imagePath)
        {
            return clsBookImagesData.isImagePathExist(imagePath);
        }



        // Additional methods specific to BookImages
        public static List<clsBookImage> GetImagesByBookId(int bookId)
        {
            List<clsBookImage> imagesList = new List<clsBookImage>();
            DataTable dt = clsBookImagesData.getImagesByBookId(bookId);

            foreach (DataRow row in dt.Rows)
            {
                clsBookImage image = new clsBookImage(
                    (int)row["ID"],
                    (string)row["ImagePath"],
                    (short)row["ImageOrder"],
                    (int)row["BookID"]
                );

                imagesList.Add(image);
            }

            return imagesList;
        }

        public static int GetImageCountByBookId(int bookId)
        {
            return clsBookImagesData.getImageCountByBookId(bookId);
        }

        public static bool DeleteImagesByBookId(int bookId)
        {
            return clsBookImagesData.deleteImagesByBookId(bookId);
        }



    }
}
