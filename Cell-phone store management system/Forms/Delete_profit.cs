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
    public partial class Delete_profit : Form
    {
        public Delete_profit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var up = new updatData();
                up.update("update invoice set  profit ='" + "0" + "' where date  >='" + dateTimePicker1.Text + "' and  date<= '" + dateTimePicker2.Text + "';");
                MessageBox.Show("Saved");
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
