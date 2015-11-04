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
    public partial class AddStation : Form
    {
        public AddStation()
        {
            InitializeComponent();
        }

        public void get_data(out string stname, out string dur)
        {
            stname = tb1.Text;
            dur = tb2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
