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
    public partial class Return_from_bill : Form
    {
        public Return_from_bill()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        DataTable dataset;
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    get_pname();
                }
                else
                {
                    get_pname();
                    get_price();
                }
            }
        }

        string imei;

        private void get_price()
        {
           try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from detialed_invoice where inv_id ='" + textBox1.Text + "' and item_id  = '" + this.textBox2.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach (DataRow row in dataset.Rows)
                    {
                        string price;
                        price = row["price"].ToString();
                        textBox4.Text = price;
                        imei = row["imei"].ToString();
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

        decimal cost;
        private void get_pname()
        {
            
           try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where  Item_id = '" + this.textBox2.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach (DataRow row in dataset.Rows)
                    {
                        string pname;
                        pname = row["Item_name"].ToString();
                        label3.Text = pname;
                        cost = decimal.Parse(row["cost"].ToString());
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void add_to_datagrid()
        {
            try
            {
                string item_id = textBox2.Text;
                int qty = int.Parse(textBox3.Text);
                string name = label3.Text;
                decimal price = decimal.Parse(textBox4.Text);
                decimal tota_cost = cost * qty;
                decimal total = qty * price;
                this.dataGridView1.Rows.Add(item_id, qty, name, total, tota_cost,imei);

                cal_total_return();
                cal_total_return_cost();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        decimal profit_loss = 0;
        decimal total_cost = 0;
        private void cal_total_return_cost()
        {
            try
            {
                decimal sum = 0;

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    sum = sum + Convert.ToInt32(dataGridView1.Rows[row].Cells[4].Value);
                }
                total_cost = sum;
                
            }
            catch
            {

            }
        }

        private void cal_total_return()
        {
            try
            {
                decimal sum = 0;

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    sum = sum + Convert.ToInt32(dataGridView1.Rows[row].Cells[3].Value);
                }
                label5.Text = sum.ToString();
            }
            catch
            {

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    add_to_datagrid();
                    textBox2.Clear();
                    textBox3.Clear();
                    label3.Text = "Item Name";
                    textBox4.Clear();
                    button2.Enabled = true;
                }
                catch
                {

                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                reduce_from_cash_box();
                update_stock();
                find_imei();
                clear_all();
                button2.Enabled = false;
            }
            else
            {
                decimal i = decimal.Parse(label5.Text);
                profit_loss = i - total_cost;
                Reduse_from_invoice();
                reduse_from_detialed_invoice();
                reduce_from_cash_box();
                update_stock();
                find_imei();
                clear_all();
                button2.Enabled = false;
            }
        }

        private void find_imei()
        {
            try
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    int i = int.Parse(dataGridView1.Rows[row].Cells[0].Value.ToString());
                    string imie = dataGridView1.Rows[row].Cells[4].Value.ToString();
                    if (string.IsNullOrEmpty(imie))
                    {

                    }
                    else
                    {
                        Update_imei(imie);
                    }

                }


            }
            catch
            {

            }
        }

        private void Update_imei(string imie)
        {
            try
            {
                var up = new updatData();
                up.update("update manage_imie set  status ='" + "In Stock" + "',invoice_no ='" + 0 + "' where imei  ='" + imei + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reduce_from_cash_box()
        {
            if(bal >= 0)
            {
                decimal amount = decimal.Parse(label5.Text);
                decimal redues_amount = 0 - amount;
                string d = DateTime.Today.ToString("yyyy-MM-dd");
                try
                {
                    var ins = new insertData();
                    ins.insert("insert into Cash_box(slip_id,type,amount,date) values ('" + textBox1.Text + "','" + "Return" + "','" + redues_amount + "','" + d + "') ;");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void clear_all()
        {
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }

        private void update_stock()
        {
            
            try
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    string item_id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    int sto_qty = int.Parse(dataGridView1.Rows[row].Cells[1].Value.ToString());
                    var up = new updatData();
                    up.update("update grn set  Qty = Qty + " + sto_qty + "  where item_id ='" + item_id + "';");

                }
            }
            catch
            {

            }
        }

        private void reduse_from_detialed_invoice()
        {
            ;
            try
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    string item_id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    float sto_qty = float.Parse(dataGridView1.Rows[row].Cells[1].Value.ToString());
                    var up = new updatData();
                    up.update("update detialed_invoice set  qty = qty - " + sto_qty + "  where item_id ='" + item_id + "'  and  inv_id = '" + textBox1.Text + "' ");

                }
            }
            catch
            {

            }
        }

        private void Reduse_from_invoice()
        {
            try
            {
               
                decimal totalreturn = decimal.Parse(label5.Text);
                var up = new updatData();
                up.update("update invoice set sub_total = sub_total - " + totalreturn + " , profit = profit -'" + profit_loss + "' , balance = balance + '" + totalreturn + "' where id  ='" + textBox1.Text + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void Return_from_bill_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                get_bal();
            }
        }

        decimal bal;
        private void get_bal()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from invoice where  id  = '" + this.textBox1.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach (DataRow row in dataset.Rows)
                    { 
                        bal = int.Parse(row["balance"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No Item Found");
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                cal_total_return();
                cal_total_return_cost();
            }
        }
    }
}
