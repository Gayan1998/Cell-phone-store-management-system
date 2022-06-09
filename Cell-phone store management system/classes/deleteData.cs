using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRINT_SHOP
{
    class deleteData
    {
     public void delete(string query)
        {
            MySqlConnection mycon = new MySqlConnection(connection.con);
            MySqlCommand cmd = new MySqlCommand(query, mycon);
            MySqlDataReader myreader;
            try
            {
                mycon.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                }
                mycon.Close();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }   
    }
}
