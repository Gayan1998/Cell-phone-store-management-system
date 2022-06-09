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
    public partial class Return_to_Supplier : Form
    {
        public Return_to_Supplier()
        {
            InitializeComponent();
        }
        DataTable dataset;
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                get_info();
            }
        }

        string type;
        private void get_info()
        {
         try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("select * from item where Item_id  = '" + this.textBox1.Text + "' ;");
                dataset = new DataTable();
                sda.Fill(dataset);
                if (dataset != null)
                {
                    foreach (DataRow row in dataset.Rows)
                    {
                        string pname;
                        string supplier;
                        pname = row["Item_name"].ToString();
                        supplier = row["supplier"].ToString();
                        type = row["category"].ToString();
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
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                add_data();
            }
        }

        private void add_data()
        {
            try
            {
                if(string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Please Fill all the data");
                }
                else
                {
                  if(type == "Cell Phones")
                    {
                        if (string.IsNullOrEmpty(textBox5.Text))
                        {
                            MessageBox.Show("Plleas enter Emei ");
                        }
                        else
                        {
                            int id = int.Parse(textBox1.Text);
                            string name = textBox2.Text;
                            string supp = textBox3.Text;
                            string imei = textBox5.Text;
                            int qty = int.Parse(textBox4.Text);
                            this.dataGridView1.Rows.Add(id, name, supp, imei, qty);
                        }
                    }
                    else
                    {
                        int id = int.Parse(textBox1.Text);
                        string name = textBox2.Text;
                        string supp = textBox3.Text;
                        string imei = textBox5.Text;
                        int qty = int.Parse(textBox4.Text);
                        this.dataGridView1.Rows.Add(id, name, supp, imei, qty);
                    }
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                update_stock();
                reset_imei();
                save_return();
                clear_all();
                MessageBox.Show("Done");
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
            dataGridView1.Rows.Clear();
        }

        private void save_return()
        {
            
            try
            {
                string item_id;
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    string d = DateTime.Today.ToString("yyyy-MM-dd");
                    item_id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    int qty = int.Parse(dataGridView1.Rows[row].Cells[4].Value.ToString());
                    string supp = dataGridView1.Rows[row].Cells[2].Value.ToString();
                    var ins = new insertData();
                    ins.insert(" insert into return_supp (Item_ID, supplier, qty,date) values ('" + item_id + "','" + supp + "','" + qty + "','" + d + "');");
                }
            }
            catch
            {

            }
        }

        private void reset_imei()
        {
            try
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    int i = int.Parse(dataGridView1.Rows[row].Cells[0].Value.ToString());
                    string imie = dataGridView1.Rows[row].Cells[3].Value.ToString();
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
                up.update("update manage_imie set  status ='" + "Returned" + "',invoice_no ='" + "0" + "' where imei  ='" + imie + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_stock()
        {
            try
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    string item_id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    float sto_qty = int.Parse(dataGridView1.Rows[row].Cells[4].Value.ToString());
                    var up = new updatData();
                    up.update("update grn set  qty = qty - " + sto_qty + "  where Item_id ='" + item_id.ToString() + "' ");
                }
            }
            catch
            {

            }
        }
    }
}
