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
    public partial class Form2 : Form
    {
        MySqlDataAdapter da, da2;
        DataSet dat, dat2;
        MySqlCommand cmd;
        MySqlConnection conn;
        int docR = 0;
        int docS = 0;
        public Form2(MySqlConnection mcon, DataSet ds)
        {
            InitializeComponent();
            dat = new DataSet();
            dat2 = new DataSet();
            conn = mcon;
            cmd = new MySqlCommand();
            cmd.Connection = mcon;
            cmd.CommandText = "SELECT * FROM RentDocument";
            da = new MySqlDataAdapter(cmd);
            da.Fill(dat, "RentDocument");
            dataGridView1.DataSource = dat.Tables["RentDocument"];
            cmd.CommandText = "SELECT * FROM SaleDocument";
            da2 = new MySqlDataAdapter(cmd);
            da2.Fill(dat2, "SaleDocument");
            dataGridView2.DataSource = dat2.Tables["SaleDocument"];
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM RentDocument";
            cmd.ExecuteNonQuery();
            da = new MySqlDataAdapter(cmd);
            dat.Clear();
            da.Fill(dat, "RentDocument");
            dataGridView1.DataSource = dat.Tables["RentDocument"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM SaleDocument";
            cmd.ExecuteNonQuery();
            da2 = new MySqlDataAdapter(cmd);
            dat2.Clear();
            da2.Fill(dat2, "SaleDocument");
            dataGridView2.DataSource = dat2.Tables["SaleDocument"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (docR == 0)
                MessageBox.Show("Сначала выберите договор");
            else
            {
                try
                {
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM RentDocument WHERE RentDocument.idRentDocument = @docR";
                    cmd.Parameters.AddWithValue("@docR", docR);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT * FROM RentDocument";
                    da = new MySqlDataAdapter(cmd);
                    dat.Clear();
                    da.Fill(dat, "RentDocument");
                    dataGridView1.DataSource = dat.Tables["RentDocument"];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (conn != null)
                    conn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == DBNull.Value)
                return;
            docR = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[0].Value == DBNull.Value)
                return;
            docS = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (docS == 0)
                MessageBox.Show("Сначала выберите договор");
            else
            {
                try
                {
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM SaleDocument WHERE SaleDocument.idSaleDocument = @docS";
                    cmd.Parameters.AddWithValue("@docS", docS);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT * FROM SaleDocument";
                    da2 = new MySqlDataAdapter(cmd);
                    dat2.Clear();
                    da2.Fill(dat2, "SaleDocument");
                    dataGridView2.DataSource = dat2.Tables["SaleDocument"];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (conn != null)
                    conn.Close();
            }
        }
    }
}
