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
    public partial class Item_Register : Form
    {
        public Item_Register()
        {
            InitializeComponent();
        }
        DataTable dataset;
        DataTable dataset2;
        private void Item_Register_Load(object sender, EventArgs e)
        {
            load_datagrid();
            load_cmb();
            comboBox3.Items.Add("Cell Phones");
            comboBox3.Items.Add("Accessories");
            comboBox4.Items.Add("Cell Phones");
            comboBox4.Items.Add("Accessories");
            this.dataGridView1.VirtualMode = Enabled;
        }

        private void  load_cmb()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select * from supplier");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2 != null)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        string name;
                        name = row["name"].ToString();
                        comboBox1.Items.Add(name);
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
 
        private void load_datagrid()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select * from item");
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
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            }
            catch 
            {
                
            }
        }

        int i_id;
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)|| string.IsNullOrEmpty(textBox4.Text)|| string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please Fill All The Data");
            }
            else
            {
                try
                {
                    var ins = new insertData();
                    i_id=ins.insert("insert into item(Item_id ,Item_name,cost,cost_code,rate,wholesale,discount,supplier,warranty,category) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox10.Text + "','" + textBox3.Text + "','" + textBox7.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "') ;");
                    add_stock();
                    if(comboBox3.SelectedIndex == 0)
                    {
                        Add_imei se = new Add_imei();
                        se.MdiParent = this.MdiParent;
                        se.ID = i_id;
                        se.Qty = int.Parse(textBox6.Text);
                        se.Show();
                    }
                    clear_all();
                    load_datagrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


    private void add_stock()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox6.Text))
                {
                }
                else
                {
                    try
                    {
                        var ins = new insertData();
                        ins.insert("insert into grn(Item_id ,qty) values ('" + i_id + "','" + textBox6.Text + "') ;");
                        MessageBox.Show("Saved");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch
            {
            }
        }

        private void clear_all()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox10.Clear();
            textBox7.Clear();
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    textBox1.Text = row.Cells["Item_id"].Value.ToString();
                    textBox2.Text = row.Cells["Item_name"].Value.ToString();
                    textBox3.Text = row.Cells["rate"].Value.ToString();
                    textBox4.Text = row.Cells["cost"].Value.ToString();
                    textBox5.Text = row.Cells["discount"].Value.ToString();
                    textBox7.Text = row.Cells["wholesale"].Value.ToString();
                    textBox10.Text = row.Cells["cost_code"].Value.ToString();
                    comboBox1.Text = row.Cells["supplier"].Value.ToString();
                    comboBox2.Text = row.Cells["warranty"].Value.ToString();
                    comboBox3.Text = row.Cells["category"].Value.ToString();
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var up = new updatData();
                up.update("update item set  Item_name ='" + textBox2.Text + "',cost ='" + textBox4.Text + "',cost_code='" + textBox10.Text + "',rate ='" + textBox3.Text + "',wholesale = '" + textBox7.Text + "',discount ='" + textBox5.Text + "' ,supplier ='" + comboBox1.Text + "', warranty = '" + comboBox2.Text + "',category = '" + comboBox3.Text + "'  where Item_id  ='" + textBox1.Text + "';");
                MessageBox.Show("Updated");
                load_datagrid();
                clear_all();
                button2.Enabled = false;
                button3.Enabled = false;
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
                delete_item();
                delete_stock();
                load_datagrid();
                clear_all();
                MessageBox.Show("Deleted");
                button3.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void delete_stock()
        {
            try
            {
                var de = new deleteData();
                de.delete("delete from stock where  Item_id  ='" + textBox1.Text + "' ;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_item()
        {
            try
            {
                var de = new deleteData();
                de.delete("delete from item where  Item_id  ='" + textBox1.Text + "' ;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Please Enter Valid Number of Copies");
            }
            else
            {
                Load_barcode();
                clear_all();
                
            }
        }

        private void Load_barcode()
        {
            int copies = int.Parse(textBox8.Text);
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from item where Item_id  = '" + textBox1.Text + "' ");
                dr.Fill(dt);

                barcode_rpt cr2 = new barcode_rpt();
                cr2.Database.Tables["item"].SetDataSource(dt);
                cr2.PrintToPrinter(copies, false, 0, 0);
            }
            catch
            {

            }
            ActiveControl = textBox2;
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(dataset is null)
                {
                    MessageBox.Show("null");
                }
                else
                {
                    DataView Dv = new DataView(dataset);
                    Dv.RowFilter = string.Format("Item_name LIKE '%{0}%'", textBox9.Text);
                    dataGridView1.DataSource = Dv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear_all();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select * from item  where category ='" + comboBox4.Text + "';");
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
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
