using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace BCM
{
    public partial class AdminAuth : Form
    {
        public AdminAuth()
        {
            InitializeComponent();
        }

        public string Admin
        {
            get { return textBox1.Text.Trim(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
