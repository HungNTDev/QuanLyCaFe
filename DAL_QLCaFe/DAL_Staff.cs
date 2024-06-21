using DTO_QLCaFe;
using System.Data;
using System.Data.SqlClient;

namespace DAL_QLCaFe
{
    public class DAL_Staff : DBConnect
    {
        private static DAL_Staff instance;

        public static DAL_Staff Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_Staff();
                return instance;
            }
            private set { instance = value; }
        }

        public DAL_Staff() { }


        SqlDataAdapter SDA;

        DataTable data = new DataTable();

        public bool AcceptLogin(DTO_Staff staff)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_AcceptLogin @Email = '" + staff.Email + "', @Password = '" + staff.Password + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                if (SDA.Fill(data) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally { _conn.Close(); }
        }

        public DataTable Check(DTO_Staff staff)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_ForgotPassword @Email = '" + staff.Email + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                return data;
            }
            finally { _conn.Close(); }
        }

        public bool ForgotPassword(DTO_Staff staff)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_ForgotPassword @Email = '" + staff.Email + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                if (SDA.Fill(data) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally { _conn.Close(); }

        }

        public bool UpdatePassword(DTO_Staff staff)
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_UpdatePassword @Email = '" + staff.Email + "', @Password = '" + staff.Password + "'";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                if (SDA.Fill(data) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally { _conn.Close(); }
        }

        public DataTable ListProfileStaff()
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_ListProfileStaff";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                return data;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataTable ListProfileStaff_DGV()
        {
            try
            {
                _conn.Open();
                string TruyVan = @"exec sp_ListProfileStaff_DGV";
                SDA = new SqlDataAdapter(TruyVan, _conn);
                data = new DataTable();
                SDA.Fill(data);
                return data;
            }
            finally { _conn.Close(); }
        }
    }
}
