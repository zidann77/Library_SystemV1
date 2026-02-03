using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsUserData
    {
        public static bool GetUserInfoByUserID(int UserID, ref string UserName,
            ref string Password, ref DateTime LastState, ref int CurrentRoleID,
            ref int PersonID, ref int LastRole, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    LastState = (DateTime)reader["LastState"];
                    CurrentRoleID = (int)reader["CurrentRoleID"];
                    PersonID = (int)reader["PersonID"];
                    LastRole = (int)reader["LastRole"];
                    IsActive = (bool)reader["IsActive"];
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

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
            ref string Password, ref DateTime LastState, ref int CurrentRoleID, ref int LastRole, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    LastState = (DateTime)reader["LastState"];
                    CurrentRoleID = (int)reader["CurrentRoleID"];
                    LastRole = (int)reader["LastRole"];
                    IsActive = (bool)reader["IsActive"];
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

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
            ref int UserID, ref DateTime LastState, ref int CurrentRoleID, ref int PersonID, ref int LastRole, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    LastState = (DateTime)reader["LastState"];
                    CurrentRoleID = (int)reader["CurrentRoleID"];
                    PersonID = (int)reader["PersonID"];
                    LastRole = (int)reader["LastRole"];
                    IsActive = (bool)reader["IsActive"];
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

  

        public static bool UpdateUser(int UserID, string UserName, string Password,
            DateTime LastState, int CurrentRoleID, int PersonID, int LastRole, bool IsActive)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                     SET UserName = @UserName, 
                         Password = @Password, 
                         LastState = @LastState, 
                         CurrentRoleID = @CurrentRoleID, 
                         PersonID = @PersonID, 
                         LastRole = @LastRole,
                         IsActive = @IsActive
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@LastState", LastState);
            command.Parameters.AddWithValue("@CurrentRoleID", CurrentRoleID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LastRole", LastRole);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM UsersListView";

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

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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
        public static int AddNewUser(string UserName, string Password, DateTime LastState,
    int CurrentRoleID, int PersonID, int LastRole, bool IsActive = true)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (UserName, Password, LastState, CurrentRoleID, PersonID, LastRole, IsActive)
                   VALUES (@UserName, @Password, @LastState, @CurrentRoleID, @PersonID, @LastRole, @IsActive);
                   SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@LastState", LastState);
            command.Parameters.AddWithValue("@CurrentRoleID", CurrentRoleID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LastRole", LastRole);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
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

            return UserID;
        }
      
        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                     SET Password = @Password 
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Password", NewPassword);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool UpdateLastStateAndRole(int UserID, DateTime LastState, int CurrentRoleID, int LastRole)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                     SET LastState = @LastState, 
                         CurrentRoleID = @CurrentRoleID, 
                         LastRole = @LastRole
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LastState", LastState);
            command.Parameters.AddWithValue("@CurrentRoleID", CurrentRoleID);
            command.Parameters.AddWithValue("@LastRole", LastRole);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        // NEW METHODS FOR IsActive COLUMN

        public static bool ActivateUser(int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                     SET IsActive = 1 
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool DeactivateUser(int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                     SET IsActive = 0 
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserActive(int UserID)
        {
            bool isActive = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT IsActive FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isActive = (bool)result;
                }
            }
            catch
            {
                isActive = false;
            }
            finally
            {
                connection.Close();
            }

            return isActive;
        }

        public static DataTable GetActiveUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE IsActive = 1";

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

        public static DataTable GetInactiveUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE IsActive = 0";

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

        public static bool GetUserInfoByUsernameAndPasswordActiveOnly(string UserName, string Password,
            ref int UserID, ref DateTime LastState, ref int CurrentRoleID, ref int PersonID, ref int LastRole, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password AND IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    LastState = (DateTime)reader["LastState"];
                    CurrentRoleID = (int)reader["CurrentRoleID"];
                    PersonID = (int)reader["PersonID"];
                    LastRole = (int)reader["LastRole"];
                    IsActive = (bool)reader["IsActive"];
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
    }
}
