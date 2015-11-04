using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dbb
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        public void get_data(out string []set, out string req)
        {
            req = "SELECT * FROM facility, Metro, HouseNumber WHERE Metro.facility_m = facility.idfacility AND facility.house = HouseNumber.idHouseNumber";
            set = new string[8];
            set[0] = tb1.Text;
            set[1] = tb2.Text;
            set[2] = tb3.Text;
            set[3] = tb4.Text;
            set[4] = tb5.Text;
            set[5] = tb6.Text;
            set[6] = tb7.Text;
            set[7] = tb8.Text;
            if (set[0] != "")
                req += " AND facility.usag = @sets0";
            if (set[1] != "")
                req += " AND facility.Square = @sets1";
            if (set[2] != "")
                req += " AND facility.NumOfRooms = @sets2";
            if (set[3] != "")
                req += " AND facility.SalePrice >= @sets3";
            if (set[4] != "")
                req += " AND facility.SalePrice <= @sets4";
            if (set[5] != "")
                req += " AND Metro.Station = @sets5";
            if (set[6] != "")
                req += " AND Metro.Duration <= @sets6";
            if (set[7] != "")
                req += " AND HouseNumber.District_h = @sets7";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
