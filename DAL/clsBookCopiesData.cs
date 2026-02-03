using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsBookCopiesData
    {
        public static bool getCopyById(int CopyID, ref int BookID, ref bool AvailabilityStatus, ref bool? Reserved)
        {
            bool Found = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT BookID, AvailabilityStatus, Reserved
                                 FROM BookCopies
                                 WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;
                                BookID = (int)reader["BookID"];
                                AvailabilityStatus = (bool)reader["AvailabilityStatus"];
                                Reserved = reader["Reserved"] == DBNull.Value
                                    ? (bool?)null
                                    : (bool)reader["Reserved"];
                            }
                        }
                    }
                }
                catch
                {
                    Found = false;
                }
            }

            return Found;
        }

        public static bool GetAvailableCopyForBorrowing(int BookID, ref int CopyID)
        {
            bool Found = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT TOP 1 CopyID
                                 FROM BookCopies
                                 WHERE BookID = @BookID
                                   AND AvailabilityStatus = 1
                                   AND Reserved = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;
                                CopyID = (int)reader["CopyID"];
                            }
                        }
                    }
                }
                catch
                {
                    Found = false;
                }
            }

            return Found;
        }

        public static bool GetAvailableCopyForReserving(int BookID, ref int CopyID)
        {
            bool Found = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"select top 1 * from BookCopies where 
                        reserved =0 and AvailabilityStatus = 0 and BookID = @BookID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;
                                CopyID = (int)reader["CopyID"];
                            }
                        }
                    }
                }
                catch
                {
                    Found = false;
                }
            }

            return Found;
        }

        public static int addNewCopy(int BookID, bool AvailabilityStatus, bool? Reserved)
        {
            int newID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO BookCopies (BookID, AvailabilityStatus, Reserved)
                                 VALUES (@BookID, @AvailabilityStatus, @Reserved);
                                 SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);
                        command.Parameters.AddWithValue("@AvailabilityStatus", AvailabilityStatus);
                        command.Parameters.AddWithValue("@Reserved", (object)Reserved ?? DBNull.Value);

                        object result = command.ExecuteScalar();
                        if (result != null)
                            newID = Convert.ToInt32(result);
                    }
                }
                catch
                {
                    newID = -1;
                }
            }

            return newID;
        }

        public static bool updateCopy(int CopyID, int BookID, bool AvailabilityStatus, bool? Reserved)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE BookCopies
                                 SET BookID = @BookID,
                                     AvailabilityStatus = @AvailabilityStatus,
                                     Reserved = @Reserved
                                 WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        command.Parameters.AddWithValue("@BookID", BookID);
                        command.Parameters.AddWithValue("@AvailabilityStatus", AvailabilityStatus);
                        command.Parameters.AddWithValue("@Reserved", (object)Reserved ?? DBNull.Value);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return rowsAffected > 0;
        }

        public static bool deleteCopy(int CopyID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM BookCopies WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return rowsAffected > 0;
        }

        public static DataTable getAllCopies()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT CopyID, BookID, AvailabilityStatus, Reserved
                                 FROM BookCopies
                                 ORDER BY CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
                catch { }
            }

            return dt;
        }

        public static Dictionary<int, (int BookID, bool AvailabilityStatus, bool? Reserved)> getCopiesByBookId(int BookID)
        {
            Dictionary<int, (int, bool, bool?)> copies = new Dictionary<int, (int, bool, bool?)>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT CopyID, BookID, AvailabilityStatus, Reserved
                                 FROM BookCopies
                                 WHERE BookID = @BookID
                                 ORDER BY CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                copies.Add(
                                    (int)reader["CopyID"],
                                    (
                                        (int)reader["BookID"],
                                        (bool)reader["AvailabilityStatus"],
                                        reader["Reserved"] == DBNull.Value ? null : (bool?)reader["Reserved"]
                                    )
                                );
                            }
                        }
                    }
                }
                catch { }
            }

            return copies;
        }

        public static Dictionary<int, bool?> getCopiesByBookIdBasic(int BookID)
        {
            Dictionary<int, bool?> copies = new Dictionary<int, bool?>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT CopyID, Reserved
                                 FROM BookCopies
                                 WHERE BookID = @BookID
                                 ORDER BY CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                copies.Add(
                                    (int)reader["CopyID"],
                                    reader["Reserved"] == DBNull.Value ? null : (bool?)reader["Reserved"]
                                );
                            }
                        }
                    }
                }
                catch { }
            }

            return copies;
        }

        public static bool isCopyExist(int CopyID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM BookCopies WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        return Convert.ToInt32(command.ExecuteScalar()) > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool isCopyExistForBook(int BookID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM BookCopies WHERE BookID = @BookID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);
                        return Convert.ToInt32(command.ExecuteScalar()) > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static int getCopiesCountByBookId(int BookID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM BookCopies WHERE BookID = @BookID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int getAvailableCopiesCount(int BookID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT COUNT(*)
                                 FROM BookCopies
                                 WHERE BookID = @BookID
                                   AND AvailabilityStatus = 1
                                   AND Reserved = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static bool deleteCopiesByBookId(int BookID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM BookCopies WHERE BookID = @BookID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", BookID);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return rowsAffected > 0;
        }

        public static bool updateCopyAvailability(int CopyID, bool AvailabilityStatus)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE BookCopies
                                 SET AvailabilityStatus = @AvailabilityStatus
                                 WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        command.Parameters.AddWithValue("@AvailabilityStatus", AvailabilityStatus);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return rowsAffected > 0;
        }

        public static bool updateCopyReserved(int CopyID, bool? Reserved)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE BookCopies
                                 SET Reserved = @Reserved
                                 WHERE CopyID = @CopyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        command.Parameters.AddWithValue("@Reserved", (object)Reserved ?? DBNull.Value);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return rowsAffected > 0;
        }
    }

}
