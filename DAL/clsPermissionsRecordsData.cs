using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPermissionsRecordsData
    {
        public static bool GetPermissionsRecordInfoByID(int PermissionsID, ref int RoleID,
            ref int PermissionID, ref DateTime LastUpdate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM PermissionsRecords WHERE PermissionsID = @PermissionsID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionsID", PermissionsID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    RoleID = (int)reader["RoleID"];
                    PermissionID = (int)reader["PermissionID"];
                    LastUpdate = (DateTime)reader["LastUpdate"];
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

        public static bool GetPermissionsRecordInfoByRoleAndPermission(int RoleID, int PermissionID,
            ref int PermissionsID, ref DateTime LastUpdate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM PermissionsRecords 
                         WHERE RoleID = @RoleID AND PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PermissionsID = (int)reader["PermissionsID"];
                    LastUpdate = (DateTime)reader["LastUpdate"];
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

        public static DataTable GetPermissionsRecordsByRoleID(int RoleID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT pr.PermissionsID, pr.RoleID, pr.PermissionID, pr.LastUpdate,
                                p.PermissionName, p.PermissionNumber, p.Description
                         FROM PermissionsRecords pr
                         INNER JOIN Persmissions p ON pr.PermissionID = p.PermissionID
                         WHERE pr.RoleID = @RoleID
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

        public static DataTable GetPermissionsRecordsByPermissionID(int PermissionID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT pr.PermissionsID, pr.RoleID, pr.PermissionID, pr.LastUpdate,
                                r.RoleName, r.RoleDescription
                         FROM PermissionsRecords pr
                         INNER JOIN Roles r ON pr.RoleID = r.RoleID
                         WHERE pr.PermissionID = @PermissionID
                         ORDER BY r.RoleName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionID", PermissionID);

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

        public static int AddNewPermissionsRecord(int RoleID, int PermissionID)
        {
            int PermissionsID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO PermissionsRecords (RoleID, PermissionID, LastUpdate)
                         VALUES (@RoleID, @PermissionID, GETDATE());
                         SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
            command.Parameters.AddWithValue("@PermissionID", PermissionID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PermissionsID = insertedID;
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

            return PermissionsID;
        }

        public static bool UpdatePermissionsRecord(int PermissionsID, int RoleID, int PermissionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE PermissionsRecords 
                         SET RoleID = @RoleID, 
                             PermissionID = @PermissionID,
                             LastUpdate = GETDATE()
                         WHERE PermissionsID = @PermissionsID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
            command.Parameters.AddWithValue("@PermissionID", PermissionID);
            command.Parameters.AddWithValue("@PermissionsID", PermissionsID);

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

        public static bool DeletePermissionsRecord(int PermissionsID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM PermissionsRecords WHERE PermissionsID = @PermissionsID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionsID", PermissionsID);

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

        public static bool DeletePermissionsRecordByRoleAndPermission(int RoleID, int PermissionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM PermissionsRecords 
                         WHERE RoleID = @RoleID AND PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
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

        public static bool DeleteAllPermissionsRecordsByRoleID(int RoleID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM PermissionsRecords WHERE RoleID = @RoleID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);

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

        public static bool DeleteAllPermissionsRecordsByPermissionID(int PermissionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM PermissionsRecords WHERE PermissionID = @PermissionID";

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

        public static DataTable GetAllPermissionsRecords()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT pr.PermissionsID, pr.RoleID, pr.PermissionID, pr.LastUpdate,
                                r.RoleName, p.PermissionName
                         FROM PermissionsRecords pr
                         INNER JOIN Roles r ON pr.RoleID = r.RoleID
                         INNER JOIN Persmissions p ON pr.PermissionID = p.PermissionID
                         ORDER BY r.RoleName, p.PermissionNumber";

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

        public static bool IsPermissionsRecordExist(int PermissionsID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM PermissionsRecords WHERE PermissionsID = @PermissionsID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionsID", PermissionsID);

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

        public static bool IsPermissionsRecordExist(int RoleID, int PermissionID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found=1 FROM PermissionsRecords 
                         WHERE RoleID = @RoleID AND PermissionID = @PermissionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
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

        public static bool UpdateLastUpdateTimestamp(int PermissionsID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE PermissionsRecords 
                         SET LastUpdate = GETDATE()
                         WHERE PermissionsID = @PermissionsID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PermissionsID", PermissionsID);

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

        public static bool DoesRoleHavePermission(int RoleID, int PermissionID)
        {
            return IsPermissionsRecordExist(RoleID, PermissionID);
        }

        public static DataTable GetRolePermissionsWithDetails(int RoleID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT p.PermissionID, p.PermissionName, p.PermissionNumber, p.Description,
                                pr.PermissionsID, pr.LastUpdate,
                                HasPermission = CASE WHEN pr.PermissionsID IS NOT NULL THEN 1 ELSE 0 END
                         FROM Persmissions p
                         LEFT JOIN PermissionsRecords pr ON p.PermissionID = pr.PermissionID AND pr.RoleID = @RoleID
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


        public static bool DoesRoleHasPermission( int RoleID, int PermissionNumber)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"
               select found = 1 from PermissionsRecords inner join Persmissions on
             PermissionsRecords.PermissionID = Persmissions.PermissionID and
               PermissionsRecords.RoleID = @RoleID and PermissionNumber = @PermissionNumber";
               
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoleID", RoleID);
            command.Parameters.AddWithValue("@PermissionNumber", PermissionNumber);
         

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


    }
}
