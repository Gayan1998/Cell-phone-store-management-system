using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRINT_SHOP
{
    public class insertData
    {
        public int insert(string query)
        {
            MySqlConnection mycon = new MySqlConnection(connection.con);
            MySqlCommand cmd = new MySqlCommand(query, mycon);
            MySqlDataReader myreader;
            try
            {
                mycon.Open();
                myreader = cmd.ExecuteReader();
               int id = (int)cmd.LastInsertedId;
                while (myreader.Read())
                {

                }
                mycon.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
