using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;


  using DataAccessLayer;
    using DataAccessLayer.DataAccessLayer;

    using System.Data;
using BusinessLogicLayer.BusinessLogicLayer;

namespace BusinessLogicLayer
    {
       public  class clsBorrowing
        {
            public int ID { get; set; }
            public DateTime BorrowingDate { get; set; }
            public int PersonID { get; set; }

            public clsPeople Person { get; set; }
            public int CopyID { get; set; }
            public int? FineID { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime? ActualReturnDate { get; set; }
            public int ByUser { get; set; }
            public string Detailes { get; set; }

            public clsBookCopy CopyInfo {  get; set; }

            public enum enMode { Add = 0, Update = 1 }
            public enMode Mode = enMode.Add;

            public clsBorrowing()
            {
                ID = -1;
                BorrowingDate = DateTime.Now;
                PersonID = -1;
                CopyID = -1;
                FineID = null;
                EndDate = DateTime.Now;
                ActualReturnDate = null;
                ByUser = -1;
                Detailes = "";
                Mode = enMode.Add;
                CopyInfo = null;
            }

            public clsBorrowing(int id, DateTime borrowingDate, int personID, int copyID,
                int? fineID, DateTime duration, DateTime? actualReturnDate, int byUser,
                string detailes)
            {
                ID = id;
                BorrowingDate = borrowingDate;
                PersonID = personID;
                Person = clsPeople.Find(personID);
                CopyID = copyID;
                FineID = fineID;
                EndDate = duration;
                ActualReturnDate = actualReturnDate;
                ByUser = byUser;
                Detailes = detailes;
                Mode = enMode.Update;
                CopyInfo = clsBookCopy.Find(this.CopyID);
            }

            // ====================== CRUD ======================

            public static clsBorrowing Find(int id)
            {
                DateTime borrowingDate = DateTime.Now;
                int personID = -1;
                int copyID = -1;
                int? fineID = null;
                DateTime duration = DateTime.Now;
                DateTime? actualReturnDate = null;
                int byUser = -1;
                string detailes = "";

                if (clsBorrowingData.GetBorrowingByID(id, ref borrowingDate, ref personID,
                    ref copyID, ref fineID, ref duration, ref actualReturnDate, ref byUser,
                    ref detailes))
                {
                    return new clsBorrowing(id, borrowingDate, personID, copyID, fineID,
                        duration, actualReturnDate, byUser, detailes);
                }
                else
                    return null;
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
                        return false;

                    case enMode.Update:
                        return _Update();

                    default:
                        return false;
                }
            }

            private bool _AddNew()
            {
                int newID = clsBorrowingData.AddNewBorrowing(BorrowingDate, PersonID, CopyID,
                    FineID, EndDate, ActualReturnDate, ByUser, Detailes);

                if (newID > 0)
                {
                    ID = newID;
                    return true;
                }
                return false;
            }

            private bool _Update()
            {
                return clsBorrowingData.UpdateBorrowing(ID, BorrowingDate, PersonID, CopyID,
                    FineID, EndDate, ActualReturnDate, ByUser, Detailes);
            }

            public bool Delete()
            {
                return clsBorrowingData.DeleteBorrowing(ID);
            }

            public static bool Delete(int id)
            {
                return clsBorrowingData.DeleteBorrowing(id);
            }

            public static DataTable GetAllBorrowings()
            {
                return clsBorrowingData.GetAllBorrowings();
            }

            public static bool Exists(int id)
            {
                return clsBorrowingData.IsBorrowingExist(id);
            }

            // ====================== Extra Helpers ======================

            public bool IsNew
            {
                get { return Mode == enMode.Add; }
            }

            public bool IsReturned
            {
                get { return ActualReturnDate.HasValue; }
            }

            public string BorrowingInfo
            {
                get
                {
                    return $"PersonID: {PersonID}, CopyID: {CopyID}, Borrowed: {BorrowingDate:yyyy-MM-dd}";
                }
            }

            public bool IsValid()
            {
                if (PersonID <= 0) return false;
                if (CopyID <= 0) return false;
                if (BorrowingDate > DateTime.Now) return false;
                if (EndDate < BorrowingDate) return false;
                if (!string.IsNullOrWhiteSpace(Detailes) && Detailes.Length > 250) return false;

                return true;
            }
        }
    }


