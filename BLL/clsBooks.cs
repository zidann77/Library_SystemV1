using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsBook
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal? Rate { get; set; }
        public string Detailes { get; set; }

        public List<clsBookImage> Images { get; set; }

        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode = enMode.Add;

        public clsBook()
        {
            BookID = -1;
            Title = "";
            ISBN = "";
            PublicationDate = DateTime.Now;
            Rate = null;
            Detailes = "";
            Mode = enMode.Add;
            Images = new List<clsBookImage>();
        }

        private clsBook(int bookID, string title, string isbn, DateTime publicationDate,
                       decimal? rate, string detailes)
        {
            BookID = bookID;
            Title = title;
            ISBN = isbn;
            PublicationDate = publicationDate;
            Rate = rate;
            Detailes = detailes;
            Mode = enMode.Update;
            Images = clsBookImage.GetImagesByBookId(bookID);
        }

        // CRUD Operations
        public static clsBook Find(int bookId)
        {
            string title = "", isbn = "", detailes = "";
            DateTime publicationDate = DateTime.Now;
            decimal? rate = null;

            if (clsBooksData.getBookById(bookId, ref title, ref isbn,
                ref publicationDate, ref rate, ref detailes))
            {
                return new clsBook(bookId, title, isbn, publicationDate, rate, detailes);
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
            int newId = clsBooksData.addNewBook(Title, ISBN, PublicationDate, Rate, Detailes);

            if (newId > 0)
            {
                BookID = newId;
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            return clsBooksData.updateBook(BookID, Title, ISBN, PublicationDate, Rate, Detailes);
        }

        public bool Delete()
        {
            return clsBooksData.deleteBook(BookID);
        }

        public static bool Delete(int bookId)
        {
            return clsBooksData.deleteBook(bookId);
        }

        public static DataTable GetAllBooks()
        {
            return clsBooksData.getAllBooks();
        }

        public static DataTable GetBookCopisByBookID(int bookId)
        {
            return clsBooksData.getBookCopiesByBookId(bookId);
        }
       
        public static List<clsBook> GetBooksList()
        {
            List<clsBook> booksList = new List<clsBook>();
            DataTable dt = GetAllBooks();

            foreach (DataRow row in dt.Rows)
            {
                clsBook book = new clsBook(
                    (int)row["BookID"],
                    (string)row["Title"],
                    (string)row["ISBN"],
                    (DateTime)row["PublicationDate"],
                    row["Rate"] == DBNull.Value ? null : (decimal?)row["Rate"],
                    row["Detailes"] == DBNull.Value ? "" : (string)row["Detailes"]
                );

                booksList.Add(book);
            }

            return booksList;
        }

        public static bool Exists(int bookId)
        {
            return clsBooksData.isBookExist(bookId);
        }

        public static bool Exists(string isbn)
        {
            return clsBooksData.isBookExist(isbn);
        }

        public bool IsNew
        {
            get { return Mode == enMode.Add; }
        }

        public string BookInfo
        {
            get
            {
                return $"{Title} (ISBN: {ISBN}) - Published: {PublicationDate:yyyy}";
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (string.IsNullOrWhiteSpace(ISBN)) return false;
            if (PublicationDate > DateTime.Now) return false;
            if (Rate.HasValue && (Rate < 0 || Rate > 5)) return false;
            if (!string.IsNullOrWhiteSpace(Detailes) && Detailes.Length > 250) return false;

            return true;
        }

        public override string ToString()
        {
            return $"{Title} - {ISBN}";
        }

        public override bool Equals(object obj)
        {
            if (obj is clsBook otherBook)
            {
                return BookID == otherBook.BookID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return BookID.GetHashCode() ^ Title.GetHashCode() ^ ISBN.GetHashCode();
        }

        public static List<clsBook> GetBooksByRating(decimal minRating)
        {
            return GetBooksList().Where(b => b.Rate >= minRating).ToList();
        }

        public static List<clsBook> GetBooksPublishedAfter(DateTime date)
        {
            return GetBooksList().Where(b => b.PublicationDate >= date).ToList();
        }

        public static List<clsBook> GetBooksByTitle(string titleKeyword)
        {
            return GetBooksList()
                .Where(b => b.Title.IndexOf(titleKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public int AgeInYears
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - PublicationDate.Year;
                if (PublicationDate > today.AddYears(-age)) age--;
                return age;
            }
        }

        public bool HasDetails
        {
            get { return !string.IsNullOrWhiteSpace(Detailes); }
        }

        public bool IsRated
        {
            get { return Rate.HasValue; }
        }

        public string RatingDisplay
        {
            get
            {
                return Rate.HasValue ? $"{Rate.Value:0.0}/5" : "Not Rated";
            }
        }
    }
}
