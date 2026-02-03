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
        public class clsBorrowingData
        {
            // ======================= Get By ID =======================

            public static bool GetBorrowingByID(int id,
                ref DateTime borrowingDate,
                ref int personID,
                ref int copyID,
                ref int? fineID,
                ref DateTime EndDate,
                ref DateTime? actualReturnDate,
                ref int byUser,
                ref string detailes)
            {
                bool isFound = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM Borrowings WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        borrowingDate = (DateTime)reader["BorrowingDate"];
                        personID = (int)reader["PersonID"];
                        copyID = (int)reader["CopyID"];
                        EndDate = (DateTime)reader["Duration"];
                        byUser = (int)reader["ByUser"];

                        fineID = reader["FineID"] == DBNull.Value ? null : (int?)reader["FineID"];
                        actualReturnDate = reader["ActualReturnDate"] == DBNull.Value
                            ? null
                            : (DateTime?)reader["ActualReturnDate"];

                        detailes = reader["Detailes"] == DBNull.Value
                            ? ""
                            : (string)reader["Detailes"];
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

            // ======================= Add =======================

            public static int AddNewBorrowing(DateTime borrowingDate, int personID, int copyID,
                int? fineID, DateTime duration, DateTime? actualReturnDate,
                int byUser, string detailes)
            {
                int newID = -1;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"
                INSERT INTO Borrowings
                (BorrowingDate, PersonID, CopyID, FineID, Duration, ActualReturnDate, ByUser, Detailes)
                VALUES
                (@BorrowingDate, @PersonID, @CopyID, @FineID, @Duration, @ActualReturnDate, @ByUser, @Detailes);
                SELECT SCOPE_IDENTITY();
            ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@BorrowingDate", borrowingDate);
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@CopyID", copyID);
                command.Parameters.AddWithValue("@FineID", (object)fineID ?? DBNull.Value);
                command.Parameters.AddWithValue("@Duration", duration);
                command.Parameters.AddWithValue("@ActualReturnDate", (object)actualReturnDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ByUser", byUser);
                command.Parameters.AddWithValue("@Detailes", string.IsNullOrEmpty(detailes) ? DBNull.Value : (object)detailes);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                        newID = Convert.ToInt32(result);
                }
                catch
                {
                    newID = -1;
                }
                finally
                {
                    connection.Close();
                }

                return newID;
            }

            // ======================= Update =======================

            public static bool UpdateBorrowing(int id, DateTime borrowingDate, int personID, int copyID,
                int? fineID, DateTime duration, DateTime? actualReturnDate,
                int byUser, string detailes)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"
                UPDATE Borrowings
                SET
                    BorrowingDate = @BorrowingDate,
                    PersonID = @PersonID,
                    CopyID = @CopyID,
                    FineID = @FineID,
                    Duration = @Duration,
                    ActualReturnDate = @ActualReturnDate,
                    ByUser = @ByUser,
                    Detailes = @Detailes
                WHERE ID = @ID
            ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@BorrowingDate", borrowingDate);
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@CopyID", copyID);
                command.Parameters.AddWithValue("@FineID", (object)fineID ?? DBNull.Value);
                command.Parameters.AddWithValue("@Duration", duration);
                command.Parameters.AddWithValue("@ActualReturnDate", (object)actualReturnDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ByUser", byUser);
                command.Parameters.AddWithValue("@Detailes", string.IsNullOrEmpty(detailes) ? DBNull.Value : (object)detailes);

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

                return rowsAffected > 0;
            }

            // ======================= Delete =======================

            public static bool DeleteBorrowing(int id)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"DELETE FROM Borrowings WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

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

                return rowsAffected > 0;
            }

            // ======================= Get All =======================

            public static DataTable GetAllBorrowings()
            {
                DataTable dt = new DataTable();

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM Borrowings";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        dt.Load(reader);

                    reader.Close();
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                }

                return dt;
            }

            // ======================= Exists =======================

            public static bool IsBorrowingExist(int id)
            {
                bool exists = false;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT 1 FROM Borrowings WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    exists = (result != null);
                }
                catch
                {
                    exists = false;
                }
                finally
                {
                    connection.Close();
                }

                return exists;
            }
        }
    }

}
