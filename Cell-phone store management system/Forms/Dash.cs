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
    public partial class Dash : Form
    {
        public Dash()
        {
            InitializeComponent();
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private void Dash_Load(object sender, EventArgs e)
        {
            Whole_Sales child = new Whole_Sales();
            child.MdiParent = this;
            child.Show();
            if (type == "Admin")
            {
                profitReportToolStripMenuItem.Enabled = true;
                salesReportToolStripMenuItem.Enabled = true;
                inventoryManagementToolStripMenuItem.Enabled = true;
                addNewUserToolStripMenuItem.Enabled = true;
                itemRegisterToolStripMenuItem.Enabled = true;
            }
            else if (type == "Top lavel emp")
            {
                inventoryManagementToolStripMenuItem.Enabled = true;
                itemRegisterToolStripMenuItem.Enabled = true;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void itemRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_Register child = new Item_Register();
            child.MdiParent = this;
            child.Show();
        }

        private void customerRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_customer child = new add_customer();
            child.MdiParent = this;
            child.Show();
        }

        private void retialSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Whole_Sales child = new Whole_Sales();
            child.MdiParent = this;
            child.Show();
        }

        private void sattleBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settle_Balance child = new Settle_Balance();
            child.MdiParent = this;
            child.Show();
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GRN child = new GRN();
            child.Type = type;
            child.MdiParent = this;
            child.Show();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Return_from_bill child = new Return_from_bill();
            child.MdiParent = this;
            child.Show();
        }

        private void acceptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repair_in child = new Repair_in();
            child.MdiParent = this;
            child.Show();
        }

        private void repairDeviceDetialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repair_out child = new repair_out();
            child.MdiParent = this;
            child.Show();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 1;
            child.MdiParent = this;
            child.Show();
        }

        private void shortageOfGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 2;
            child.MdiParent = this;
            child.Show();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 3;
            child.MdiParent = this;
            child.Show();
        }

        private void profitReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 4;
            child.MdiParent = this;
            child.Show();
        }

        private void customerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cashBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 5;
            child.MdiParent = this;
            child.Show();
        }

        private void outstandingPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 6;
            child.MdiParent = this;
            child.Show();
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "c:\\backup\\database.sql";
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=ps; convert zero datetime = true;";
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = con;
                            con.Open();
                            mb.ExportToFile(path);
                            con.Close();
                            MessageBox.Show("Backup Compleated");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void enterChequeDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ceque_Data child = new Ceque_Data();
            child.MdiParent = this;
            child.Show();
        }

        private void outstandingChecksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_rpt_v child = new Stock_rpt_v();
            child.Val = 7;
            child.MdiParent = this;
            child.Show();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_new_user child = new add_new_user();
            child.MdiParent = this;
            child.Show();
        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_supplier child = new add_supplier();
            child.MdiParent = this;
            child.Show();
        }

        private void trackPhonesByImeiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Track__phone_by_imei child = new Track__phone_by_imei();
            child.MdiParent = this;
            child.Show();
        }

        private void returnToSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Return_to_Supplier child = new Return_to_Supplier();
            child.MdiParent = this;
            child.Show();
        }

        private void deleteProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete_profit child = new Delete_profit();
            child.MdiParent = this;
            child.Show();
        }

        private void binCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
