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
    public partial class find_bill_v : Form
    {
        public find_bill_v()
        {
            InitializeComponent();
        }

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private void find_bill_v_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from item ");
                dr.Fill(dt);

                DataTable dt1 = new DataTable();
                dr = get.returnData("select * from detialed_invoice where inv_id ='" + id.ToString() + "'");
                dr.Fill(dt1);

                DataTable dt3 = new DataTable();
                dr = get.returnData("select * from invoice where id  = '" + id.ToString() + "'");
                dr.Fill(dt3);

                Bill_rpt cr2 = new Bill_rpt();
                cr2.Database.Tables["item"].SetDataSource(dt);
                cr2.Database.Tables["detialed_invoice"].SetDataSource(dt1);
                cr2.Database.Tables["invoice"].SetDataSource(dt3);
                this.crystalReportViewer1.ReportSource = cr2; 
            }
            catch
            {

            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
