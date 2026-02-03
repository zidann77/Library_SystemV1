using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsPermissionsRecords
    {
        enum enMode { AddNew = 0, Update = 1 }
        public enum enPermissionNumber { view =1 , Remove = 2  , Add = 4 , Update = 8 , ChangePassword = 16}

        enMode Mode = enMode.AddNew;
        public int PermissionsID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public DateTime LastUpdate { get; set; }

        // Navigation properties
    //    public clsRole RoleInfo { get; set; }
        public clsPermission PermissionInfo { get; set; }

        public clsPermissionsRecords()
        {
            PermissionsID = -1;
            RoleID = -1;
            PermissionID = -1;
            LastUpdate = DateTime.Now;
      ///      RoleInfo = null;
            PermissionInfo = null;
            Mode = enMode.AddNew;
        }

        public clsPermissionsRecords(int permissionsID, int roleID, int permissionID, DateTime lastUpdate)
        {
            this.PermissionsID = permissionsID;
            this.RoleID = roleID;
            this.PermissionID = permissionID;
            this.LastUpdate = lastUpdate;
        //    this.RoleInfo = clsRole.FindByRoleID(roleID); // Assuming clsRole exists
            this.PermissionInfo = clsPermission.FindByPermissionID(permissionID);

            Mode = enMode.Update;
        }

        public static clsPermissionsRecords FindByPermissionsID(int permissionsID)
        {
            int roleID = -1;
            int permissionID = -1;
            DateTime lastUpdate = DateTime.Now;

            if (clsPermissionsRecordsData.GetPermissionsRecordInfoByID(permissionsID, ref roleID,
                ref permissionID, ref lastUpdate))
            {
                return new clsPermissionsRecords(permissionsID, roleID, permissionID, lastUpdate);
            }
            else
                return null;
        }

        public static clsPermissionsRecords FindByRoleAndPermission(int roleID, int permissionID)
        {
            int permissionsID = -1;
            DateTime lastUpdate = DateTime.Now;

            if (clsPermissionsRecordsData.GetPermissionsRecordInfoByRoleAndPermission(roleID, permissionID,
                ref permissionsID, ref lastUpdate))
            {
                return new clsPermissionsRecords(permissionsID, roleID, permissionID, lastUpdate);
            }
            else
                return null;
        }

        public static bool GrantPermissionToRole(int roleID, int permissionID)
        {
            // Check if the permission is already granted
            if (IsPermissionGrantedToRole(roleID, permissionID))
                return false; // Already exists

            // Create new permissions record
            int newID = clsPermissionsRecordsData.AddNewPermissionsRecord(roleID, permissionID);
            return newID != -1;
        }

        public static bool RevokePermissionFromRole(int roleID, int permissionID)
        {
            return clsPermissionsRecordsData.DeletePermissionsRecordByRoleAndPermission(roleID, permissionID);
        }

        public static bool RevokeAllPermissionsFromRole(int roleID)
        {
            return clsPermissionsRecordsData.DeleteAllPermissionsRecordsByRoleID(roleID);
        }

        public static bool RevokePermissionFromAllRoles(int permissionID)
        {
            return clsPermissionsRecordsData.DeleteAllPermissionsRecordsByPermissionID(permissionID);
        }

        private bool _AddNewPermissionsRecord()
        {
            this.PermissionsID = clsPermissionsRecordsData.AddNewPermissionsRecord(this.RoleID, this.PermissionID);

            if (this.PermissionsID != -1)
            {
                // Refresh the LastUpdate from database
                var updatedRecord = FindByPermissionsID(this.PermissionsID);
                if (updatedRecord != null)
                {
                    this.LastUpdate = updatedRecord.LastUpdate;
                }
                return true;
            }

            return false;
        }

        private bool _UpdatePermissionsRecord()
        {
            if (clsPermissionsRecordsData.UpdatePermissionsRecord(this.PermissionsID, this.RoleID, this.PermissionID))
            {
                // Refresh the LastUpdate from database
                var updatedRecord = FindByPermissionsID(this.PermissionsID);
                if (updatedRecord != null)
                {
                    this.LastUpdate = updatedRecord.LastUpdate;
                }
                return true;
            }

            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPermissionsRecord())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePermissionsRecord();
            }
            return false;
        }

        public bool Delete()
        {
            return clsPermissionsRecordsData.DeletePermissionsRecord(this.PermissionsID);
        }

        public static DataTable GetAllPermissionsRecords()
        {
            return clsPermissionsRecordsData.GetAllPermissionsRecords();
        }

        public static DataTable GetPermissionsRecordsByRoleID(int roleID)
        {
            return clsPermissionsRecordsData.GetPermissionsRecordsByRoleID(roleID);
        }

        public static DataTable GetPermissionsRecordsByPermissionID(int permissionID)
        {
            return clsPermissionsRecordsData.GetPermissionsRecordsByPermissionID(permissionID);
        }

        public static DataTable GetRolePermissionsWithDetails(int roleID)
        {
            return clsPermissionsRecordsData.GetRolePermissionsWithDetails(roleID);
        }

        public static bool IsPermissionsRecordExist(int permissionsID)
        {
            return clsPermissionsRecordsData.IsPermissionsRecordExist(permissionsID);
        }

        public static bool IsPermissionGrantedToRole(int roleID, int permissionID)
        {
            return clsPermissionsRecordsData.IsPermissionsRecordExist(roleID, permissionID);
        }

        public bool UpdateTimestamp()
        {
            return clsPermissionsRecordsData.UpdateLastUpdateTimestamp(this.PermissionsID);
        }

        // Business logic methods
        public static bool CanRoleAccessPermission(int roleID, int permissionID)
        {
            return IsPermissionGrantedToRole(roleID, permissionID);
        }

        public static bool BulkGrantPermissionsToRole(int roleID, List<int> permissionIDs)
        {
            bool allSuccess = true;

            foreach (int permissionID in permissionIDs)
            {
                if (!IsPermissionGrantedToRole(roleID, permissionID))
                {
                    if (!GrantPermissionToRole(roleID, permissionID))
                    {
                        allSuccess = false;
                    }
                }
            }

            return allSuccess;
        }

        public static bool BulkRevokePermissionsFromRole(int roleID, List<int> permissionIDs)
        {
            bool allSuccess = true;

            foreach (int permissionID in permissionIDs)
            {
                if (!RevokePermissionFromRole(roleID, permissionID))
                {
                    allSuccess = false;
                }
            }

            return allSuccess;
        }

        public static List<clsPermission> GetRolePermissionsList(int roleID)
        {
            List<clsPermission> permissions = new List<clsPermission>();
            DataTable dt = GetPermissionsRecordsByRoleID(roleID);

            foreach (DataRow row in dt.Rows)
            {
                int permissionID = Convert.ToInt32(row["PermissionID"]);
                clsPermission permission = clsPermission.FindByPermissionID(permissionID);
                if (permission != null)
                {
                    permissions.Add(permission);
                }
            }

            return permissions;
        }

        public static bool HasRoleAnyPermission(int roleID)
        {
            DataTable dt = GetPermissionsRecordsByRoleID(roleID);
            return dt.Rows.Count > 0;
        }

        public static int GetRolePermissionsCount(int roleID)
        {
            DataTable dt = GetPermissionsRecordsByRoleID(roleID);
            return dt.Rows.Count;
        }

        public static bool DoesRoleHasPermission(int RoleID , int PermissionNumber)
        {
            return clsPermissionsRecordsData.DoesRoleHasPermission(RoleID, PermissionNumber);
        }

        //// Validation methods
        //public static bool ValidateRolePermissionCombination(int roleID, int permissionID)
        //{
        //    // Add business rules here, for example:
        //    // - Check if role and permission exist
        //    // - Check if combination is allowed by business rules
        //    // - Prevent circular dependencies, etc.

        //    return clsRole.IsRoleExist(roleID) && clsPermission.IsPermissionExist(permissionID);
        //}

        //public bool IsRecentlyUpdated()
        //{
        //    return (DateTime.Now - this.LastUpdate).TotalHours < 24; // Updated in last 24 hours
        //}

        //public override string ToString()
        //{
        //    return $"Permissions Record #{PermissionsID} (Role: {RoleID}, Permission: {PermissionID})";
        //}

        //public string GetDetailedInfo()
        //{
        //    string roleName = RoleInfo?.PermissionName ?? "Unknown Role";
        //    string permissionName = PermissionInfo?.PermissionName ?? "Unknown Permission";

        //    return $"Permissions Record ID: {PermissionsID}\n" +
        //           $"Role: {roleName} (ID: {RoleID})\n" +
        //           $"Permission: {permissionName} (ID: {PermissionID})\n" +
        //           $"Last Updated: {LastUpdate}";
        //}
    }
}
