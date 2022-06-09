using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRINT_SHOP
{
    class getData
    {
        public MySqlDataAdapter returnData(string query)
        {
            MySqlConnection con = new MySqlConnection(connection.con);
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                con.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                con.Close();
                return sda;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }
}
