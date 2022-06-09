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
    public partial class GRN : Form
    {
        public GRN()
        {
            InitializeComponent();
            load_datagrid();
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        DataTable dataset;
        private void button1_Click(object sender, EventArgs e)
        {
            save_stock();
            update_grn();
            if(category == "Cell Phones")
            {
                Add_imei se = new Add_imei();
                se.MdiParent = this.MdiParent;
                se.ID = int.Parse(textBox1.Text);
                se.Qty = int.Parse(textBox4.Text);
                se.Show();
            }
            clear_all();
            load_datagrid();
            ActiveControl = textBox1;
        }

        private void load_datagrid()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select item.item_id , item.item_name , grn.qty from item inner join grn on item.item_id =grn.Item_id ;");
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

        private void update_grn()
        {
            string d = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                var ins = new insertData();
                ins.insert("insert into g_r_n(Item_id,qty,date) values ('" + textBox1.Text + "','" + textBox4.Text + "','" + d + "') ;");
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
            textBox4.Clear();
        }

        private void save_stock()
        {
            try
            {
                int count = 0;
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from grn where Item_id = '" + this.textBox1.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                foreach (DataRow row in dataset.Rows)
                {
                    count++;
                }

                if (count == 1)
                {
                    try
                    {
                        int sum = int.Parse(textBox4.Text);
                        var up = new updatData();
                        up.update("update grn set qty = qty + " + sum + "  where Item_id ='" + textBox1.Text + "' ");
                        MessageBox.Show("Saved");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (count == 0)
                {

                    try
                    {
                        var ins = new insertData();
                        ins.insert("insert into grn(Item_id,Qty) values ('" + textBox1.Text + "','" + textBox4.Text + "') ;");
                        MessageBox.Show("Saved");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                get_item_data();
            }
        }
        string category;
        private void get_item_data()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select * from item where Item_id = '"+textBox1.Text+"';");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach(DataRow row in dataset.Rows)
                    {
                        string pname;
                        string supplier;
                        pname = row["Item_name"].ToString();
                        supplier = row["supplier"].ToString();
                        category = row["category"].ToString();
                        textBox2.Text = pname;
                        textBox3.Text = supplier;
                    }
               
                }
                else
                {
                    MessageBox.Show("No Item Found");
                }
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
        private void validate_qty()
        {
            int qty = int.Parse(textBox4.Text);
            if(type == "Admin")
            {
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Enter Valid Qty");
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(textBox4.Text) && qty < 0)
                {
                    MessageBox.Show("Enter Valid Qty");
                }
                else
                {
                    button1.Enabled = true;
                }
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                validate_qty();
            }
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                SendKeys.Send("{TAB}");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("Item_Name LIKE '%{0}%'", textBox5.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GRN_Load(object sender, EventArgs e)
        {
            if(type == "Admin")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            } 
            else if (type == "Top lavel emp")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }
    }
}
