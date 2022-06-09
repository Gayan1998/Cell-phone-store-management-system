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
    public partial class Track__phone_by_imei : Form
    {
        public Track__phone_by_imei()
        {
            InitializeComponent();
        }
        DataTable dataset;

        private void Track__phone_by_imei_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("In Stock");
            comboBox1.Items.Add("Sold");
            comboBox1.Items.Add("Returned");
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(comboBox1.SelectedIndex == 0)
            {
                try
                {
                    var getdata = new getData();
                    MySqlDataAdapter sda = getdata.returnData("Select item.item_id , item.item_name , manage_imie.imei from item inner join manage_imie on item.item_id = manage_imie.Item_ID where manage_imie.status = '" + "In Stock" + "' ;");
                    dataset = new DataTable();
                    sda.Fill(dataset);
                    BindingSource bsource = new BindingSource();
                    bsource.DataSource = dataset;
                    dataGridView1.DataSource = bsource;
                    sda.Update(dataset);

                    this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           else if(comboBox1.SelectedIndex == 1)
            {

                try
                {
                    var getdata = new getData();
                    MySqlDataAdapter sda = getdata.returnData("Select item.item_id , item.item_name , manage_imie.imei , manage_imie.invoice_no from item inner join manage_imie on item.item_id = manage_imie.Item_ID where manage_imie.status = '" + "Sold" + "' ;");
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
                catch 
                {
                    
                }
            }else if(comboBox1.SelectedIndex == 2)
            {
                try
                {
                    var getdata = new getData();
                    MySqlDataAdapter sda = getdata.returnData("Select item.item_id , item.item_name , manage_imie.imei , manage_imie.invoice_no from item inner join manage_imie on item.item_id = manage_imie.Item_ID where manage_imie.status = '" + "Returned" + "' ;");
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
                catch
                {

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("Item_Name LIKE '%{0}%'", textBox1.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("imei LIKE '%{0}%'", textBox2.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
