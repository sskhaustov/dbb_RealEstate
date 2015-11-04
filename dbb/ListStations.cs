using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace dbb
{
    public partial class ListStations : Form
    {
        private string sconnect;
        private string stm;
        MySqlConnection mconnect = null;
        MySqlDataAdapter da;
        private DataSet ds;
        public ListStations()
        {
            InitializeComponent();
            sconnect = @"server=localhost;port=3306;user=root;password=trehsvyat06;database=mydb2;";
            try
            {
                mconnect = new MySqlConnection(sconnect);
                mconnect.Open();

                stm = "SELECT * FROM MetroList";
                da = new MySqlDataAdapter(stm, mconnect);
                ds = new DataSet();
                da.Fill(ds, "MetroList");
                dataGridView1.DataSource = ds.Tables["MetroList"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void Updator_Click(object sender, EventArgs e)
        {
            try
            {
                mconnect = new MySqlConnection(sconnect);
                mconnect.Open();
                MySqlCommandBuilder cmb = new MySqlCommandBuilder(da);
                da.Update(ds.Tables["MetroList"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }
    }
}
