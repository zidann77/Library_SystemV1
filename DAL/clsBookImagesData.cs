using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsBookImagesData
    {
        public static bool getImageById(int ImageID, ref string ImagePath, ref short ImageOrder, ref int BookID)
        {
            bool Found = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM BookImages WHERE ID = @ImageID";

            SqlCommand Comm = new SqlCommand(query, conn);

            Comm.Parameters.AddWithValue("@ImageID", ImageID);

            try
            {
                conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();

                if (reader.Read())
                {
                    Found = true;
                    ImagePath = (string)reader["ImagePath"];
                    ImageOrder = (short)reader["ImageOrder"];
                    BookID = (int)reader["BookID"];
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

        public static int addNewImage(string ImagePath, short ImageOrder, int BookID)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO BookImages 
                (ImagePath, ImageOrder, BookID)
                VALUES (@ImagePath, @ImageOrder, @BookID);
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            command.Parameters.AddWithValue("@ImageOrder", ImageOrder);
            command.Parameters.AddWithValue("@BookID", BookID);

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

        public static bool updateImage(int ImageID, string ImagePath, short ImageOrder, int BookID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE BookImages 
                SET ImagePath = @ImagePath, 
                    ImageOrder = @ImageOrder, 
                    BookID = @BookID
                WHERE ID = @ImageID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ImageID", ImageID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            command.Parameters.AddWithValue("@ImageOrder", ImageOrder);
            command.Parameters.AddWithValue("@BookID", BookID);

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

        public static bool deleteImage(int ImageID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM BookImages WHERE ID = @ImageID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ImageID", ImageID);

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

        public static DataTable getAllImages()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM BookImages ORDER BY ID";

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

        public static DataTable getImagesByBookId(int BookID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM BookImages WHERE BookID = @BookID ORDER BY ImageOrder";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", BookID);

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

        public static bool isImageExist(int ImageID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM BookImages WHERE ID = @ImageID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ImageID", ImageID);

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

        public static bool isImagePathExist(string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM BookImages WHERE ImagePath = @ImagePath";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ImagePath", ImagePath);

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

        public static int getImageCountByBookId(int BookID)
        {
            int count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(*) FROM BookImages WHERE BookID = @BookID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", BookID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int i))
                {
                    count = i;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        public static bool deleteImagesByBookId(int BookID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM BookImages WHERE BookID = @BookID";

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
    }
}
