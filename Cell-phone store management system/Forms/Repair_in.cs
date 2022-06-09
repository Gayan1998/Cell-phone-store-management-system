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
    public partial class Repair_in : Form
    {
        public Repair_in()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void clear_all()
        {
            foreach (Control t in this.Controls)
            {
                if (t is TextBox)
                {
                    t.Text = "";
                }
            }
            foreach (Control t in this.Controls)
            {
                if (t is CheckBox)
                {
                    ((CheckBox)t).Checked = false;
                }
            }
            foreach (Control t in groupBox1.Controls)
            {
                if (t is CheckBox)
                {
                    ((CheckBox)t).Checked = false;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
     
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please Fill All the data ");
            }
            else
            {
                string s = "";
                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox b = (CheckBox)c;
                        if (b.Checked)
                        {
                            s = b.Text + " , " + s;

                        }
                    }
                }
                string i = "";
                foreach (Control a in groupBox1.Controls)
                {
                    if (a is CheckBox)
                    {
                        CheckBox x = (CheckBox)a;
                        if (x.Checked)
                        {
                            i = x.Text + " , " + i;
                        }
                    }
                }
                string d = DateTime.Today.ToString("yyyy-MM-dd");
                try
                {
                    var ins = new insertData();
                    ins.insert("insert into repair(rp_id,cust_name ,contact_no,manufacture,model,ime,fault,other_issues,in_date,out_date,state,include,serial) values ('" + urid + "','" + textBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + s + "','" + d + "','" + d + "','" + "Pending" + "','" + i + "','" + textBox7.Text + "') ;");
                    clear_all();
                    printRecipt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void printRecipt()
        {
                MySqlConnection mycon = new MySqlConnection(connection.con);
                MySqlCommand cmd;
                MySqlDataAdapter dr;
                try
                {
                    mycon.Open();
                    DataTable dt = new DataTable();
                    cmd = new MySqlCommand("select * from  repair where rp_id  = '" + urid + "' ", mycon);
                    dr = new MySqlDataAdapter(cmd);
                    dr.Fill(dt);
                    mycon.Close();

                    repairInRpt cr2 = new repairInRpt();
                    cr2.Database.Tables["repair"].SetDataSource(dt);
                    cr2.PrintToPrinter(1, false, 0, 0);
                }
                catch
                {

                }
        }

        private void Repair_in_Load(object sender, EventArgs e)
        {
            getLastRPid();
        }
        int urpid = 0;
        string urid = "";
        private void getLastRPid()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(connection.con);
                MySqlCommand query = new MySqlCommand("SELECT id FROM repair ORDER BY id DESC LIMIT 1;", con);
                MySqlDataReader reader2;
                con.Open();
                reader2 = query.ExecuteReader();

                if (reader2.Read())
                {
                    urpid = int.Parse(reader2["id"].ToString());
                    urpid = urpid + 1;
                }
                con.Close();
                urid = "RP" + urpid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox4_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
