using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsBooksData
    {
        public static bool getBookById(int BookID, ref string Title, ref string ISBN,
         ref DateTime PublicationDate, ref decimal? Rate, ref string Detailes)
        {
            bool Found = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Books WHERE BookID = @BookID";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@BookID", BookID);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    Title = (string)reader["Title"];
                    ISBN = (string)reader["ISBN"];
                    PublicationDate = (DateTime)reader["PublicationDate"];

                    //Rate: allows null in database so we should handle null
                    if (reader["Rate"] != DBNull.Value)
                    {
                        Rate = (decimal)reader["Rate"];
                    }
                    else
                    {
                        Rate = null;
                    }

                    //Detailes: allows null in database so we should handle null
                    if (reader["Detailes"] != DBNull.Value)
                    {
                        Detailes = (string)reader["Detailes"];
                    }
                    else
                    {
                        Detailes = "";
                    }
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

        public static int addNewBook(string Title, string ISBN, DateTime PublicationDate,
            decimal? Rate, string Detailes)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Books 
          (Title, ISBN, PublicationDate, Rate, Detailes)
          VALUES (@Title, @ISBN, @PublicationDate, @Rate, @Detailes);
          SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@ISBN", ISBN);
            command.Parameters.AddWithValue("@PublicationDate", PublicationDate);

            if (Rate.HasValue)
                command.Parameters.AddWithValue("@Rate", Rate.Value);
            else
                command.Parameters.AddWithValue("@Rate", System.DBNull.Value);

            if (Detailes != "" && Detailes != null)
                command.Parameters.AddWithValue("@Detailes", Detailes);
            else
                command.Parameters.AddWithValue("@Detailes", System.DBNull.Value);

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

        public static bool updateBook(int BookID, string Title, string ISBN,
            DateTime PublicationDate, decimal? Rate, string Detailes)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Books 
          SET Title = @Title, 
              ISBN = @ISBN, 
              PublicationDate = @PublicationDate, 
              Rate = @Rate,
              Detailes = @Detailes
          WHERE BookID = @BookID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", BookID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@ISBN", ISBN);
            command.Parameters.AddWithValue("@PublicationDate", PublicationDate);

            if (Rate.HasValue)
                command.Parameters.AddWithValue("@Rate", Rate.Value);
            else
                command.Parameters.AddWithValue("@Rate", System.DBNull.Value);

            if (Detailes != "" && Detailes != null)
                command.Parameters.AddWithValue("@Detailes", Detailes);
            else
                command.Parameters.AddWithValue("@Detailes", System.DBNull.Value);

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

        public static bool deleteBook(int BookID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Books WHERE BookID = @BookID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", BookID);

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

        public static DataTable getAllBooks()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from DTBooksList order by BookID";

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

        public static bool isBookExist(int BookID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Books WHERE BookID = @BookID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", BookID);

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

        public static bool isBookExist(string ISBN)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Books WHERE ISBN = @ISBN";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ISBN", ISBN);

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

        public static DataTable getBookCopiesByBookId(int bookId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT 
                BookCopies.BookID, 
                BookCopies.CopyID, 
                Books.Title, 
                Books.ISBN, 
                BookCopies.AvailabilityStatus
            FROM BookCopies
            INNER JOIN Books ON BookCopies.BookID = Books.BookID
            WHERE BookCopies.BookID = @BookID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                    catch
                    {
                        // ممكن تحط Logging هنا
                    }
                }
            }
            return dt;
        }

    }
}
