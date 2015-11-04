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
    public partial class AddFacility : Form
    {
        public AddFacility()
        {
            InitializeComponent();
        }

        public void get_data(out string _uno, out string _usage, out string _rprice, out string _sprice, out string _sq, out string _usq, out string _rooms, out string _housenum, out string _dis, out string _str)
        {
            _uno = tb1.Text;
            _usage = tb2.Text;
            _rprice = tb3.Text;
            _sprice = tb4.Text;
            _sq = tb5.Text;
            _usq = tb6.Text;
            _rooms = tb7.Text;
            _dis = tb8.Text;
            _str = tb9.Text;
            _housenum = tb10.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
