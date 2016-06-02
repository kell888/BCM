using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class SettingTable : Form
    {
        public SettingTable()
        {
            InitializeComponent();
        }

        public string TableName
        {
            get { return textBox1.Text.Trim(); }
            set { textBox1.Text = value; }
        }

        public bool HasPrimaryKey
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public string PrimaryKey
        {
            get { return textBox2.Text.Trim(); }
            set { textBox2.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
