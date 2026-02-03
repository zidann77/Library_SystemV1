using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsPermission
    {
        enum enMode { AddNew = 0, Update = 1 }

        enMode Mode = enMode.AddNew;
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public int PermissionNumber { get; set; }
        public string Description { get; set; }

        public clsPermission()
        {
            PermissionID = -1;
            PermissionName = string.Empty;
            PermissionNumber = -1;
            Description = string.Empty;
            Mode = enMode.AddNew;
        }

        public clsPermission(int permissionID, string permissionName, int permissionNumber, string description)
        {
            this.PermissionID = permissionID;
            this.PermissionName = permissionName;
            this.PermissionNumber = permissionNumber;
            this.Description = description;
            Mode = enMode.Update;
        }

        public static clsPermission FindByPermissionID(int permissionID)
        {
            string permissionName = string.Empty;
            int permissionNumber = -1;
            string description = string.Empty;

            if (clsPermissionData.GetPermissionInfoByPermissionID(permissionID, ref permissionName,
                ref permissionNumber, ref description))
            {
                return new clsPermission(permissionID, permissionName, permissionNumber, description);
            }
            else
                return null;
        }

        public static clsPermission FindByPermissionNumber(int permissionNumber)
        {
            int permissionID = -1;
            string permissionName = string.Empty;
            string description = string.Empty;

            bool isFound = clsPermissionData.GetPermissionInfoByPermissionNumber(permissionNumber,
                ref permissionID, ref permissionName, ref description);

            if (isFound)
                return new clsPermission(permissionID, permissionName, permissionNumber, description);
            else
                return null;
        }

        public static clsPermission FindByPermissionName(string permissionName)
        {
            int permissionID = -1;
            int permissionNumber = -1;
            string description = string.Empty;

            if (clsPermissionData.GetPermissionInfoByPermissionName(permissionName, ref permissionID,
                ref permissionNumber, ref description))
            {
                return new clsPermission(permissionID, permissionName, permissionNumber, description);
            }
            else
                return null;
        }

        private bool _AddNewPermission()
        {
            this.PermissionID = clsPermissionData.AddNewPermission(this.PermissionName,
                this.PermissionNumber, this.Description);
            return this.PermissionID != -1;
        }

        private bool _UpdatePermission()
        {
            return clsPermissionData.UpdatePermission(this.PermissionID, this.PermissionName,
                this.PermissionNumber, this.Description);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPermission())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePermission();
            }
            return false;
        }

        public static DataTable GetAllPermissions()
        {
            return clsPermissionData.GetAllPermissions();
        }

        public static bool DeletePermission(int permissionID)
        {
            return clsPermissionData.DeletePermission(permissionID);
        }

        public static bool IsPermissionExist(int permissionID)
        {
            return clsPermissionData.IsPermissionExist(permissionID);
        }

        public static bool IsPermissionExist(string permissionName)
        {
            return clsPermissionData.IsPermissionExist(permissionName);
        }

        public static bool IsPermissionExistByNumber(int permissionNumber)
        {
            return clsPermissionData.IsPermissionExistByNumber(permissionNumber);
        }

        public static DataTable GetPermissionsByRoleID(int roleID)
        {
            return clsPermissionData.GetPermissionsByRoleID(roleID);
        }

        // Additional business logic methods
        public bool HasAccess(int requiredPermissionNumber)
        {
            return this.PermissionNumber >= requiredPermissionNumber;
        }

        public bool CanGrantPermission(clsPermission targetPermission)
        {
            // Business rule: A permission can only grant permissions of lower or equal number
            return this.PermissionNumber >= targetPermission.PermissionNumber;
        }

        public override string ToString()
        {
            return $"{PermissionName} (#{PermissionNumber})";
        }

        public string GetFullInfo()
        {
            return $"Permission ID: {PermissionID}\n" +
                   $"Name: {PermissionName}\n" +
                   $"Number: {PermissionNumber}\n" +
                   $"Description: {Description}";
        }

        // Static utility methods
        public static bool ValidatePermissionNumber(int permissionNumber)
        {
            return permissionNumber >= 0 && permissionNumber <= 9999; // Example validation
        }

        public static bool ValidatePermissionName(string permissionName)
        {
            return !string.IsNullOrWhiteSpace(permissionName) && permissionName.Length <= 50;
        }
    }
}
