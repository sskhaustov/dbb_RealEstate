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
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        public void get_data(out string _sname, out string _name, out string _fname, out string _bdate, out string _pseria, out string _pnumber, out string _inn, out string _work, out string _citizen)
        {
            _sname = tb1.Text;
            _name = tb2.Text;
            _fname = tb3.Text;
            _bdate = tb4.Text;
            _pseria = tb5.Text;
            _pnumber = tb6.Text;
            _inn = tb7.Text;
            _work = tb8.Text;
            _citizen = tb9.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
