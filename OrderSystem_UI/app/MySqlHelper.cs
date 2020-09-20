using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------
using MySql.Data.MySqlClient;

namespace OrderSystem_UI
{
    public class MySqlHelper
    {
        MySqlConnection cn;
        public MySqlHelper(string connectionString)
        {
            cn = new MySqlConnection(connectionString);
        }

        public bool isConnection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                    cn.Open();
                return true;
            }
        }
    }
}
