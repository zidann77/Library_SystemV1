using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 }

        enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastState { get; set; }
        public int CurrentRoleID { get; set; }
        public int LastRole { get; set; }
        public bool IsActive { get; set; }

        // Assuming you have a clsPerson class for person information
        public clsPeople PersonInfo { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = string.Empty;
            Password = string.Empty;
            LastState = DateTime.Now;
            CurrentRoleID = -1;
            LastRole = -1;
            IsActive = true;
            PersonInfo = null;
            Mode = enMode.AddNew;
        }

        public clsUser(int userID, int personID, string userName, string password,
                      DateTime lastState, int currentRoleID, int lastRole, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.LastState = lastState;
            this.CurrentRoleID = currentRoleID;
            this.LastRole = lastRole;
            this.IsActive = isActive;
            this.PersonInfo = clsPeople.Find(personID); // Assuming clsPerson has a Find method

            Mode = enMode.Update;
        }

        public static clsUser FindByUserID(int userID)
        {
            string userName = string.Empty;
            string password = string.Empty;
            DateTime lastState = DateTime.Now;
            int currentRoleID = -1;
            int personID = -1;
            int lastRole = -1;
            bool isActive = true;

            if (clsUserData.GetUserInfoByUserID(userID, ref userName, ref password,
                ref lastState, ref currentRoleID, ref personID, ref lastRole, ref isActive))
            {
                return new clsUser(userID, personID, userName, password,
                                 lastState, currentRoleID, lastRole, isActive);
            }
            else
                return null;
        }

        public static clsUser FindByPersonID(int personID)
        {
            int userID = -1;
            string userName = string.Empty;
            string password = string.Empty;
            DateTime lastState = DateTime.Now;
            int currentRoleID = -1;
            int lastRole = -1;
            bool isActive = true;

            bool isFound = clsUserData.GetUserInfoByPersonID(personID, ref userID,
                ref userName, ref password, ref lastState, ref currentRoleID, ref lastRole, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password,
                                 lastState, currentRoleID, lastRole, isActive);
            else
                return null;
        }

        public static clsUser FindByUserNameAndPassword(string userName, string password)
        {
            int userID = -1;
            DateTime lastState = DateTime.Now;
            int currentRoleID = -1;
            int personID = -1;
            int lastRole = -1;
            bool isActive = true;

            if (clsUserData.GetUserInfoByUsernameAndPassword(userName, password,
                ref userID, ref lastState, ref currentRoleID, ref personID, ref lastRole, ref isActive))
            {
                return new clsUser(userID, personID, userName, password,
                                 lastState, currentRoleID, lastRole, isActive);
            }
            else
                return null;
        }

        public static clsUser FindByUserNameAndPasswordActiveOnly(string userName, string password)
        {
            int userID = -1;
            DateTime lastState = DateTime.Now;
            int currentRoleID = -1;
            int personID = -1;
            int lastRole = -1;
            bool isActive = true;

            if (clsUserData.GetUserInfoByUsernameAndPasswordActiveOnly(userName, password,
                ref userID, ref lastState, ref currentRoleID, ref personID, ref lastRole, ref isActive))
            {
                return new clsUser(userID, personID, userName, password,
                                 lastState, currentRoleID, lastRole, isActive);
            }
            else
                return null;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.UserName, this.Password, this.LastState,
                                               this.CurrentRoleID, this.PersonID, this.LastRole, this.IsActive);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password,
                                        this.LastState, this.CurrentRoleID, this.PersonID, this.LastRole, this.IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static DataTable GetActiveUsers()
        {
            return clsUserData.GetActiveUsers();
        }

        public static DataTable GetInactiveUsers()
        {
            return clsUserData.GetInactiveUsers();
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }

        public static bool IsUserExist(int userID)
        {
            return clsUserData.IsUserExist(userID);
        }

        public static bool IsUserExist(string userName)
        {
            return clsUserData.IsUserExist(userName);
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            return clsUserData.IsUserExistForPersonID(personID);
        }

        public bool ChangePassword(string newPassword)
        {
            if (clsUserData.ChangePassword(this.UserID, newPassword))
            {
                this.Password = newPassword;
                return true;
            }
            return false;
        }

  
        public static bool ActivateUser(int userID)
        {
            return clsUserData.ActivateUser(userID);
        }

        public static bool DeactivateUser(int userID)
        {
            return clsUserData.DeactivateUser(userID);
        }

      

      
       
        public string UserInfo
        {
            get
            {
                return $"{UserName} (ID: {UserID}) - {(IsActive ? "Active" : "Inactive")}";
            }
        }

     

        // Static methods to get lists
        public static List<clsUser> GetUsersList()
        {
            List<clsUser> usersList = new List<clsUser>();
            DataTable dt = GetAllUsers();

            foreach (DataRow row in dt.Rows)
            {
                clsUser user = new clsUser(
                    (int)row["UserID"],
                    (int)row["PersonID"],
                    (string)row["UserName"],
                    (string)row["Password"],
                    (DateTime)row["LastState"],
                    (int)row["CurrentRoleID"],
                    (int)row["LastRole"],
                    (bool)row["IsActive"]
                );

                usersList.Add(user);
            }

            return usersList;
        }

        public static List<clsUser> GetActiveUsersList()
        {
            return GetUsersList().Where(u => u.IsActive).ToList();
        }

        public static List<clsUser> GetInactiveUsersList()
        {
            return GetUsersList().Where(u => !u.IsActive).ToList();
        }
    }
}
