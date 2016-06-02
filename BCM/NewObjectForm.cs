using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class NewObjectForm : Form
    {
        public NewObjectForm(TreeNode parentNode, PrimaryKeyShowField currentPrimaryKeyShowField)
        {
            InitializeComponent();
            NewNode(parentNode, currentPrimaryKeyShowField);
        }

        public Dictionary<string, ObjectReference> Record
        {
            get
            {
                return nodeEditor1.Result.Tag as Dictionary<string, ObjectReference>;
            }
        }

        public TreeNode Result
        {
            get
            {
                return nodeEditor1.Result;
            }
        }

        private void NewNode(TreeNode parentNode, PrimaryKeyShowField pksf)
        {
            nodeEditor1.NewNode(parentNode, pksf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
