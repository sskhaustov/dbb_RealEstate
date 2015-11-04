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
    public partial class stc : Form
    {
        MySqlConnection mconnect;
        MySqlCommand cmd;
        MySqlDataReader rdr;
        Graphics g;
        Pen p;
        Bitmap b1 = new Bitmap(387, 292);
        string scon;
        public stc(string sconnect)
        {
            InitializeComponent();
            try
            {
                mconnect = new MySqlConnection(sconnect);
                mconnect.Open();
                scon = sconnect;

                p = new Pen(Color.Black, 1);
                g = Graphics.FromImage(b1);
                draw_axis(g);
                img.Image = b1;

                cmd = new MySqlCommand();
                cmd.Connection = mconnect;
                cmd.Parameters.Add("@typ", MySqlDbType.String);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void draw_axis(Graphics g)
        {
            p.Width = 1;
            p.Color = Color.Black;
            g.Clear(Color.White);
            g.DrawLine(p, 39, 275, 39, 15);
            g.DrawLine(p, 39, 275, 350, 275);
            g.DrawString("Price", new System.Drawing.Font("Arial Black", 8, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.Black), new Point(3, 10));
            g.DrawString("facilities", new System.Drawing.Font("Arial Black", 8, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.Black), new Point(320, 275));
        }

        private void stc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Необходимо указать тип помещения");
            else
            {
                try
                {
                    mconnect.Open();

                    //число помещений в базе
                    cmd.CommandText = "SELECT COUNT(*) FROM facility WHERE facility.usag = @typ";
                    cmd.Parameters["@typ"].Value = textBox1.Text;
                    double c = Convert.ToDouble(cmd.ExecuteScalar());
                    label6.Text = "Всего помещений: " + c.ToString();

                    //средняя температура по больнице
                    cmd.CommandText = "SELECT * FROM facility WHERE facility.usag = @typ";
                    float sumR = 0, sumS = 0;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        sumR += rdr.GetFloat(2);
                        sumS += rdr.GetFloat(3);
                    }
                    rdr.Close();
                    label3.Text = "Средняя цена аренды: " + (sumR / c).ToString();
                    label4.Text = "Средняя цена продажи: " + (sumS / c).ToString();

                    //уровень спроса
                    double sp = 0;
                    cmd.CommandText = "SELECT COUNT(*) FROM RentDocument, facility WHERE RentDocument.Facility = facility.idfacility AND facility.usag = @typ";
                    sp += Convert.ToDouble(cmd.ExecuteScalar());
                    cmd.CommandText = "SELECT COUNT(*) FROM SaleDocument, facility WHERE SaleDocument.Facility = facility.idfacility AND facility.usag = @typ";
                    sp += Convert.ToDouble(cmd.ExecuteScalar());
                    label5.Text = "Уровень спроса: " + (sp / c).ToString() + "%";

                    //изменение цен
                    draw_axis(g);
                    p.Color = Color.Blue;
                    p.Width = 2;
                    Point preA = new Point();
                    Point preB = new Point();
                    Point A = new Point();
                    Point B = new Point();
                    preA.X = 39;
                    preA.Y = 275;
                    preB.X = 39;
                    preB.Y = 275;
                    cmd.CommandText = "SELECT RentPrice, SalePrice FROM facility WHERE facility.usag = @typ";
                    rdr = cmd.ExecuteReader();
                    int ax = Convert.ToInt32(387 / (c * 10));
                    while (rdr.Read())
                    {
                        float pr1 = rdr.GetFloat(0);
                        float pr2 = rdr.GetFloat(1);
                        A.X = ax + 50;
                        B.X = ax + 50;
                        A.Y = 275 - Convert.ToInt32(pr1 / (2920));
                        B.Y = 275 - Convert.ToInt32(pr2 / (2920)) - 10;
                        p.Color = Color.Blue;
                        g.DrawLine(p, preA.X, preA.Y, A.X, A.Y);
                        g.DrawString(pr1.ToString(), new System.Drawing.Font("Arial Black", 8, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.Blue), new Point(A.X, A.Y - 15));
                        preA = A;
                        p.Color = Color.Red;
                        g.DrawLine(p, preB.X, preB.Y, B.X, B.Y);
                        g.DrawString(pr2.ToString(), new System.Drawing.Font("Arial Black", 8, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.Red), new Point(B.X, B.Y - 15));
                        preB = B;
                        ax += 50;
                    }
                    rdr.Close();
                    img.Image = b1;
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
}
