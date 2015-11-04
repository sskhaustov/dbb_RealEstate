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
    public partial class AddContacts : Form
    {
        public AddContacts()
        {
            InitializeComponent();
        }

        public void get_data(out string number, out string email)
        {
            number = tb1.Text;
            email = tb2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
