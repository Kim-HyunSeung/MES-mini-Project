using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    public class DBHelper
    {

        public SqlConnection sCon = new SqlConnection(Common.sConn);
        public DBHelper()
        {

            sCon.Open();
        }

        public void Close()
        {
            sCon.Close();
        }


    }
}
