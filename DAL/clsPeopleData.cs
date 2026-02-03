using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsPeopleData
    {
        public static bool getPersonById(int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref string Phone, ref int CountryID,
            ref string Email, ref string ImageURL, ref bool Ismale)
        {
            bool Found = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand Comm = new SqlCommand(query, conn);

            Comm.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    CountryID = (int)reader["CountryID"];

                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    //ImageURL: allows null in database so we should handle null
                    if (reader["ImageURL"] != DBNull.Value)
                    {
                        ImageURL = (string)reader["ImageURL"];
                    }
                    else
                    {
                        ImageURL = "";
                    }

                    Ismale = (bool)reader["Ismale"];
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

        public static int addNewPerson(string FirstName, string SecondName, string ThirdName,
            string LastName, string Phone, int CountryID, string Email, string ImageURL, bool Ismale)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People 
                   (FirstName, SecondName, ThirdName, LastName, Phone, CountryID, Email, ImageURL, Ismale)
                   VALUES (@FirstName, @SecondName, @ThirdName, @LastName, @Phone, @CountryID, @Email, @ImageURL, @Ismale);
                   SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (ImageURL != "" && ImageURL != null)
                command.Parameters.AddWithValue("@ImageURL", ImageURL);
            else
                command.Parameters.AddWithValue("@ImageURL", System.DBNull.Value);

            command.Parameters.AddWithValue("@Ismale", Ismale);

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

        public static bool updatePerson(int PersonID, string FirstName, string SecondName,
            string ThirdName, string LastName, string Phone, int CountryID, string Email, string ImageURL, bool Ismale)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE People 
                   SET FirstName = @FirstName, 
                       SecondName = @SecondName, 
                       ThirdName = @ThirdName, 
                       LastName = @LastName, 
                       Phone = @Phone, 
                       CountryID = @CountryID,
                       Email = @Email,
                       ImageURL = @ImageURL,
                       Ismale = @Ismale
                   WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (ImageURL != "" && ImageURL != null)
                command.Parameters.AddWithValue("@ImageURL", ImageURL);
            else
                command.Parameters.AddWithValue("@ImageURL", System.DBNull.Value);

            command.Parameters.AddWithValue("@Ismale", Ismale);

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

        public static bool deletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable getAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM PersonDetails ORDER BY PersonID";

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

        public static bool isPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool isPersonExist(string Email)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE Email = @Email";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", Email);

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
