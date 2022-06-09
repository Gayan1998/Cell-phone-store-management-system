using MySql.Data.MySqlClient;
using PRINT_SHOP.repot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRINT_SHOP
{
    public partial class Whole_Sales : Form
    {
        public Whole_Sales()
        {
            InitializeComponent();
        }

        private int cust;

        public int Cust
        {
            get { return cust; }
            set { cust = value; }
        }

        DataTable dataset;
        DataTable dataset2;
        DataTable dataset3;
        private void Retial_Sales_Load(object sender, EventArgs e)
        {
            LOad_table();
            textBox2.Text = cust.ToString();
            comboBox2.Items.Add("Retail");
            comboBox2.Items.Add("Wholesale");
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void LOad_table()
        {
            string connection = "Server=localhost;Database=ps;User Id=root;Password=;";
            MySqlConnection mycon = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("Select Item_id,Item_Name from ps.item;", mycon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                dataset3 = new DataTable();
                sda.Fill(dataset3);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dataset3;
                dataGridView1.DataSource = bsource;
                sda.Update(dataset3);
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView Dv = new DataView(dataset3);
                Dv.RowFilter = string.Format("Item_Name LIKE '%{0}%'", textBox1.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Select_cust se = new Select_cust();
            se.MdiParent = this.MdiParent;
            se.Val = 1;
            se.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Pay Cannot be empty");
            }
            else
            {
                Save_invoice();
                save_detialed_invoice();
                save_cashbox();
                update_stock();
                Delete_sold_4ns();
                MessageBox.Show("Done");
                clear_all_2();
                button2.Enabled = false;
                button3.Enabled = true;
                ActiveControl = textBox3;
                mqty = 0;

            }
        }

        private void Delete_sold_4ns()
        {
            try
            {
                for (int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                    int i = int.Parse(dataGridView2.Rows[row].Cells[0].Value.ToString());
                    string imie = dataGridView2.Rows[row].Cells[7].Value.ToString();
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


        private void Update_imei(string imei)
        {
            try
            {
                var up = new updatData();
                up.update("update manage_imie set  status ='" + "Sold" + "',invoice_no ='" + invoice_id + "' where imei  ='" + imei + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void save_cashbox()
        {
            try
            {
                decimal i = decimal.Parse(textBox8.Text);
                if (i > 0)
                {
                    try
                    {
                        var ins = new insertData();
                        ins.insert("insert into cash_box(slip_id,type,amount,date) values ('" + invoice_id + "','" + "Cash Sale" + "','" + subTotal + "','" + dateTimePicker1.Text + "') ;");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear_all_2()
        {
            dataGridView2.Rows.Clear();
            label8.Text = "0.00";
            label10.Text = "0.00";
            label16.Text = "0";
            textBox8.Clear();
            textBox3.Clear();
        }

        private void update_stock()
        {
            try
            {
                for (int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                    string item_id = dataGridView2.Rows[row].Cells[0].Value.ToString();
                    float sto_qty = int.Parse(dataGridView2.Rows[row].Cells[4].Value.ToString());
                    var up = new updatData();
                    up.update("update grn set  qty = qty - " + sto_qty + "  where Item_id ='" + item_id.ToString() + "' ");
                }
            }
            catch
            {

            }
        }

        private void save_detialed_invoice()
        {
            try
            {
                string item_id;
                for (int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                    
                    item_id = dataGridView2.Rows[row].Cells[0].Value.ToString();
                    decimal price = decimal.Parse(dataGridView2.Rows[row].Cells[2].Value.ToString());
                    int qty = int.Parse(dataGridView2.Rows[row].Cells[4].Value.ToString());
                    string imei = dataGridView2.Rows[row].Cells[7].Value.ToString();
                    decimal dis = decimal.Parse(dataGridView2.Rows[row].Cells[3].Value.ToString());
                    var ins = new insertData();
                    ins.insert(" insert into detialed_invoice (inv_id, item_id, price,discount,qty,date,imei) values ('" + invoice_id + "','" + item_id + "', '" + price + "','" + dis + "','" + qty + "','" + dateTimePicker1.Text + "','" + imei + "');");
                }
            }
            catch
            {
                
            }
        }

        int invoice_id;
        private void Save_invoice()
        {
            try
            {
                var ins = new insertData();
                invoice_id = ins.insert("insert into invoice(cust_id,sub_total,profit,pay,balance,payment_type,date) values ('" + textBox2.Text + "','" + subTotal + "','" + total_profit + "','" + pay + "','" + label10.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "');");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load_bill();
            button3.Enabled = false;
            ActiveControl = textBox3;
        }

        private void load_bill()
        {
            print_bill();
        }

        private void print_bill()
        {
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from item ");
                dr.Fill(dt);

                DataTable dt1 = new DataTable();
                dr = get.returnData("select * from detialed_invoice where inv_id ='" + invoice_id.ToString() + "'");
                dr.Fill(dt1);

                DataTable dt3 = new DataTable();
                dr = get.returnData("select * from invoice where id  = '" + invoice_id.ToString() + "'");
                dr.Fill(dt3);

                Bill_rpt cr2 = new Bill_rpt();
                cr2.Database.Tables["item"].SetDataSource(dt);
                cr2.Database.Tables["detialed_invoice"].SetDataSource(dt1);
                cr2.Database.Tables["invoice"].SetDataSource(dt3);
                cr2.PrintToPrinter(1, false, 0, 0);
                cr2.Dispose();
                cr2.Close();
            }
            catch
            {

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("Please Enter Valid Bill Number");
            }
            else
            {
                int inv_no = int.Parse(textBox12.Text.Trim());
                find_bill_v se = new find_bill_v();
                se.ID = inv_no;
                se.MdiParent = this.MdiParent;
                se.Show();
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (comboBox2.Text == "Retail")
                    {
                        get_retial_info();
                        get_qty();
                        load_cmb();
                    }
                    else
                    {
                        get_item_info_wholesale();
                        get_qty();
                        load_cmb();
                    }
                    
                }
                else if(e.KeyCode == Keys.Space)
                {
                    textBox8.Select();
                }
                else if(e.KeyCode == Keys.F11)
                {
                    print_bill();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int mqty = 0;
        private void get_qty()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from grn where Item_id  = '" + this.textBox3.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach (DataRow row in dataset.Rows)
                    {
 
                        mqty = int.Parse(row["Qty"].ToString());
                    }

                }
                else
                {
                    MessageBox.Show("No Item Found");
                }
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void load_cmb()
        {
            try
            {
                comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from manage_imie where Item_ID ='" + textBox3.Text + "' and status = '" + "In Stock" + "';");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2 != null)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        string name;
                        name = row["imei"].ToString();
                        comboBox3.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("No Item Found");
                }
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string type;
        private void get_retial_info()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where Item_id  = '" + this.textBox3.Text + "' ;");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2.Rows.Count == 1)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        textBox4.Text = row["Item_name"].ToString();
                        textBox5.Text = row["rate"].ToString();
                        textBox13.Text = row["warranty"].ToString();
                        type = row["category"].ToString();
                        textBox6.Text = "0";
                        cost = decimal.Parse(row["Cost"].ToString());
                    }
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("No Item Found");
                    ActiveControl = textBox3;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        decimal cost;
        private void get_item_info_wholesale()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where Item_id  = '" + this.textBox3.Text + "' ;");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2.Rows.Count == 1)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        textBox4.Text = row["Item_name"].ToString();
                        textBox5.Text = row["wholesale"].ToString();
                        textBox13.Text = row["warranty"].ToString();
                        type = row["category"].ToString();
                        textBox6.Text = "0";
                        cost = decimal.Parse(row["Cost"].ToString());
                    }
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("No Item Found");
                    ActiveControl = textBox3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
        }
        decimal subTotal;
        private void cal_sub_total()
        {
            decimal sum = 0;

            for (int row = 0; row < dataGridView2.Rows.Count; row++)
            {
                sum = sum + Convert.ToInt32(dataGridView2.Rows[row].Cells[5].Value);
            }
            const string numericFormat = "###,###,###,###,###,###,###";
            label8.Text = sum.ToString(numericFormat);
            subTotal = sum;
        }

        private void add_to_datagrid()
        {
            try
            {
                string id = textBox3.Text;
                string name = textBox4.Text;
                decimal price = decimal.Parse(textBox5.Text);
                int qty = int.Parse(textBox7.Text);
                decimal dis = decimal.Parse(textBox6.Text);
                string imie = comboBox3.Text;

                this.dataGridView2.Rows.Add(id, name, price, dis, qty, total, profit, imie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        decimal profit;
        decimal total;
        decimal total_cost;
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    int qty1 = int.Parse(textBox7.Text);
                    if (string.IsNullOrEmpty(textBox7.Text) && qty1 < 0)
                    {
                        MessageBox.Show("Qty Cant Be Empty or minus!");
                    }
                    else
                    {
                        int itemID = int.Parse(textBox3.Text);
                        if (type == "Cell Phones")
                        {
                            if (string.IsNullOrEmpty(comboBox3.Text))
                            {
                                MessageBox.Show("Please Enter a Imei numer");
                            }
                            else
                            {
                                decimal price = decimal.Parse(textBox5.Text);
                                int qty = int.Parse(textBox7.Text);
                                total = (price * qty);
                                total_cost = cost * qty;
                                profit = total - total_cost;
                                int qtyInStock = 0;
                                if (dataGridView2.Rows.Count == 1)
                                {
                                    qtyInStock = mqty;
                                }
                                else
                                {
                                    int count = countItem(itemID);
                                    qtyInStock = mqty - count;
                                }
                                if (qty <= qtyInStock)
                                {
                                    add_to_datagrid();
                                    cal_sub_total();
                                    cal_profit();
                                    cal_item_qty();
                                    clear_();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid qty");
                                }
                            }
                        }
                        else
                        {
                            decimal price = decimal.Parse(textBox5.Text);
                            int qty = int.Parse(textBox7.Text);
                            total = (price * qty);
                            total_cost = cost * qty;
                            profit = total - total_cost;
                            int qtyInStock =0;
                            if (dataGridView2.Rows.Count == 1)
                            {
                                qtyInStock = mqty;
                            }
                            else
                            {
                                int count= countItem(itemID);
                                qtyInStock= mqty - count;
                            }
                            if (qty <= qtyInStock)
                            {
                                add_to_datagrid();
                                cal_sub_total();
                                cal_profit();
                                cal_item_qty();
                                clear_();
                            }
                            else
                            {
                                MessageBox.Show("Invalid qty");
                            }
                        }
                    }

                    SendKeys.Send("{TAB}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private int countItem(int id)
        {
            int count = 0;
                for(int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                if (Convert.ToInt32(dataGridView2.Rows[row].Cells[0].Value) == id)
                {

                    count = count + Convert.ToInt32(dataGridView2.Rows[row].Cells[4].Value);
                }
            }  
            return count;
        }

        private void cal_item_qty()
        {
            float sum = 0;

            for (int row = 0; row < dataGridView2.Rows.Count; row++)
            {
                sum = sum + Convert.ToInt32(dataGridView2.Rows[row].Cells[4].Value);
            }
            label16.Text = sum.ToString();
        }

        decimal total_profit;
        private void cal_profit()
        {
            decimal sum = 0;

            for (int row = 0; row < dataGridView2.Rows.Count; row++)
            {
                sum = sum + Convert.ToInt32(dataGridView2.Rows[row].Cells[6].Value);
            }
            total_profit = sum;
            
        }

        private void clear_()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox13.Clear();
            comboBox3.Text = "";
            comboBox3.Items.Clear();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                decimal price = decimal.Parse(textBox5.Text);
                decimal dis = decimal.Parse(textBox6.Text);
                decimal new_price = price - dis;
                textBox5.Text = new_price.ToString();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
                cal_sub_total();
                cal_profit();
                cal_item_qty();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 1)
            {
                label12.Text = "Wholesale";
            }
            else
            {
                label12.Text = "Retail";
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == 13)
                {
                    if (comboBox2.Text == "Retail")
                    {
                        get_retial_price();
                    }
                    else
                    {
                        get_wholesale_price();
                    }
                    decimal dis;
                    decimal p = decimal.Parse(textBox5.Text);
                    dis = r - p;
                    textBox6.Text = dis.ToString();
                    ActiveControl = textBox7;
                }
            }
            catch
            {
                
            }
        }

        decimal r;
        private void get_retial_price()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where Item_id  = '" + this.textBox3.Text + "' ;");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2 != null)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        r = decimal.Parse(row["rate"].ToString());
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

        private void get_wholesale_price()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where Item_id  = '" + this.textBox3.Text + "' ;");
                dataset2 = new DataTable();
                sda.Fill(dataset2);
                if (dataset2 != null)
                {
                    foreach (DataRow row in dataset2.Rows)
                    {
                        r = decimal.Parse(row["wholesale"].ToString());
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

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    decimal sub = decimal.Parse(label8.Text);
                    decimal pay = decimal.Parse(textBox8.Text);
                    decimal bal = pay - sub;
                    label10.Text = bal.ToString();
                    button2.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (string.IsNullOrEmpty(textBox8.Text))
                {
                    MessageBox.Show("Pay Cannot be empty");
                }
                else
                {
                    Save_invoice();
                    save_detialed_invoice();
                    save_cashbox();
                    update_stock();
                    Delete_sold_4ns();
                    MessageBox.Show("Done");
                    clear_all_2();
                    button2.Enabled = false;
                    button3.Enabled = true;
                    ActiveControl = textBox3;
                }
            }
            {
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        decimal pay;
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == "" || textBox8.Text == "0") return;
                decimal number;
                number = decimal.Parse(textBox8.Text, System.Globalization.NumberStyles.Currency);
                textBox8.Text = number.ToString("#,#");
                textBox8.SelectionStart = textBox8.Text.Length;
                pay = decimal.Parse(textBox8.Text);
            }
            catch
            {

            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    textBox3.Text = row.Cells["Item_id"].Value.ToString();
                    textBox3.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
