using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class Condition : UserControl
    {
        public Condition(OperConcat oc, WhereTable owner)
        {
            InitializeComponent();
            this.owner = owner;
            checkBox1.Text = oc.ToString().PadRight(3, ' ');
            comboBox2.SelectedIndex = 0;
        }

        WhereTable owner;
        Dictionary<string, ObjectReference> record;

        public OperConcat OperConcat
        {
            get
            {
                return (OperConcat)Enum.Parse(typeof(OperConcat), checkBox1.Text.Trim());
            }
        }

        public string OperObj
        {
            get
            {
                return comboBox1.Text;
            }
        }

        public string Oper
        {
            get
            {
                return comboBox2.Text;
            }
        }

        public string OperVal
        {
            get
            {
                if (panel1.Controls.Count > 0)
                {
                    Control c = panel1.Controls[0];
                    if (c is TextBox)
                    {
                        TextBox t = c as TextBox;
                        return t.Text;
                    }
                    else if (c is ObjectList)
                    {
                        ObjectList o = c as ObjectList;
                        return o.SelectedObject.Value.ToString();
                    }
                }
                return "";
            }
        }

        public void LoadCondition(Dictionary<string, ObjectReference> record)
        {
            this.record = record;
            comboBox1.Items.Clear();
            if (record != null && record.Count > 0)
            {
                foreach (string key in record.Keys)
                {
                    comboBox1.Items.Add(key);
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            if (comboBox1.SelectedIndex > -1)
            {
                string key = comboBox1.SelectedItem.ToString();
                bool isNumeric = record[key].IsNumeric;
                bool nullable = record[key].Nullable;
                string val = "";
                if (record[key].Value != null)
                    val = record[key].Value.ToString();
                string value = isNumeric ? val : (val.Length > 1 && val.StartsWith("'") && val.EndsWith("'") ? val.Substring(1, val.Length - 2) : val);
                if (record[key].IsReference)
                {
                    ObjectList o = new ObjectList();
                    o.Name = key;
                    o.Size = new Size(150, 21);
                    o.Location = new Point(0, 0);
                    o.Dock = DockStyle.Fill;
                    o.Tag = new IsNumericIsNullable(isNumeric, nullable);
                    o.LoadAllObjects(key, record[key].RefObject, value, nullable);
                    o.SelectedIndexChanged += new EventHandler<RefFieldArgs>(o_SelectedIndexChanged);
                    panel1.Controls.Add(o);
                }
                else
                {
                    TextBox t = new TextBox();
                    t.Name = "T_" + key;
                    t.Text = value;
                    if (isNumeric && t.Text.Trim() == "")
                        t.Text = "0";
                    t.Size = new Size(150, 21);
                    t.Location = new Point(0, 0);
                    t.Dock = DockStyle.Fill;
                    t.Tag = new IsNumericIsNullable(isNumeric, nullable);
                    t.TextChanged += new EventHandler(t_TextChanged);
                    panel1.Controls.Add(t);
                }
                owner.GetWhere();
            }
        }

        void o_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectList o = sender as ObjectList;
            if (o.SelectedObject != null)
            {
                owner.GetWhere();
            }
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            IsNumericIsNullable nn = t.Tag as IsNumericIsNullable;
            if (nn != null && nn.IsNumeric)
            {
                if (Common.IsNumber(t.Text))
                {
                    MessageBox.Show("请输入数字！");
                    t.Focus();
                    t.SelectAll();
                    return;
                }
            }
            owner.GetWhere();
        }

        public bool IsSelected
        {
            get
            {
                return checkBox1.Checked;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
            {
                owner.GetWhere();
            }
        }
    }
}
