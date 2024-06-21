using DTO_QLCaFe;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DAL_QLCaFe
{
    public class DAL_Log: DBConnect
    {
        private static DAL_Log instance;

        public static DAL_Log Instance
        {
            get { if (instance == null) instance = new DAL_Log(); return DAL_Log.instance; }
            private set { DAL_Log.instance = value; }
        }

        SqlDataAdapter SDA;
        DataTable data = new DataTable();

        public DataTable Log(DTO_Log log)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_Log @Object = N'" + log.Object1 + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                return data;
            }
            finally { _conn.Close(); }
        }

        public DataTable Logs(DTO_Log log)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_Logs @Object1 = N'" + log.Object1 + "',  @Object2 = N'" + log.Object2 + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                return data;
            }
            finally { _conn.Close(); }
        }
    }
}
