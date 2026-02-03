using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsRole
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public clsRole()
        {
            RoleID = -1;
            RoleName = "";
            Description = "";
        }

        public clsRole(int roleID, string roleName, string description)
        {
            RoleID = roleID;
            RoleName = roleName;
            Description = description;
        }

        private bool _LoadRoleData(int RoleID)
        {
            string roleName = "", description = "";

            bool found = DataAccessLayer.clsRolesData.getRoleById(RoleID, ref roleName, ref description);

            if (found)
            {
                this.RoleID = RoleID;
                this.RoleName = roleName;
                this.Description = description;
            }

            return found;
        }

        public static clsRole Find(int RoleID)
        {
            clsRole role = new clsRole();

            if (role._LoadRoleData(RoleID))
                return role;
            else
                return null;
        }

        public static clsRole Find(string RoleName)
        {
            // First get the RoleID from the RoleName
            DataTable dt = GetAllRoles();

            foreach (DataRow row in dt.Rows)
            {
                if (row["RoleName"].ToString().Equals(RoleName, StringComparison.OrdinalIgnoreCase))
                {
                    int roleID = Convert.ToInt32(row["RoleID"]);
                    return Find(roleID);
                }
            }

            return null;
        }

        public bool Save()
        {
            if (RoleID == -1)
            {
                // Add new role
                return _AddNewRole();
            }
            else
            {
                // Update existing role
                return _UpdateRole();
            }
        }

        private bool _AddNewRole()
        {
            // Validate data before saving
            if (!_ValidateData())
                return false;

            int newRoleID = DataAccessLayer.clsRolesData.addNewRole(RoleName, Description);

            if (newRoleID != -1)
            {
                RoleID = newRoleID;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _UpdateRole()
        {
            // Validate data before updating
            if (!_ValidateData())
                return false;

            return DataAccessLayer.clsRolesData.updateRole(RoleID, RoleName, Description);
        }

        public bool Delete()
        {
            if (RoleID == -1)
                return false;

            return DataAccessLayer.clsRolesData.deleteRole(RoleID);
        }

        private bool _ValidateData()
        {
            // Validate RoleName
            if (string.IsNullOrWhiteSpace(RoleName))
            {
                throw new Exception("Role name cannot be empty");
            }

            if (RoleName.Length > 100) // Assuming database limit
            {
                throw new Exception("Role name cannot exceed 100 characters");
            }

            // Validate Description length if provided
            if (!string.IsNullOrEmpty(Description) && Description.Length > 500)
            {
                throw new Exception("Description cannot exceed 500 characters");
            }

            // Check if role name already exists (for new roles or when name changes)
            if ((RoleID == -1 || _IsRoleNameChanged()) && DataAccessLayer.clsRolesData.isRoleExist(RoleName))
            {
                throw new Exception($"Role name '{RoleName}' already exists");
            }

            return true;
        }

        private bool _IsRoleNameChanged()
        {
            if (RoleID == -1)
                return true;

            clsRole originalRole = Find(RoleID);
            if (originalRole != null)
            {
                return !originalRole.RoleName.Equals(RoleName, StringComparison.OrdinalIgnoreCase);
            }

            return true;
        }

        public static DataTable GetAllRoles()
        {
            return DataAccessLayer.clsRolesData.getAllRoles();
        }

        public static Dictionary<int, string> GetRolesBasicInfo()
        {
            return DataAccessLayer.clsRolesData.getRolesBasicInfo();
        }

        public static Dictionary<int, (string RoleName, string Description)> GetAllRolesAsDictionary()
        {
            return DataAccessLayer.clsRolesData.getAllRolesAsDictionary();
        }

        public static bool DoesRoleExist(int RoleID)
        {
            return DataAccessLayer.clsRolesData.isRoleExist(RoleID);
        }

        public static bool DoesRoleExist(string RoleName)
        {
            return DataAccessLayer.clsRolesData.isRoleExist(RoleName);
        }
    }
}