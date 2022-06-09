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
    public partial class add_customer : Form
    {
        public add_customer()
        {
            InitializeComponent();
            load_datagrid();
        }
        int cust_id;
        DataTable dataset;
        private void load_datagrid()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select * from cust;");
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var ins = new insertData();
                ins.insert("insert into cust(Name,address,contact) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "') ;");
                MessageBox.Show("Saved");
                clear_all();
                load_datagrid();
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
            textBox3.Clear();
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

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                cust_id = int.Parse(row.Cells["Id"].Value.ToString());
                textBox1.Text = row.Cells["name"].Value.ToString();
                textBox2.Text = row.Cells["address"].Value.ToString();
                textBox3.Text = row.Cells["contact"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var up = new updatData();
                up.update("update cust set  name ='" + textBox1.Text + "',address ='" + textBox2.Text + "',contact ='" + textBox3.Text + "'  where id ='" + cust_id + "';");
                MessageBox.Show("Updated");
                load_datagrid();
                clear_all();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete","Do you want to remove this customer ?", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    var de = new deleteData();
                    de.delete("delete from cust where  id ='" + cust_id + "' ;");
                    MessageBox.Show("Deleted");
                    load_datagrid();
                    clear_all();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void add_customer_Load(object sender, EventArgs e)
        {

        }
    }
}
