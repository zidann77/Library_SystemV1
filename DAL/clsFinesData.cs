using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class clsFinesData
    {
        public static bool getFineById(int FineID, ref int ByUser, ref int BorrowingRecordID, ref short NumberOfLateDays,
            ref decimal FineAmount, ref bool? PaymentStatus, ref bool? PaymentWay, ref string Detailes)
        {
            bool Found = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Fines WHERE FineID = @FineID";

            SqlCommand Comm = new SqlCommand(query, conn);

            Comm.Parameters.AddWithValue("@FineID", FineID);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;

                    ByUser = (int)reader["ByUser"];
                    BorrowingRecordID = (int)reader["BorrowingRecordID"];
                    NumberOfLateDays = (short)reader["NumberOfLateDays"];
                    FineAmount = (decimal)reader["FineAmount"];

                    if (reader["PaymentStatus"] != DBNull.Value)
                        PaymentStatus = (bool)reader["PaymentStatus"];
                    else
                        PaymentStatus = null;

                    if (reader["PaymentWay"] != DBNull.Value)
                        PaymentWay = (bool)reader["PaymentWay"];
                    else
                        PaymentWay = null;

                    Detailes = reader["Detailes"].ToString();
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

        public static int addNewFine(int ByUser, int BorrowingRecordID, short NumberOfLateDays, decimal FineAmount,
            bool? PaymentStatus, bool? PaymentWay, string Detailes)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Fines
                (ByUser, BorrowingRecordID, NumberOfLateDays, FineAmount, PaymentStatus, PaymentWay, Detailes)
                VALUES (@ByUser, @BorrowingRecordID, @NumberOfLateDays, @FineAmount, @PaymentStatus, @PaymentWay, @Detailes);
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@ByUser", ByUser);
            command.Parameters.AddWithValue("@BorrowingRecordID", BorrowingRecordID);
            command.Parameters.AddWithValue("@NumberOfLateDays", NumberOfLateDays);
            command.Parameters.AddWithValue("@FineAmount", FineAmount);

            if (PaymentStatus.HasValue)
                command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus.Value);
            else
                command.Parameters.AddWithValue("@PaymentStatus", DBNull.Value);

            if (PaymentWay.HasValue)
                command.Parameters.AddWithValue("@PaymentWay", PaymentWay.Value);
            else
                command.Parameters.AddWithValue("@PaymentWay", DBNull.Value);

            command.Parameters.AddWithValue("@Detailes", Detailes);

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

        public static bool updateFine(int FineID, int ByUser, int BorrowingRecordID, short NumberOfLateDays, decimal FineAmount,
            bool? PaymentStatus, bool? PaymentWay, string Detailes)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Fines
                SET ByUser = @ByUser,
                    BorrowingRecordID = @BorrowingRecordID,
                    NumberOfLateDays = @NumberOfLateDays,
                    FineAmount = @FineAmount,
                    PaymentStatus = @PaymentStatus,
                    PaymentWay = @PaymentWay,
                    Detailes = @Detailes
                WHERE FineID = @FineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);
            command.Parameters.AddWithValue("@ByUser", ByUser);
            command.Parameters.AddWithValue("@BorrowingRecordID", BorrowingRecordID);
            command.Parameters.AddWithValue("@NumberOfLateDays", NumberOfLateDays);
            command.Parameters.AddWithValue("@FineAmount", FineAmount);

            if (PaymentStatus.HasValue)
                command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus.Value);
            else
                command.Parameters.AddWithValue("@PaymentStatus", DBNull.Value);

            if (PaymentWay.HasValue)
                command.Parameters.AddWithValue("@PaymentWay", PaymentWay.Value);
            else
                command.Parameters.AddWithValue("@PaymentWay", DBNull.Value);

            command.Parameters.AddWithValue("@Detailes", Detailes);

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

        public static bool deleteFine(int FineID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Fines WHERE FineID = @FineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);

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

        public static DataTable getAllFines()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM FinesView ORDER BY FineID";

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

        public static DataTable getFinesByUserId(int ByUser)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Fines WHERE ByUser = @ByUser ORDER BY FineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ByUser", ByUser);

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

        public static bool isFineExist(int FineID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Fines WHERE FineID = @FineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);

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
