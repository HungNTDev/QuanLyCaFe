using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLCaFe
{
    public class DBConnect
    {
        protected SqlConnection _conn = new SqlConnection("Data Source=ADMIN\\TUANHUNG;Initial Catalog=QuanLyCafe;Integrated Security=True;Encrypt=False"); 
    }
}
