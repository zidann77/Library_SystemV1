using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountryData
    { 
        public static bool getCountryById(int CountryID, ref string CountryName)
        {
            bool Found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    CountryName = (string)reader["CountryName"];
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

         public static bool getCountryByName(ref int CountryID,  string CountryName)
        {
            bool Found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    CountryID = (int)reader["CountryID"];
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

        public static int addNewCountry(string CountryName)
        {
            int CountryID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Countries (CountryName)
                     VALUES (@CountryName);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                conn.Open();
                object result = Comm.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    CountryID = id;
                }
            }
            catch (Exception ex)
            {
                CountryID = -1;
            }
            finally
            {
                conn.Close();
            }
            return CountryID;
        }

        public static bool updateCountry(int CountryID, string CountryName)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Countries 
                     SET CountryName = @CountryName
                     WHERE CountryID = @CountryID";

            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryID", CountryID);
            Comm.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                conn.Open();
                rowsAffected = Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                conn.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool deleteCountry(int CountryID)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Countries WHERE CountryID = @CountryID";
            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                conn.Open();
                rowsAffected = Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                conn.Close();
            }
            return (rowsAffected > 0);
        }

        public static DataTable getAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries ORDER BY CountryID";
            SqlCommand Comm = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static bool isCountryExist(int CountryID)
        {
            bool exists = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Countries WHERE CountryID = @CountryID";
            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                conn.Open();
                object result = Comm.ExecuteScalar();
                exists = (result != null);
            }
            catch (Exception ex)
            {
                exists = false;
            }
            finally
            {
                conn.Close();
            }
            return exists;
        }

        public static bool isCountryExist(string CountryName)
        {
            bool exists = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Countries WHERE CountryName = @CountryName";
            SqlCommand Comm = new SqlCommand(query, conn);
            Comm.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                conn.Open();
                object result = Comm.ExecuteScalar();
                exists = (result != null);
            }
            catch (Exception ex)
            {
                exists = false;
            }
            finally
            {
                conn.Close();
            }
            return exists;
        }
    }
}
