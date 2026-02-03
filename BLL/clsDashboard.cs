using DataAccessLayer;
using DataAccessLayer.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class clsDashboard
    {
        public static DataTable GetCirculationStatistics()
        {
            return clsDashboardData.GetCirculationStatistics();
        }

        public static DataTable GetFinesStatistics()
        {
            return clsDashboardData.GetFinesStatistics();
           
        }
    }
}
