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
    public partial class Ceque_Data : Form
    {
        public Ceque_Data()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("Please Fill All The Data");
            }
            else
            {
                string connection = "datasource=localhost;port=3306;username=root;password=;";
                string query = "insert into ps.Cheque_data(Cheque_No,bank,ammount,date) values ('" + textBox3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "') ;";
                MySqlConnection mycon = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, mycon);
                MySqlDataReader myreader;
                try
                {
                    mycon.Open();
                    myreader = cmd.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myreader.Read())
                    {

                    }
                    mycon.Close();
                    clear_all();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clear_all()
        {
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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
