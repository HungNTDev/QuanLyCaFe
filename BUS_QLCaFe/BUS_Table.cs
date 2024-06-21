using DAL_QLCaFe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLCaFe
{
    public class BUS_Table
    {
        private static BUS_Table instance;

        public static BUS_Table Instance
        {
            get
            {
                if (instance == null) instance = new BUS_Table();
                return BUS_Table.instance;
            }
            private set { BUS_Table.instance = value; }
        }
        public DataTable ListTable()
        {
            return DAL_Table.Instance.ListTable();
        }
    }
}
