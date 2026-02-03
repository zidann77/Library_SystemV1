using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPermissionData
    {
        public static bool GetPermissionInfoByPermissionID(int PermissionID, ref string PermissionName,
            ref int PermissionNumber, ref string Description)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Persmissions WHERE PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PermissionName = (string)reader["PermissionName"];
                    PermissionNumber = (int)reader["PermissionNumber"];
                    Description = (reader["Description"] == DBNull.Value) ? string.Empty : (string)reader["Description"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPermissionInfoByPermissionNumber(int PermissionNumber, ref int PermissionID,
            ref string PermissionName, ref string Description)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Persmissions WHERE PermissionNumber = @PermissionNumber";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionNumber", PermissionNumber);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PermissionID = (int)reader["PermissionID"];
                    PermissionName = (string)reader["PermissionName"];
                    Description = (reader["Description"] == DBNull.Value) ? string.Empty : (string)reader["Description"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPermissionInfoByPermissionName(string PermissionName, ref int PermissionID,
            ref int PermissionNumber, ref string Description)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Persmissions WHERE PermissionName = @PermissionName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionName", PermissionName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PermissionID = (int)reader["PermissionID"];
                    PermissionNumber = (int)reader["PermissionNumber"];
                    Description = (reader["Description"] == DBNull.Value) ? string.Empty : (string)reader["Description"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewPermission(string PermissionName, int PermissionNumber, string Description)
        {
            int PermissionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Persmissions (PermissionName, PermissionNumber, Description)
                         VALUES (@PermissionName, @PermissionNumber, @Description);
                         SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionName", PermissionName);
            command.Parameters.AddWithValue("@PermissionNumber", PermissionNumber);
            command.Parameters.AddWithValue("@Description",
                string.IsNullOrEmpty(Description) ? (object)DBNull.Value : Description);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PermissionID = insertedID;
                }
            }
            catch
            {
                // Log error here if needed
            }
            finally
            {
                connection.Close();
            }

            return PermissionID;
        }

        public static bool UpdatePermission(int PermissionID, string PermissionName,
            int PermissionNumber, string Description)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Persmissions 
                         SET PermissionName = @PermissionName, 
                             PermissionNumber = @PermissionNumber, 
                             Description = @Description
                         WHERE PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionName", PermissionName);
            command.Parameters.AddWithValue("@PermissionNumber", PermissionNumber);
            command.Parameters.AddWithValue("@Description",
                string.IsNullOrEmpty(Description) ? (object)DBNull.Value : Description);
            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePermission(int PermissionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Persmissions WHERE PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                // Log error here if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllPermissions()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Persmissions ORDER BY PermissionNumber";

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
            catch
            {
                // Log error here if needed
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsPermissionExist(int PermissionID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Persmissions WHERE PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPermissionExist(string PermissionName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Persmissions WHERE PermissionName = @PermissionName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionName", PermissionName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPermissionExistByNumber(int PermissionNumber)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Persmissions WHERE PermissionNumber = @PermissionNumber";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionNumber", PermissionNumber);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetPermissionsByRoleID(int RoleID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Assuming you have a RolePermissions table that links roles to permissions
            string query = @"SELECT p.* 
                         FROM Persmissions p
                         INNER JOIN RolePermissions rp ON p.PermissionID = rp.PermissionID
                         WHERE rp.RoleID = @RoleID
                         ORDER BY p.PermissionNumber";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);

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
            catch
            {
                // Log error here if needed
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
