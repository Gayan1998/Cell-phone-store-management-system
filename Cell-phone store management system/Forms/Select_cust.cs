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
    public partial class Select_cust : Form
    {
        public Select_cust()
        {
            InitializeComponent();
        }
        DataTable dataset;
        private int val;

        public int Val
        {
            get { return val; }
            set { val = value; }
        }

        private void Select_cust_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void load_data()
        {
            try
            {
                var getdata = new getData();
                MySqlDataAdapter sda = getdata.returnData("Select id,name from cust;");
                dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dataset);
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
                DataView Dv = new DataView(dataset);
                Dv.RowFilter = string.Format("name LIKE '%{0}%'", textBox1.Text);
                dataGridView1.DataSource = Dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int cust_id = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(val == 1)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    cust_id = int.Parse(row.Cells["id"].Value.ToString());
                    Whole_Sales ws = new Whole_Sales();
                    ws.MdiParent = this.MdiParent;
                    ws.Cust = cust_id;
                    ws.Show();
                    this.Close();
                }
            }else if (val == 2)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    cust_id = int.Parse(row.Cells["id"].Value.ToString());
                    Settle_Balance ws = new Settle_Balance();
                    ws.MdiParent = this.MdiParent;
                    ws.Cust = cust_id;
                    ws.Show();
                    this.Close();
                }
            }
        }
    }
}
