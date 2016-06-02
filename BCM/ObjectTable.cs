using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class ObjectTable : Form
    {
        public ObjectTable()
        {
            InitializeComponent();
        }

        public string TableName
        {
            get { return textBox1.Text.Trim(); }
            set { textBox1.Text = value; }
        }

        public string PrimaryKey
        {
            get { return textBox2.Text.Trim(); }
            set { textBox2.Text = value; }
        }

        public string ShowField
        {
            get { return textBox3.Text.Trim(); }
            set { textBox3.Text = value; }
        }

        public string ParentKey
        {
            get { return textBox4.Text.Trim(); }
            set { textBox4.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
