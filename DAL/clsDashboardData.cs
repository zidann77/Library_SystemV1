using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public  class clsDashboardData
    {
        public static DataTable GetCirculationStatistics()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand comm = new SqlCommand("sp_DashboardCounts", conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                dt.Load(reader);
            }
            catch
            {
                dt = new DataTable();
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static DataTable GetFinesStatistics()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand comm = new SqlCommand("sp_FinesDashboardStats", conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                dt.Load(reader);
            }
            catch
            {
                dt = new DataTable();
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
