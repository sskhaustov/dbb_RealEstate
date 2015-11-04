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
    public partial class Client : Form
    {
        MySqlCommand cmd;
        MySqlConnection mconnect;
        MySqlDataReader rdr;
        public Client(string sconnect)
        {
            InitializeComponent();
            mconnect = new MySqlConnection(sconnect);
            mconnect.Open();
            cmd = new MySqlCommand();
            cmd.Connection = mconnect;
            cmd.CommandText = "SELECT INN FROM citizen";
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
                comboBox1.Items.Add(rdr.GetInt32(0));
            rdr.Close();
            if (mconnect != null)
                mconnect.Close();
        }

        public void get_data(out string _inn, out string _srok)
        {
            _inn = comboBox1.Text;
            _srok = tb2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
