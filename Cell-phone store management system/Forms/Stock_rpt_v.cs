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
    public partial class Stock_rpt_v : Form
    {
        public Stock_rpt_v()
        {
            InitializeComponent();
        }
        private int val;

        public int Val
        {
            get { return val; }
            set { val = value; }
        }

        private void Stock_rpt_v_Load(object sender, EventArgs e)
        {
            if(val == 1)
            {
                load_sock();
            }
            else if (val == 2)
            {
                load_sog();
            }
            else if (val == 3)
            {
                Load_sales();
            }
            else if (val == 4)
            {
                load_profit();
            }
            else if(val == 5)
            {
                load_cashbox();
            }
            else if(val == 6)
            {
                load_outstanding_payment();
            }
            else if (val == 7)
            {
                load_outstanding_cheques();
            }
        }
    

        private void load_outstanding_cheques()
        {
            
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from cheque_data ");
                dr.Fill(dt);

                Outstanding_cheque cr2 = new Outstanding_cheque();
                cr2.Database.Tables["cheque_data"].SetDataSource(dt);
                this.crystalReportViewer1.ReportSource = cr2;
            }
            catch
            {

            }
        }

        private void load_outstanding_payment()
        {
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from cust ");
                dr.Fill(dt); 
                
                DataTable dt1 = new DataTable();
                dr = get.returnData("select * from invoice where balance < 0 ");
                dr.Fill(dt1);

                outsanding_payments_rpt cr2 = new outsanding_payments_rpt();
                cr2.Database.Tables["cust"].SetDataSource(dt);
                cr2.Database.Tables["invoice"].SetDataSource(dt1);
                this.crystalReportViewer1.ReportSource = cr2;
            }
            catch
            {

            }
        }

        private void load_cashbox()
        {
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from cash_box ");
                dr.Fill(dt);

                Cash_box_rpt cr2 = new Cash_box_rpt();
                cr2.Database.Tables["Cash_box"].SetDataSource(dt);
                this.crystalReportViewer1.ReportSource = cr2;
            }
            catch
            {

            }
        }

        private void load_profit()
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
                dr = get.returnData("select * from detialed_invoice ");
                dr.Fill(dt1);

                DataTable dt3 = new DataTable();
                dr = get.returnData("select * from invoice ");
                dr.Fill(dt3);

                Profit_rpt cr2 = new Profit_rpt();
                cr2.Database.Tables["item"].SetDataSource(dt);
                cr2.Database.Tables["detialed_invoice"].SetDataSource(dt1);
                cr2.Database.Tables["invoice"].SetDataSource(dt3);
                this.crystalReportViewer1.ReportSource = cr2;
            }
            catch
            {

            }
        }

        private void Load_sales()
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
                dr = get.returnData("select * from detialed_invoice ");
                dr.Fill(dt1);

                sales_rpt cr2 = new sales_rpt();
                cr2.Database.Tables["item"].SetDataSource(dt);
                cr2.Database.Tables["detialed_invoice"].SetDataSource(dt1);
                this.crystalReportViewer1.ReportSource = cr2;
            }
            catch
            {

            }
        }

        private void load_sog()
        {
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from grn where Qty < 5 ");
                dr.Fill(dt);

                DataTable dt1 = new DataTable();
                dr = get.returnData("select * from item");
                dr.Fill(dt1);

                sortageofgoods cr1 = new sortageofgoods();
                cr1.Database.Tables["item"].SetDataSource(dt1);
                cr1.Database.Tables["grn"].SetDataSource(dt);
                this.crystalReportViewer1.ReportSource = cr1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void load_sock()
        {
            this.WindowState = FormWindowState.Maximized;
            MySqlDataAdapter dr;
            try
            {
                DataTable dt = new DataTable();
                var get = new getData();
                dr = get.returnData("select * from grn");
                dr.Fill(dt);

                DataTable dt1 = new DataTable();
                dr = get.returnData("select * from item");
                dr.Fill(dt1);

                stock_report cr1 = new stock_report();
                cr1.Database.Tables["item"].SetDataSource(dt1);
                cr1.Database.Tables["grn"].SetDataSource(dt);
                this.crystalReportViewer1.ReportSource = cr1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
