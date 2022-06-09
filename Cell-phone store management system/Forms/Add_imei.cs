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
    public partial class Add_imei : Form
    {
        public Add_imei()
        {
            InitializeComponent();
        }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = textBox1.Text;
                this.dataGridView1.Rows.Add(id);
                textBox1.Clear();
                check_qty();
                ActiveControl = textBox1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                check_qty();
            }
        }

        private void check_qty()
        {
            int row =  int.Parse(dataGridView1.Rows.Count.ToString());
            if(row == qty)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string imei;
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    imei = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    var ins = new insertData();
                    ins.insert(" insert into ps.manage_imie (Item_ID,imei,status) values('" + id + "','" + imei + "','" + "In Stock" + "');");
                }
                MessageBox.Show("Done");
                Clear_all();
                this.Close();
            }
            catch
            {

            }
        }

        private void Clear_all()
        {
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }
    }
}
