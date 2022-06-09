using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRINT_SHOP
{
    public partial class Loging : Form
    {
        public Loging()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Database=ps;User Id=root;Password=;";
                MySqlConnection mycon = new MySqlConnection(connectionString);
                MySqlCommand select = new MySqlCommand("select * from ps.loging where user_name = '" + this.textBox1.Text + "' and password ='" + this.textBox2.Text + "';", mycon);
                MySqlDataReader reader;
                mycon.Open();
                reader = select.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    get_type();
                    MessageBox.Show("welcome");
                    this.Hide();
                    Dash d1 = new Dash();
                    d1.Type = type;
                    d1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid User Name or Password");
                    ActiveControl = textBox1;
                }

                mycon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string type;
        private void get_type()
        {
            try
            {
                string connectionString = "Server=localhost;Database=ps;User Id=root;Password=;";
                MySqlConnection mycon = new MySqlConnection(connectionString);
                MySqlCommand select = new MySqlCommand("select * from ps.loging where user_name = '" + this.textBox1.Text + "' and password = '" + this.textBox2.Text + "';", mycon);
                MySqlDataReader reader2;
                mycon.Open();
                reader2 = select.ExecuteReader();
                if (reader2.Read())
                {
                    type = reader2["type"].ToString();
                }
                else
                {
                    MessageBox.Show("No Item Found");
                }
                mycon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
