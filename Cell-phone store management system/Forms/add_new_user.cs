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
    public partial class add_new_user : Form
    {
        public add_new_user()
        {
            InitializeComponent();
        }

        private void add_new_user_Load(object sender, EventArgs e)
        {
            load_table();
        }
        DataTable dataset;
        private void load_table()
        {
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            MySqlConnection mycon = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("Select * from ps.loging;", mycon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dataset);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            string query = "insert into ps.loging(user_name ,password,type) values ('" + textBox1.Text + "','" + textBox2.Text + "','"+comboBox1.Text+"') ;";
            MySqlConnection mycon = new MySqlConnection(connection);
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
                load_table();
                clear_all();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int uid;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    textBox1.Text = row.Cells["user_name"].Value.ToString();
                    textBox2.Text = row.Cells["password"].Value.ToString();
                    uid = int.Parse(row.Cells["id"].Value.ToString());
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            string query = "update ps.loging set  user_name ='" + textBox1.Text + "',password ='" + textBox2.Text + "',type='" + comboBox1.Text + "' where Item_id  ='" + uid + "';";
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
                load_table();
                clear_all();
                button2.Enabled = false;
                button3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear_all()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete", "Do you want to remove this Item ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string connection = "datasource=localhost;port=3306;username=root;password=;";
                string query = "delete from ps.loging where  id  ='" + uid + "' ;";
                MySqlConnection mycon = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, mycon);
                MySqlDataReader myreader;
                try
                {
                    mycon.Open();
                    myreader = cmd.ExecuteReader();
                    MessageBox.Show("Deleted");
                    while (myreader.Read())
                    {

                    }
                    mycon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }   

}
