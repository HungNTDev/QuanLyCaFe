using System.Data;
using System.Data.SqlClient;

namespace DAL_QLCaFe
{
    public class DAL_Table : DBConnect
    {
        private static DAL_Table instance;

        public static DAL_Table Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_Table();
                return DAL_Table.instance;
            }
            private set
            {
                DAL_Table.instance = value;
            }
        }
        SqlDataAdapter SDA;
        DataTable data = new DataTable();

        public DataTable ListTable()
        {
            _conn.Open();
            string TruyVan = @"exec sp_ListTable";
            SDA = new SqlDataAdapter(TruyVan, _conn);
            data = new DataTable();
            SDA.Fill(data);
            return data;
            _conn.Close();
        }
    }
}
