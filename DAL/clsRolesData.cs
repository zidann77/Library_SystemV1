using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsRolesData
    {
        public static bool getRoleById(int RoleID, ref string RoleName, ref string Description)
        {
            bool Found = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Roles WHERE RoleID = @RoleID";

            SqlCommand Comm = new SqlCommand(query, conn);

            Comm.Parameters.AddWithValue("@RoleID", RoleID);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    RoleName = (string)reader["RoleName"];

                    //Description: allows null in database so we should handle null
                    if (reader["Description"] != DBNull.Value)
                    {
                        Description = (string)reader["Description"];
                    }
                    else
                    {
                        Description = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Found = false;
            }
            finally
            {
                conn.Close();
            }
            return Found;
        }

        public static int addNewRole(string RoleName, string Description)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Roles 
            (RoleName, Description)
            VALUES (@RoleName, @Description);
            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@RoleName", RoleName);

            if (Description != "" && Description != null)
                command.Parameters.AddWithValue("@Description", Description);
            else
                command.Parameters.AddWithValue("@Description", System.DBNull.Value);

            int id = -1;

            try
            {
                conn.Open();
                object newID = command.ExecuteScalar();

                if (newID != null && int.TryParse(newID.ToString(), out int i))
                {
                    id = i;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        public static bool updateRole(int RoleID, string RoleName, string Description)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Roles 
            SET RoleName = @RoleName, 
                Description = @Description
            WHERE RoleID = @RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
            command.Parameters.AddWithValue("@RoleName", RoleName);

            if (Description != "" && Description != null)
                command.Parameters.AddWithValue("@Description", Description);
            else
                command.Parameters.AddWithValue("@Description", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool deleteRole(int RoleID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Roles WHERE RoleID = @RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable getAllRoles()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT RoleID, RoleName, Description FROM Roles ORDER BY RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static Dictionary<int, (string RoleName, string Description)> getAllRolesAsDictionary()
        {
            Dictionary<int, (string RoleName, string Description)> rolesDict =
                new Dictionary<int, (string RoleName, string Description)>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT RoleID, RoleName, Description FROM Roles ORDER BY RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int roleId = (int)reader["RoleID"];
                    string roleName = (string)reader["RoleName"];

                    string description = "";
                    if (reader["Description"] != DBNull.Value)
                    {
                        description = (string)reader["Description"];
                    }

                    rolesDict.Add(roleId, (roleName, description));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return rolesDict;
        }

        public static Dictionary<int, string> getRolesBasicInfo()
        {
            Dictionary<int, string> rolesDict = new Dictionary<int, string>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT RoleID, RoleName FROM Roles ORDER BY RoleName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int roleId = (int)reader["RoleID"];
                    string roleName = (string)reader["RoleName"];

                    rolesDict.Add(roleId, roleName);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return rolesDict;
        }

        public static bool isRoleExist(int RoleID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Roles WHERE RoleID = @RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool isRoleExist(string RoleName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Roles WHERE RoleName = @RoleName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleName", RoleName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}