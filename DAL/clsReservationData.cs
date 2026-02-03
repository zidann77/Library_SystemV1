using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsReservationData
    {
        // ======================= Get By ID =======================

        public static bool GetReservationByID(int id,
            ref int personID,
            ref int byUser,
            ref int copyID,
            ref DateTime reservationDate,
            ref string detailes,
            ref byte reservationStatus)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Reservations WHERE ID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = (int)reader["PersonID"];
                    byUser = (int)reader["ByUser"];
                    copyID = (int)reader["CopyID"];
                    reservationDate = (DateTime)reader["ReservationDate"];
                    reservationStatus = (byte)reader["ReservationStatus"];

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


        public static bool GetReservationByBookCopyID(ref int id,
            ref int personID,
            ref int byUser,
             int copyID,
            ref DateTime reservationDate,
            ref string detailes,
            ref byte reservationStatus)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Reservations WHERE CopyID = @CopyID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CopyID", copyID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = (int)reader["PersonID"];
                    byUser = (int)reader["ByUser"];
                    id = (int)reader["ID"];
                    reservationDate = (DateTime)reader["ReservationDate"];
                    reservationStatus = (byte)reader["ReservationStatus"];

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

        public static int AddNewReservation(int personID, int byUser, int copyID,
            DateTime reservationDate, string detailes, byte reservationStatus)
        {
            int newID = -1;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                INSERT INTO Reservations
                (PersonID, ByUser, CopyID, ReservationDate, Detailes, ReservationStatus)
                VALUES
                (@PersonID, @ByUser, @CopyID, @ReservationDate, @Detailes, @ReservationStatus);
                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@ByUser", byUser);
            command.Parameters.AddWithValue("@CopyID", copyID);
            command.Parameters.AddWithValue("@ReservationDate", reservationDate);
            command.Parameters.AddWithValue("@ReservationStatus", reservationStatus);
            command.Parameters.AddWithValue("@Detailes",
                string.IsNullOrEmpty(detailes) ? DBNull.Value : (object)detailes);

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

        public static bool UpdateReservation(int id, int personID, int byUser, int copyID,
            DateTime reservationDate, string detailes, byte reservationStatus)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                UPDATE Reservations
                SET
                    PersonID = @PersonID,
                    ByUser = @ByUser,
                    CopyID = @CopyID,
                    ReservationDate = @ReservationDate,
                    Detailes = @Detailes,
                    ReservationStatus = @ReservationStatus
                WHERE ID = @ID
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@ByUser", byUser);
            command.Parameters.AddWithValue("@CopyID", copyID);
            command.Parameters.AddWithValue("@ReservationDate", reservationDate);
            command.Parameters.AddWithValue("@ReservationStatus", reservationStatus);
            command.Parameters.AddWithValue("@Detailes",
                string.IsNullOrEmpty(detailes) ? DBNull.Value : (object)detailes);

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

        public static bool DeleteReservation(int id)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM Reservations WHERE ID = @ID";

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

        public static DataTable GetAllReservations()
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM ReservationsView";

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

        public static bool IsReservationExist(int id)
        {
            bool exists = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 1 FROM Reservations WHERE ID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                exists = (command.ExecuteScalar() != null);
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
