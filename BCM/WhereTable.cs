using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class WhereTable : Form
    {
        public WhereTable(Dictionary<string, ObjectReference> record, string table, bool isDel)
        {
            InitializeComponent();
            this.record = record;
            this.table = table;
            this.isDel = isDel;
        }

        Dictionary<string, ObjectReference> record;
        string table;
        bool isDel;

        public string Table
        {
            get { return table; }
        }

        public bool IsDel
        {
            get { return isDel; }
        }

        public string Where
        {
            get { return textBox1.Text.Trim(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetWhere();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void GetWhere()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Control c in panel2.Controls)
            {
                Condition con = c as Condition;
                if (con != null)
                {
                    ConditionOperation co = new ConditionOperation(con.OperConcat, con.OperObj, con.Oper, con.OperVal);
                    sb.Append(co.Result);
                }
            }
            textBox1.Text = sb.ToString();
        }

        private void AddCondition(OperConcat oc)
        {
            lock (this)
            {
                int count = panel2.Controls.Count;
                Condition con = new Condition(oc, this);
                con.Name = "condition" + AvailableNumber.ToString();
                con.LoadCondition(record);
                panel2.Controls.Add(con);
                Relayout();
            }
        }

        private void Relayout()
        {
            int y = 0;
            foreach (Control c in panel2.Controls)
            {
                c.Location = new Point(0, y);
                y += 24;
            }
        }

        private int AvailableNumber
        {
            get
            {
                int i = 1;
                while (true)
                {
                    if (panel2.Controls.IndexOfKey("condition" + i) == -1)
                    {
                        break;
                    }
                    i++;
                }
                return i;
            }
        }

        private void RemoveCondition(string key)
        {
            int index = panel2.Controls.IndexOfKey(key);
            panel2.Controls.RemoveAt(index);
        }

        private void WhereTable_Load(object sender, EventArgs e)
        {
            string op = "更新";
            if (this.IsDel)
                op = "删除";
            this.Text = "数据库操作 -- " + op + " 表 " + this.Table + " 的数据";
            LoadOperConcats();
        }

        private void LoadOperConcats()
        {
            string[] vals = Enum.GetNames(typeof(OperConcat));
            operConcat.Items.AddRange(vals);
            if (operConcat.Items.Count > 0)
                operConcat.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCondition((OperConcat)Enum.Parse(typeof(OperConcat), operConcat.Text));
            GetWhere();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Condition> cons = Selecteds();
            if (cons.Count > 0)
            {
                foreach (Condition con in cons)
                {
                    RemoveCondition(con.Name);
                }
                Relayout();
                GetWhere();
            }
            else
            {
                MessageBox.Show("请选定要去除的条件！");
            }
        }

        public List<Condition> Selecteds()
        {
            List<Condition> conditions = new List<Condition>();
            foreach (Control c in panel2.Controls)
            {
                Condition con = c as Condition;
                if (con != null && con.IsSelected)
                {
                    conditions.Add(con);
                }
            }
            return conditions;
        }
    }
}
