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
    public partial class Form1 : Form
    {
        int _inn = 0;
        int _id = 0;
        int _idhouse = 0;
        private string sconnect;
        private string stm;
        MySqlConnection mconnect = null;
        MySqlCommand [] cmd;
        MySqlDataAdapter [] da;
        MySqlDataReader rdr = null;
        private DataSet [] ds;
        public Form1()
        {
            InitializeComponent();
            da = new MySqlDataAdapter[11];
            cmd = new MySqlCommand[4];
            ds = new DataSet[11];
            sconnect = @"server=localhost;port=3306;user=root;password=trehsvyat06;database=mydb2;";
            try
            {
                mconnect = new MySqlConnection(sconnect);
                mconnect.Open();

                for (int i = 0; i < 10; i++)
                    ds[i] = new DataSet();

                stm = "SELECT * FROM citizen";
                da[0] = new MySqlDataAdapter(stm, mconnect);
                da[0].Fill(ds[0], "citizen");
                dataGridView1.DataSource = ds[0].Tables[0];

                stm = "SELECT * FROM facility";
                da[1] = new MySqlDataAdapter(stm, mconnect);
                da[1].Fill(ds[1], "facility");
                ds[1].Clear();
                dataGridView2.DataSource = ds[1].Tables[0];

                stm = "SELECT Email FROM Emails";
                da[2] = new MySqlDataAdapter(stm, mconnect);
                da[2].Fill(ds[2], "Emails");
                ds[2].Clear();
                dataGridView3.DataSource = ds[2].Tables[0];

                stm = "SELECT PhoneNumber FROM PhoneNumbers";
                da[3] = new MySqlDataAdapter(stm, mconnect);
                da[3].Fill(ds[3], "PhoneNumbers");
                ds[3].Clear();
                dataGridView4.DataSource = ds[3].Tables[0];

                stm = "SELECT * FROM Districts";
                da[4] = new MySqlDataAdapter(stm, mconnect);
                da[4].Fill(ds[4], "Districts");

                stm = "SELECT * FROM Streets";
                da[5] = new MySqlDataAdapter(stm, mconnect);
                da[5].Fill(ds[5], "Streets");

                stm = "SELECT * FROM HouseNumber";
                da[6] = new MySqlDataAdapter(stm, mconnect);
                da[6].Fill(ds[6], "HouseNumber");
                ds[6].Clear();
                dataGridView7.DataSource = ds[6].Tables[0];

                stm = "SELECT Station,Duration FROM Metro";
                da[7] = new MySqlDataAdapter(stm, mconnect);
                da[7].Fill(ds[7], "Metro");
                ds[7].Clear();
                dataGridView8.DataSource = ds[7].Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private int KEY_GEN(string _cmd)
        {
            cmd[3] = new MySqlCommand();
            cmd[3].Connection = mconnect;
            cmd[3].CommandText = _cmd;
            cmd[3].Prepare();
            rdr = cmd[3].ExecuteReader();
            int max = 0;
            int k;
            while (rdr.Read())
            {
                k = rdr.GetInt32(0);
                if (k > max)
                    max = k;
            }
            rdr.Close();
            max++;
            return max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mconnect.Open();
                MySqlCommandBuilder cmb = new MySqlCommandBuilder(da[0]);
                da[0].Update(ds[0].Tables[0]);
                cmb = new MySqlCommandBuilder(da[1]);
                da[1].Update(ds[1].Tables[0]);
                cmb = new MySqlCommandBuilder(da[2]);
                da[2].Update(ds[2].Tables[0]);
                cmb = new MySqlCommandBuilder(da[3]);
                da[3].Update(ds[3].Tables[0]);
                cmb = new MySqlCommandBuilder(da[4]);
                da[4].Update(ds[4].Tables[0]);
                cmb = new MySqlCommandBuilder(da[5]);
                da[5].Update(ds[5].Tables[0]);
                cmb = new MySqlCommandBuilder(da[6]);
                da[6].Update(ds[6].Tables[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == dataGridView1.Rows.Count - 1)
                    return;
                mconnect.Open();
                if (dataGridView1.Rows[e.RowIndex].Cells[6].Value == DBNull.Value)
                    return;
                _inn = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                for (int i = 2; i < 4; i++)
                    ds[i].Clear();
                stm = "SELECT Email FROM Emails WHERE Emails.citizen_e = @_inn";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].Prepare();
                cmd[0].Parameters.AddWithValue("@_inn", _inn);
                da[2] = new MySqlDataAdapter(cmd[0]);
                da[2].Fill(ds[2], "Emails");
                dataGridView3.DataSource = ds[2].Tables["Emails"];

                stm = "SELECT PhoneNumber FROM PhoneNumbers WHERE PhoneNumbers.citizen_pn = @_inn";
                cmd[1] = new MySqlCommand(stm, mconnect);
                cmd[1].Prepare();
                cmd[1].Parameters.AddWithValue("@_inn", _inn);
                da[3] = new MySqlDataAdapter(cmd[1]);
                da[3].Fill(ds[3], "PhoneNumbers");
                dataGridView4.DataSource = ds[3].Tables["PhoneNumbers"];

                stm = "SELECT * FROM facility WHERE facility.household = @_inn";
                cmd[1] = new MySqlCommand(stm, mconnect);
                cmd[1].Prepare();
                cmd[1].Parameters.AddWithValue("@_inn", _inn);
                da[1] = new MySqlDataAdapter(cmd[1]);
                ds[1].Clear();
                da[1].Fill(ds[1], "facility");
                dataGridView2.DataSource = ds[1].Tables["facility"];

                da[6].Fill(ds[6], "HouseNumber");
                ds[6].Clear();
                dataGridView7.DataSource = ds[6].Tables["HouseNumber"];
                da[7].Fill(ds[7], "Metro");
                ds[7].Clear();
                dataGridView8.DataSource = ds[7].Tables["Metro"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int _idfac = 0;
            try
            {
                if (e.RowIndex == dataGridView2.Rows.Count - 1)
                    return;
                mconnect.Open();
                if (dataGridView2.Rows[e.RowIndex].Cells[9].Value == DBNull.Value)
                    return;
                _id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[9].Value);
                _idfac = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[8].Value);
                if (_idfac == 0)
                    return;
                for (int i = 4; i < 8; i++)
                    ds[i].Clear();
                stm = "SELECT * FROM HouseNumber WHERE HouseNumber.idHouseNumber = @_idfac";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].Prepare();
                cmd[0].Parameters.AddWithValue("@_idfac", _idfac);
                da[6] = new MySqlDataAdapter(cmd[0]);
                da[6].Fill(ds[6], "HouseNumber");
                dataGridView7.DataSource = ds[6].Tables["HouseNumber"];

                stm = "SELECT Station, Duration FROM Metro WHERE Metro.facility_m = @id";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].Prepare();
                cmd[0].Parameters.AddWithValue("@id", _id);
                da[7] = new MySqlDataAdapter(cmd[0]);
                da[7].Fill(ds[7], "Metro");
                dataGridView8.DataSource = ds[7].Tables["Metro"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sname = "", name = "", fname = "",
                   bdate = "", pseria = "", pnumber = "",
                   inn = "", work = "", citizen = "";
            try
            {
                Add w = new Add();
                w.ShowDialog();
                if (w.DialogResult == DialogResult.OK)
                {
                    w.get_data(out sname, out name, out fname, out bdate, out pseria, out pnumber, out inn, out work, out citizen);
                    mconnect.Open();
                    cmd[1] = new MySqlCommand();
                    cmd[1].Connection = mconnect;
                    cmd[1].CommandText = @"INSERT INTO citizen(Surname, Name, FatherName, DateBirthday, PassportSeria, PassportNumber, INN, Workplace, Citizenship)
                                    VALUES(
                                            @Sname,
                                            @name,
                                            @Fname,
                                            @DateB,
                                            @PSeria,
                                            @PNumber,
                                            @INN,
                                            @Work,
                                            @Citizen)";
                    cmd[1].Prepare();
                    cmd[1].Parameters.AddWithValue("@Sname", sname);
                    cmd[1].Parameters.AddWithValue("@name", name);
                    cmd[1].Parameters.AddWithValue("@Fname", fname);
                    cmd[1].Parameters.AddWithValue("@DateB", bdate);
                    cmd[1].Parameters.AddWithValue("@PSeria", pseria);
                    cmd[1].Parameters.AddWithValue("@PNumber", pnumber);
                    cmd[1].Parameters.AddWithValue("@INN", inn);
                    cmd[1].Parameters.AddWithValue("@Work", work);
                    cmd[1].Parameters.AddWithValue("@Citizen", citizen);
                    cmd[1].ExecuteNonQuery();
                    ds[0].Clear();
                    da[0].Fill(ds[0], "citizen");
                    dataGridView1.DataSource = ds[0].Tables["citizen"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string number = "", email = "";
            try
            {
                mconnect.Open();
                AddContacts v = new AddContacts();
                v.ShowDialog();
                if (v.DialogResult == DialogResult.OK)
                {
                    v.get_data(out number, out email);
                    cmd[0] = new MySqlCommand();
                    cmd[1] = new MySqlCommand();
                    cmd[0].Connection = mconnect;
                    cmd[1].Connection = mconnect;
                    if (number != "")
                    {
                        cmd[0].CommandText = @"INSERT INTO PhoneNumbers(citizen_pn, PhoneNumber)
                                    VALUES(
                                            @INN,
                                            @number)";
                        cmd[0].Prepare();
                        cmd[0].Parameters.AddWithValue("@INN", _inn);
                        cmd[0].Parameters.AddWithValue("@number", number);
                        cmd[0].ExecuteNonQuery();
                    }
                    if (email != "")
                    {
                        cmd[1].CommandText = @"INSERT INTO Emails(citizen_e, Email)
                                    VALUES(
                                            @INN,
                                            @email)";
                        cmd[1].Prepare();
                        cmd[1].Parameters.AddWithValue("@INN", _inn);
                        cmd[1].Parameters.AddWithValue("@email", email);
                        cmd[1].ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                mconnect.Open();
                string stm = "DELETE FROM citizen WHERE citizen.INN = @_inn";
                string stm2 = "DELETE FROM PhoneNumbers WHERE PhoneNumbers.citizen_pn = @_inn";
                string stm3 = "DELETE FROM Emails WHERE Emails.citizen_e = @_inn";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[1] = new MySqlCommand(stm2, mconnect);
                cmd[2] = new MySqlCommand(stm3, mconnect);
                cmd[0].Parameters.AddWithValue("@_inn", _inn);
                cmd[1].Parameters.AddWithValue("@_inn", _inn);
                cmd[2].Parameters.AddWithValue("@_inn", _inn);
                cmd[1].ExecuteNonQuery();
                cmd[2].ExecuteNonQuery();
                cmd[0].ExecuteNonQuery();
                ds[0].Clear();
                da[0].Fill(ds[0], "citizen");
                dataGridView1.DataSource = ds[0].Tables["citizen"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string uno = "", use = "", rprice = "",
                   sprice = "", sq = "", usq = "",
                   rooms = "", housenum = "", dis = "", str = "";
            if (_inn == 0)
                MessageBox.Show("Сначала выберите владельца!");
            else
            {
                try
                {
                    AddFacility w = new AddFacility();
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.OK)
                    {
                        string s = "";
                        bool samestr = false,
                             samedis = false,
                             samenum = false;
                        w.get_data(out uno, out use, out rprice, out sprice, out sq, out usq, out rooms, out housenum, out dis, out str);
                        mconnect.Open();
                        cmd[1] = new MySqlCommand();
                        cmd[1].Connection = mconnect;
                        cmd[0] = new MySqlCommand();
                        cmd[0].Connection = mconnect;
                        cmd[0].Prepare();
                        cmd[0].CommandText = @"SELECT Street FROM Streets";
                        rdr = cmd[0].ExecuteReader();
                        while (rdr.Read() && !samestr)
                        {
                            s = rdr.GetString(0);
                            if (s == str)
                                samestr = true;
                        }
                        rdr.Close();
                        if (samestr == false)
                        {
                            cmd[0].CommandText = @"INSERT INTO Streets(Street)
                                    VALUES(
                                            @str)";
                            cmd[0].Parameters.AddWithValue("@str", str);
                            cmd[0].Prepare();
                            cmd[0].ExecuteNonQuery();
                        }

                        cmd[2] = new MySqlCommand();
                        cmd[2].Connection = mconnect;
                        cmd[2].Prepare();
                        cmd[2].CommandText = @"SELECT District FROM Districts";
                        rdr = cmd[2].ExecuteReader();
                        while (rdr.Read() && !samedis)
                        {
                            s = rdr.GetString(0);
                            if (s == dis)
                                samedis = true;
                        }
                        rdr.Close();
                        if (samedis == false)
                        {
                            cmd[2].CommandText = @"INSERT INTO Districts(District)
                                    VALUES(
                                            @dis)";
                            cmd[2].Parameters.AddWithValue("@dis", dis);
                            cmd[2].Prepare();
                            cmd[2].ExecuteNonQuery();
                        }
                        if (samestr && samedis)
                        {
                            cmd[2].Prepare();
                            cmd[2].CommandText = @"SELECT * FROM HouseNumber";
                            rdr = cmd[2].ExecuteReader();
                            while (rdr.Read() && !samenum)
                            {
                                s = rdr.GetString(3);
                                if (s == housenum)
                                    samenum = true;
                            }
                            rdr.Close();
                        }
                        if (!samenum)
                        {
                            _idhouse = KEY_GEN(@"SELECT idHouseNumber FROM HouseNumber");
                            cmd[3] = new MySqlCommand();
                            cmd[3].Connection = mconnect;
                            cmd[3].CommandText = @"INSERT INTO HouseNumber(idHouseNumber, District_h, Street_h, Number)
                                    VALUES(
                                            @idHN,
                                            @dis,
                                            @str,
                                            @housenum)";
                            cmd[3].Prepare();
                            cmd[3].Parameters.AddWithValue("@idHN", _idhouse);
                            cmd[3].Parameters.AddWithValue("@dis", dis);
                            cmd[3].Parameters.AddWithValue("@str", str);
                            cmd[3].Parameters.AddWithValue("@housenum", housenum);
                            cmd[3].ExecuteNonQuery();
                        }
                        if (samenum)
                        {
                            cmd[3] = new MySqlCommand();
                            cmd[3].Connection = mconnect;
                            cmd[3].Prepare();
                            cmd[3].CommandText = @"SELECT idHouseNumber FROM HouseNumber WHERE HouseNumber.District_h = @dis AND HouseNumber.Street_h = @str AND HouseNumber.Number = @housenum";
                            cmd[3].Parameters.AddWithValue("@dis", dis);
                            cmd[3].Parameters.AddWithValue("@str", str);
                            cmd[3].Parameters.AddWithValue("@housenum", housenum);
                            rdr = cmd[3].ExecuteReader();
                            while (rdr.Read())
                                _idhouse = rdr.GetInt32(0);
                            rdr.Close();
                        }
                        cmd[1].CommandText = @"INSERT INTO facility(numb, usag, RentPrice, SalePrice, Square, UsefulSquare, NumOfRooms, household, house, idfacility)
                                    VALUES(
                                            @uno,
                                            @use,
                                            @rprice,
                                            @sprice,
                                            @sq,
                                            @usq,
                                            @rooms,
                                            @inn,
                                            @idHN,
                                            @k)";
                        cmd[1].Prepare();
                        cmd[1].Parameters.AddWithValue("@uno", uno);
                        cmd[1].Parameters.AddWithValue("@use", use);
                        cmd[1].Parameters.AddWithValue("@rprice", rprice);
                        cmd[1].Parameters.AddWithValue("@sprice", sprice);
                        cmd[1].Parameters.AddWithValue("@sq", sq);
                        cmd[1].Parameters.AddWithValue("@usq", usq);
                        cmd[1].Parameters.AddWithValue("@rooms", rooms);
                        cmd[1].Parameters.AddWithValue("@inn", _inn);
                        cmd[1].Parameters.AddWithValue("@idHN", _idhouse);
                        cmd[1].Parameters.AddWithValue("@k", KEY_GEN(@"SELECT idfacility FROM facility"));
                        cmd[1].ExecuteNonQuery();

                        stm = "SELECT * FROM facility WHERE facility.household = @inn";
                        cmd[0] = new MySqlCommand(stm);
                        cmd[0].Connection = mconnect;
                        cmd[0].Prepare();
                        cmd[0].Parameters.AddWithValue("@inn", _inn);
                        da[1] = new MySqlDataAdapter(cmd[0]);
                        ds[1].Clear();
                        da[1].Fill(ds[1], "facility");
                        dataGridView2.DataSource = ds[1].Tables["facility"];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (mconnect != null)
                    mconnect.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                mconnect.Open();

                stm = "DELETE FROM Emails";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[2].Clear();
                da[2].Fill(ds[2], "Emails");
                dataGridView3.DataSource = ds[2].Tables["Emails"];

                stm = "DELETE FROM PhoneNumbers";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[3].Clear();
                da[3].Fill(ds[3], "PhoneNumbers");
                dataGridView4.DataSource = ds[3].Tables["PhoneNumbers"];

                stm = "DELETE FROM Metro";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[7].Clear();
                da[7].Fill(ds[7], "Metro");
                dataGridView8.DataSource = ds[7].Tables["Metro"];

                stm = "DELETE FROM facility";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[1].Clear();
                da[1].Fill(ds[1], "facility");
                dataGridView2.DataSource = ds[1].Tables["facility"];

                stm = "DELETE FROM citizen";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[0].Clear();
                da[0].Fill(ds[0], "citizen");
                dataGridView1.DataSource = ds[0].Tables["citizen"];

                stm = "DELETE FROM HouseNumber";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
                ds[6].Clear();
                da[6].Fill(ds[6], "HouseNumber");
                dataGridView7.DataSource = ds[6].Tables["HouseNumber"];

                stm = "DELETE FROM Districts";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();

                stm = "DELETE FROM Streets";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ListStations w = new ListStations();
            w.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string stname = "", dur = "";
            AddStation w = new AddStation();
            w.ShowDialog();
            if (w.DialogResult == DialogResult.OK)
            {
                w.get_data(out stname, out dur);
                try
                {
                    mconnect.Open();

                    //генерация ключа
                    int k = KEY_GEN(@"SELECT skey FROM Metro");

                    cmd[0] = new MySqlCommand();
                    cmd[0].Connection = mconnect;
                    cmd[0].CommandText = @"INSERT INTO Metro(facility_m, Station, Duration, skey)
                                      VALUES(
                                            @id,
                                            @stn,
                                            @d,
                                            @ke)";
                    cmd[0].Prepare();
                    cmd[0].Parameters.AddWithValue("@id", _id);
                    cmd[0].Parameters.AddWithValue("@stn", stname);
                    cmd[0].Parameters.AddWithValue("@d", dur);
                    cmd[0].Parameters.AddWithValue("@ke", k);
                    cmd[0].ExecuteNonQuery();

                    stm = "SELECT Station, Duration FROM Metro WHERE Metro.facility_m = @id";
                    cmd[0] = new MySqlCommand(stm);
                    cmd[0].Connection = mconnect;
                    cmd[0].Prepare();
                    cmd[0].Parameters.AddWithValue("@id", _id);
                    da[7] = new MySqlDataAdapter(cmd[0]);
                    ds[7].Clear();
                    da[7].Fill(ds[7], "Metro");
                    dataGridView8.DataSource = ds[7].Tables["Metro"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (mconnect != null)
                    mconnect.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s="", d="";
            try
            {
                mconnect.Open();
                string stm = "SELECT house FROM facility WHERE facility.idfacility = @_id";
                cmd[0] = new MySqlCommand(stm, mconnect);
                cmd[0].Parameters.AddWithValue("@_id", _id);
                cmd[0].Parameters.AddWithValue("@_idhouse", _idhouse);
                rdr = cmd[0].ExecuteReader();
                while (rdr.Read())
                    _idhouse = rdr.GetInt32(0);
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                cmd[0].CommandText = "DELETE FROM Metro WHERE Metro.facility_m = @_id";
                cmd[0].ExecuteNonQuery();

                cmd[0].CommandText = "DELETE FROM facility WHERE facility.idfacility = @_id";
                cmd[0].ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                cmd[0].CommandText = "SELECT Street_h FROM HouseNumber WHERE HouseNumber.idHouseNumber = @_idhouse";
                rdr = cmd[0].ExecuteReader();
                while (rdr.Read())
                    s = rdr.GetString(0);
                rdr.Close();

                cmd[0].CommandText = "SELECT District_h FROM HouseNumber WHERE HouseNumber.idHouseNumber = @_idhouse";
                rdr = cmd[0].ExecuteReader();
                while (rdr.Read())
                    d = rdr.GetString(0);
                rdr.Close();

                string stm2 = "DELETE FROM HouseNumber WHERE HouseNumber.idHouseNumber = @_idhouse";
                cmd[1] = new MySqlCommand(stm2, mconnect);
                cmd[1].Parameters.AddWithValue("@_idhouse", _idhouse);
                cmd[1].ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                cmd[0].CommandText = "DELETE FROM Districts WHERE Districts.District = @_d";
                cmd[0].Parameters.AddWithValue("@_d", d);
                cmd[0].ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                cmd[0].CommandText = "DELETE FROM Streets WHERE Streets.Street = @_s";
                cmd[0].Parameters.AddWithValue("@_s", s);
                cmd[0].ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                ds[1].Clear();
                da[1].Fill(ds[1], "facility");
                dataGridView2.DataSource = ds[1].Tables[0];
                ds[6].Clear();
                da[6].Fill(ds[6], "HouseNumber");
                dataGridView7.DataSource = ds[6].Tables[0];
                ds[7].Clear();
                da[7].Fill(ds[7], "Metro");
                dataGridView8.DataSource = ds[7].Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mconnect != null)
                mconnect.Close();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string[] sets;
            string comm;
            Search w = new Search();
            w.ShowDialog();
            if (w.DialogResult == DialogResult.OK)
            {
                w.get_data(out sets, out comm);
                try
                {
                    mconnect.Open();
                    cmd[0] = new MySqlCommand();
                    cmd[0].Connection = mconnect;
                    cmd[0].Prepare();
                    cmd[0].Parameters.AddWithValue("@sets0", sets[0]);
                    cmd[0].Parameters.AddWithValue("@sets1", sets[1]);
                    cmd[0].Parameters.AddWithValue("@sets2", sets[2]);
                    cmd[0].Parameters.AddWithValue("@sets3", sets[3]);
                    cmd[0].Parameters.AddWithValue("@sets4", sets[4]);
                    cmd[0].Parameters.AddWithValue("@sets5", sets[5]);
                    cmd[0].Parameters.AddWithValue("@sets6", sets[6]);
                    cmd[0].Parameters.AddWithValue("@sets7", sets[7]);
                    cmd[0].CommandText = comm;
                    da[1] = new MySqlDataAdapter(cmd[0]);
                    ds[1].Clear();
                    da[1].Fill(ds[1], "facility");
                    dataGridView2.DataSource = ds[1].Tables["facility"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (mconnect != null)
                    mconnect.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string _in = "", _srok = "";
            if (_id == 0)
            {
                MessageBox.Show("Сначала выберите помещение");
                return;
            }
            Client w = new Client(sconnect);
                try
                {
                    mconnect.Open();
                    cmd[0] = new MySqlCommand();
                    cmd[0].Connection = mconnect;
                    cmd[0].CommandText = "SELECT Facility FROM RentDocument WHERE RentDocument.Facility = @id";
                    cmd[0].Parameters.AddWithValue("@id", _id);
                    rdr = cmd[0].ExecuteReader();
                    int _rent = 0;
                    while (rdr.Read())
                        _rent = rdr.GetInt32(0);
                    rdr.Close();
                    cmd[0].CommandText = "SELECT household FROM facility WHERE facility.idfacility = @id";
                    rdr = cmd[0].ExecuteReader();
                    int _hos = 0;
                    while (rdr.Read())
                        _hos = rdr.GetInt32(0);
                    rdr.Close();
                    if (_rent == _id)
                    {
                        MessageBox.Show("Это помещение уже арендовано");
                    }
                    else
                    {
                        w.ShowDialog();
                        if (w.DialogResult == DialogResult.OK)
                        {
                            w.get_data(out _in, out _srok);
                            if (Convert.ToInt32(_in) == _hos)
                                MessageBox.Show("Ошибка, выбранный клиент и владелец - один и тот же человек");
                            else
                            {
                                cmd[1] = new MySqlCommand();
                                cmd[1].Connection = mconnect;
                                cmd[1].CommandText = @"REPLACE INTO RentDocument(idRentDocument, Owner, Client, Facility, Date, Duration)
                                      VALUES(
                                            @idDoc,
                                            @Ow,
                                            @Cl,
                                            @Fac,
                                            @Dat,
                                            @Dur)";
                                cmd[1].Parameters.AddWithValue("@idDoc", KEY_GEN("SELECT idRentDocument FROM RentDocument"));
                                cmd[1].Parameters.AddWithValue("@Ow", _hos);
                                cmd[1].Parameters.AddWithValue("@Cl", Convert.ToInt32(_in));
                                cmd[1].Parameters.AddWithValue("@Fac", _id);
                                cmd[1].Parameters.AddWithValue("@Dat", DateTime.Today);
                                cmd[1].Parameters.AddWithValue("@Dur", _srok);
                                cmd[1].ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (mconnect != null)
                    mconnect.Close();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            mconnect = new MySqlConnection(sconnect);
            mconnect.Open();
            Form2 w = new Form2(mconnect, ds[8]);
            w.ShowDialog();
            if (mconnect != null)
                mconnect.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string _in = "", _srok = "";
            if (_id == 0)
            {
                MessageBox.Show("Сначала выберите помещение");
                return;
            }
            Client w = new Client(sconnect);
                try
                {
                    mconnect.Open();
                    cmd[0] = new MySqlCommand();
                    cmd[0].Connection = mconnect;
                    cmd[0].CommandText = "SELECT Facility FROM SaleDocument WHERE SaleDocument.Facility = @id";
                    cmd[0].Parameters.AddWithValue("@id", _id);
                    rdr = cmd[0].ExecuteReader();
                    int _sale = 0;
                    while (rdr.Read())
                        _sale = rdr.GetInt32(0);
                    rdr.Close();
                    cmd[0].CommandText = "SELECT household FROM facility WHERE facility.idfacility = @id";
                    rdr = cmd[0].ExecuteReader();
                    int _hos = 0;
                    while (rdr.Read())
                        _hos = rdr.GetInt32(0);
                    rdr.Close();
                    if (_sale == _id)
                    {
                        MessageBox.Show("Это помещение уже продано!");
                    }
                    else
                    {
                        w.ShowDialog();
                        if (w.DialogResult == DialogResult.OK)
                        {
                            w.get_data(out _in, out _srok);
                            if (Convert.ToInt32(_in) == _hos)
                                MessageBox.Show("Ошибка, выбранный клиент и владелец - один и тот же человек");
                            else
                            {
                                cmd[1] = new MySqlCommand();
                                cmd[1].Connection = mconnect;
                                cmd[1].CommandText = @"REPLACE INTO SaleDocument(idSaleDocument, Saler, Client, Facility, Date)
                                      VALUES(
                                            @idDoc,
                                            @Sl,
                                            @Cl,
                                            @Fac,
                                            @Dat)";
                                cmd[1].Parameters.AddWithValue("@idDoc", KEY_GEN("SELECT idSaleDocument FROM SaleDocument"));
                                cmd[1].Parameters.AddWithValue("@Sl", _hos);
                                cmd[1].Parameters.AddWithValue("@Cl", Convert.ToInt32(_in));
                                cmd[1].Parameters.AddWithValue("@Fac", _id);
                                cmd[1].Parameters.AddWithValue("@Dat", DateTime.Today);
                                cmd[1].ExecuteNonQuery();
                                cmd[1].CommandText = @"UPDATE facility SET household=@Cl WHERE facility.idfacility=@Fac";
                                cmd[1].ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (mconnect != null)
                    mconnect.Close();
        }

        private void показатьАрхивАрендованныхпроданныхПомещенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mconnect.Open();
            Form2 w = new Form2(mconnect, ds[8]);
            w.ShowDialog();
            if (mconnect != null)
                mconnect.Close();
        }

        private void показатьСтатистикуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stc w = new stc(sconnect);
            w.ShowDialog();
        }
    }
}
