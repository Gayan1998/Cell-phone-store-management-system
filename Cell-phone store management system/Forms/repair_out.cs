using MySql.Data.MySqlClient;
using PRINT_SHOP.repot;
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
    public partial class repair_out : Form
    {
        public repair_out()
        {
            InitializeComponent();
        }
        DataTable dataset;
        private void repair_out_Load(object sender, EventArgs e)
        {
            load_datagrid();
        }

        private void load_datagrid()
        {
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            MySqlConnection mycon = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("Select * from ps.repair;", mycon);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int repair_id = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    repair_id = int.Parse(row.Cells["id"].Value.ToString());
                    textBox1.Text = row.Cells["cust_name"].Value.ToString();
                    textBox2.Text = row.Cells["contact_no"].Value.ToString();
                    textBox3.Text = row.Cells["manufacture"].Value.ToString();
                    textBox4.Text = row.Cells["fault"].Value.ToString();
                    button2.Enabled = true;
                    button1.Enabled = true;
                    button3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("cust_name LIKE '%{0}%'", textBox5.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("contact_no LIKE '%{0}%'", textBox6.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_bill();
        }

        private void load_bill()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update_database();
            save_to_invoice();
            clear_all();
        }

        private void clear_all()
        {
            textBox7.Clear();
            textBox4.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
        }

        private void save_to_invoice()
        {
            string d = DateTime.Today.ToString("yyyy-MM-dd");
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            string query = "insert into ps.cash_box(slip_id,type,amount,date) values ('" + repair_id + "','" + "Repair" + "','" + textBox7.Text + "','" + d + "') ;";
            MySqlConnection mycon = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, mycon);
            MySqlDataReader myreader;
            try
            {
                mycon.Open();
                myreader = cmd.ExecuteReader();
                mycon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_database()
        {
            string d = DateTime.Today.ToString("yyyy-MM-dd");
            string connection = "datasource=localhost;port=3306;username=root;password=;";
            string query = "update ps.repair set  cost = '"+textBox7.Text+ "', out_date ='"+d+"' where id = '"+repair_id+"';";
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
                load_datagrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete", "Do you want to remove this Item ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string connection = "datasource=localhost;port=3306;username=root;password=;";
                string query = "delete from ps.repair where  id  ='" + repair_id + "' ;";
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
                load_datagrid();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                string connection = "datasource=localhost;port=3306;username=root;password=;";
                MySqlConnection mycon = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand("Select cust_name,contact_no,manufacture,model,ime,fault,in_date from ps.repair where cost = 0;", mycon);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else if(comboBox1.SelectedIndex == 1)
            {
                string connection = "datasource=localhost;port=3306;username=root;password=;";
                MySqlConnection mycon = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand("Select * from ps.repair where cost > 0;", mycon);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
