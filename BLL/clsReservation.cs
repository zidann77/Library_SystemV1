using System;
using System.Data;
using BusinessLogicLayer.BusinessLogicLayer;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class clsReservation
    {
        public enum enReservationStatus : byte
        {
            New = 1,
            Cancelled = 2,
            Complete = 3
        }

        public int ID { get; set; }
        public int PersonID { get; set; }
        public int ByUser { get; set; }
        public int CopyID { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Detailes { get; set; }
        public enReservationStatus ReservationStatus { get; set; }

        public clsPeople Person { get; set; }
        public clsBookCopy CopyInfo { get; set; }

        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode = enMode.Add;

        // ====================== Constructors ======================

        public clsReservation()
        {
            ID = -1;
            PersonID = -1;
            ByUser = -1;
            CopyID = -1;
            ReservationDate = DateTime.Now;
            Detailes = "";
            ReservationStatus = enReservationStatus.New;
            Mode = enMode.Add;
        }

        private clsReservation(int id, int personID, int byUser, int copyID,
            DateTime reservationDate, string detailes, byte reservationStatus)
        {
            ID = id;
            PersonID = personID;
            ByUser = byUser;
            CopyID = copyID;
            ReservationDate = reservationDate;
            Detailes = detailes;
            ReservationStatus = (enReservationStatus)reservationStatus;

            Person = clsPeople.Find(personID);
            CopyInfo = clsBookCopy.Find(copyID);

            Mode = enMode.Update;
        }

        // ====================== Find ======================

        public static clsReservation Find(int id)
        {
            int personID = -1;
            int byUser = -1;
            int copyID = -1;
            DateTime reservationDate = DateTime.Now;
            string detailes = "";
            byte reservationStatus = 1;

            if (clsReservationData.GetReservationByID(id,
                ref personID, ref byUser, ref copyID,
                ref reservationDate, ref detailes, ref reservationStatus))
            {
                return new clsReservation(id, personID, byUser, copyID,
                    reservationDate, detailes, reservationStatus);
            }

            return null;
        }

        public static clsReservation FindByBookCopy(int copyID)
        {
            int personID = -1;
            int byUser = -1;
            int id = -1;
            DateTime reservationDate = DateTime.Now;
            string detailes = "";
            byte reservationStatus = 1;

            if (clsReservationData.GetReservationByBookCopyID(ref id,
                ref personID, ref byUser, copyID,
                ref reservationDate, ref detailes, ref reservationStatus))
            {
                return new clsReservation(id, personID, byUser, copyID,
                    reservationDate, detailes, reservationStatus);
            }

            return null;
        }

        // ====================== Save ======================

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
            int newID = clsReservationData.AddNewReservation(
                PersonID,
                ByUser,
                CopyID,
                ReservationDate,
                Detailes,
                (byte)ReservationStatus
            );

            if (newID > 0)
            {
                ID = newID;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsReservationData.UpdateReservation(
                ID,
                PersonID,
                ByUser,
                CopyID,
                ReservationDate,
                Detailes,
                (byte)ReservationStatus
            );
        }

        // ====================== Delete ======================

        public bool Delete()
        {
            return clsReservationData.DeleteReservation(ID);
        }

        public static bool Delete(int id)
        {
            return clsReservationData.DeleteReservation(id);
        }

        // ====================== Lists ======================

        public static DataTable GetAllReservations()
        {
            return clsReservationData.GetAllReservations();
        }

        public static bool Exists(int id)
        {
            return clsReservationData.IsReservationExist(id);
        }

        // ====================== Helpers ======================

        public bool IsNew => Mode == enMode.Add;

        public bool IsCancelled =>
            ReservationStatus == enReservationStatus.Cancelled;

        public bool IsCompleted =>
            ReservationStatus == enReservationStatus.Complete;

        public bool IsValid()
        {
            if (PersonID <= 0) return false;
            if (CopyID <= 0) return false;
            if (ByUser <= 0) return false;
            if (ReservationDate > DateTime.Now) return false;
            if (!string.IsNullOrWhiteSpace(Detailes) && Detailes.Length > 250) return false;

            return true;
        }
    }
}
