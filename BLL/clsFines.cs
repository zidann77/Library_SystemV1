using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class clsFine
    {
        public int FineID { get; set; }
        public int ByUser { get; set; }
        public int BorrowingRecordID { get; set; }
        public short NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }
        public bool? PaymentStatus { get; set; }
        public bool? PaymentWay { get; set; }
        public string Detailes { get; set; }

        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode = enMode.Add;

        public clsFine()
        {
            FineID = -1;
            ByUser = -1;
            BorrowingRecordID = -1;
            NumberOfLateDays = 0;
            FineAmount = 0;
            PaymentStatus = null;
            PaymentWay = null;
            Detailes = "";
            Mode = enMode.Add;
        }

        private clsFine(int fineID, int byUser, int borrowingRecordID, short numberOfLateDays,
                        decimal fineAmount, bool? paymentStatus, bool? paymentWay, string detailes)
        {
            FineID = fineID;
            ByUser = byUser;
            BorrowingRecordID = borrowingRecordID;
            NumberOfLateDays = numberOfLateDays;
            FineAmount = fineAmount;
            PaymentStatus = paymentStatus;
            PaymentWay = paymentWay;
            Detailes = detailes;
            Mode = enMode.Update;
        }

        // CRUD Operations
        public static clsFine Find(int fineId)
        {
            int byUser = -1;
            int borrowingRecordID = -1;
            short numberOfLateDays = 0;
            decimal fineAmount = 0;
            bool? paymentStatus = null;
            bool? paymentWay = null;
            string detailes = "";

            if (clsFinesData.getFineById(fineId, ref byUser, ref borrowingRecordID,
                ref numberOfLateDays, ref fineAmount, ref paymentStatus, ref paymentWay, ref detailes))
            {
                return new clsFine(fineId, byUser, borrowingRecordID, numberOfLateDays,
                    fineAmount, paymentStatus, paymentWay, detailes);
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
            int newId = clsFinesData.addNewFine(ByUser, BorrowingRecordID, NumberOfLateDays,
                FineAmount, PaymentStatus, PaymentWay, Detailes);

            if (newId > 0)
            {
                FineID = newId;
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            return clsFinesData.updateFine(FineID, ByUser, BorrowingRecordID, NumberOfLateDays,
                FineAmount, PaymentStatus, PaymentWay, Detailes);
        }

        public bool Delete()
        {
            return clsFinesData.deleteFine(FineID);
        }

        public static bool Delete(int fineId)
        {
            return clsFinesData.deleteFine(fineId);
        }

        public static DataTable GetAllFines()
        {
            return clsFinesData.getAllFines();
        }

        public static List<clsFine> GetFinesList()
        {
            List<clsFine> finesList = new List<clsFine>();
            DataTable dt = GetAllFines();

            foreach (DataRow row in dt.Rows)
            {
                clsFine fine = new clsFine(
                    (int)row["FineID"],
                    (int)row["ByUser"],
                    (int)row["BorrowingRecordID"],
                    (short)row["NumberOfLateDays"],
                    (decimal)row["FineAmount"],
                    row["PaymentStatus"] == DBNull.Value ? null : (bool?)row["PaymentStatus"],
                    row["PaymentWay"] == DBNull.Value ? null : (bool?)row["PaymentWay"],
                    row["Detailes"] == DBNull.Value ? "" : (string)row["Detailes"]
                );

                finesList.Add(fine);
            }

            return finesList;
        }

        public static bool Exists(int fineId)
        {
            return clsFinesData.isFineExist(fineId);
        }

        public bool IsNew
        {
            get { return Mode == enMode.Add; }
        }

        public bool IsPaid
        {
            get { return PaymentStatus.HasValue && PaymentStatus.Value; }
        }

        public bool IsUnpaid
        {
            get { return !PaymentStatus.HasValue || !PaymentStatus.Value; }
        }

        public bool HasDetails
        {
            get { return !string.IsNullOrWhiteSpace(Detailes); }
        }

        public string FineInfo
        {
            get
            {
                return $"Fine #{FineID} - Amount: {FineAmount:C}";
            }
        }

        public override string ToString()
        {
            return $"Fine #{FineID} - {FineAmount:C}";
        }
    }
}

