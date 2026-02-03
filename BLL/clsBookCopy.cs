using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    namespace BusinessLogicLayer
    {
        public class clsBookCopy
        {
            public int CopyID { get; set; }
            public int BookID { get; set; }
            public bool AvailabilityStatus { get; set; }
            public bool? Reserved { get; set; }   // ✅ added (nullable)
            public clsBook BookInfo { get; set; }

            public enum enMode { Add = 0, Update = 1 }
            public enMode Mode = enMode.Add;

            public clsBookCopy()
            {
                CopyID = -1;
                BookID = -1;
                AvailabilityStatus = true;
                Reserved = null;
                Mode = enMode.Add;
                BookInfo = new clsBook();
            }

            public clsBookCopy(int copyID, int bookID, bool availabilityStatus, bool? reserved = null)
            {
                CopyID = copyID;
                BookID = bookID;
                AvailabilityStatus = availabilityStatus;
                Reserved = reserved;
                Mode = enMode.Update;
                BookInfo = clsBook.Find(bookID);
            }

            // CRUD Operations
            public static clsBookCopy Find(int copyID)
            {
                int bookID = -1;
                bool availabilityStatus = false;
                bool? reserved = null;

                bool found = DataAccessLayer.clsBookCopiesData
                    .getCopyById(copyID, ref bookID, ref availabilityStatus, ref reserved);

                if (found)
                {
                    return new clsBookCopy(copyID, bookID, availabilityStatus, reserved);
                }
                else
                {
                    return null;
                }
            }

            public static clsBookCopy FindAvailableCopyForReservation(int BookID)
            {
                int CopyID = -1;
                bool Found =  clsBookCopiesData.GetAvailableCopyForReserving(BookID , ref CopyID);
                if (Found)
                    return new clsBookCopy(CopyID, BookID, false, false);
                else
                    return null;
            }

            public static clsBookCopy FindAvailableCopyForBorrowing(int BookID)
            {
                int CopyID = -1;
                bool availabilityStatus = true;

                bool found = DataAccessLayer.clsBookCopiesData
                    .GetAvailableCopyForBorrowing(BookID, ref CopyID);

                if (found)
                {
                    return new clsBookCopy(CopyID, BookID, availabilityStatus, false);
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
                int newId = DataAccessLayer.clsBookCopiesData
                    .addNewCopy(BookID, AvailabilityStatus, Reserved);

                if (newId > 0)
                {
                    CopyID = newId;
                    return true;
                }
                return false;
            }

            private bool _Update()
            {
                return DataAccessLayer.clsBookCopiesData
                    .updateCopy(CopyID, BookID, AvailabilityStatus, Reserved);
            }

            public bool Delete()
            {
                return DataAccessLayer.clsBookCopiesData.deleteCopy(CopyID);
            }

            public static bool Delete(int copyID)
            {
                return DataAccessLayer.clsBookCopiesData.deleteCopy(copyID);
            }

            public static DataTable GetAllCopies()
            {
                return DataAccessLayer.clsBookCopiesData.getAllCopies();
            }

            public static List<clsBookCopy> GetCopiesList()
            {
                List<clsBookCopy> copiesList = new List<clsBookCopy>();
                DataTable dt = GetAllCopies();

                foreach (DataRow row in dt.Rows)
                {
                    clsBookCopy copy = new clsBookCopy(
                        (int)row["CopyID"],
                        (int)row["BookID"],
                        (bool)row["AvailabilityStatus"],
                        row["Reserved"] == DBNull.Value ? null : (bool?)row["Reserved"]
                    );

                    copiesList.Add(copy);
                }

                return copiesList;
            }

            public static bool Exists(int copyID)
            {
                return DataAccessLayer.clsBookCopiesData.isCopyExist(copyID);
            }

            public string CopyInfo
            {
                get
                {
                    string reservedText = Reserved == true ? "Reserved" : "Free";
                    return $"Copy #{CopyID} - {(AvailabilityStatus ? "Available" : "Borrowed")} - {reservedText} - Book: {BookInfo?.Title}";
                }
            }

            public static bool DeleteCopiesByBookID(int bookID)
            {
                return DataAccessLayer.clsBookCopiesData.deleteCopiesByBookId(bookID);
            }

            // Property to check if copy is available
            public bool IsAvailable
            {
                get { return AvailabilityStatus && Reserved != true; }
            }

            static public int getCountAvailableCopies(int BookID)
            {
                return clsBookCopiesData.getAvailableCopiesCount(BookID);
            }

            static public int getCountAllCopies(int BookID)
            {
                return clsBookCopiesData.getCopiesCountByBookId(BookID);
            }
        }
    }
}
