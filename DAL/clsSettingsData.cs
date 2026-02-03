using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    namespace DataAccessLayer
    {
        public class clsSettingsData
        {
            // READ
            public static bool getSettings(ref byte DefualtBorrrowDays, ref byte DefualtFinePerDays)
            {
                bool Found = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = "SELECT TOP 1 * FROM Settings";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Found = true;
                        DefualtBorrrowDays = (byte)reader["DefualtBorrrowDays"];
                        DefualtFinePerDays = (byte)reader["DefualtFinePerDays"];
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Found = false;
                }
                finally
                {
                    connection.Close();
                }

                return Found;
            }

            // CREATE
            public static bool addSettings(byte DefualtBorrrowDays, byte DefualtFinePerDays)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"INSERT INTO Settings 
                            (DefualtBorrrowDays, DefualtFinePerDays)
                            VALUES (@Days, @Fine)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Days", DefualtBorrrowDays);
                command.Parameters.AddWithValue("@Fine", DefualtFinePerDays);

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

            // UPDATE
            public static bool updateSettings(byte DefualtBorrrowDays, byte DefualtFinePerDays)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"UPDATE Settings
                            SET DefualtBorrrowDays = @Days,
                                DefualtFinePerDays = @Fine";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Days", DefualtBorrrowDays);
                command.Parameters.AddWithValue("@Fine", DefualtFinePerDays);

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

            // DELETE
            public static bool deleteSettings()
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = "DELETE FROM Settings";

                SqlCommand command = new SqlCommand(query, connection);

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

            // EXISTS (optional but useful)
            public static bool isSettingsExist()
            {
                bool isFound = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = "SELECT Found = 1 FROM Settings";

                SqlCommand command = new SqlCommand(query, connection);

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

}
