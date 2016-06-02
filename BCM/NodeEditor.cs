using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace BCM
{
    public partial class NodeEditor : UserControl
    {
        public NodeEditor()
        {
            InitializeComponent();
            refEventField = Common.GetRefEventField();
        }

        PrimaryKeyShowField primaryKeyShowField;
        Dictionary<string, string> refEventField;

        public void ClearNodes()
        {
            this.Controls.Clear();
        }

        public void LoadNode(TreeNode node, PrimaryKeyShowField primaryKeyShowField)
        {
            this.primaryKeyShowField = primaryKeyShowField;
            if (node != null)
            {
                int x = 4;
                int x2 = 110;
                int x3 = 262;
                int y = 4;
                this.Controls.Clear();
                Dictionary<string, ObjectReference> record = node.Tag as Dictionary<string, ObjectReference>;
                if (record != null)
                {
                    List<string> disabledItems = new List<string>();
                    string editDisabledItems = ConfigurationManager.AppSettings["editDisabledItems"];
                    if (!string.IsNullOrEmpty(editDisabledItems))
                    {
                        string[] edis = editDisabledItems.Split('|');
                        foreach (string edi in edis)
                        {
                            disabledItems.Add(edi.ToLower());
                        }
                    }
                    foreach (string key in record.Keys)
                    {
                        bool isNumeric = record[key].IsNumeric;
                        bool nullable = record[key].Nullable;
                        string val = "";
                        if (record[key].Value != null)
                            val = record[key].Value.ToString();
                        string value = isNumeric ? val : (val.Length > 1 && val.StartsWith("'") && val.EndsWith("'") ? val.Substring(1, val.Length - 2) : val);
                        Label l = new Label();
                        l.Name = "L_" + key;
                        if (key == primaryKeyShowField.PrimaryKey)
                            l.Text = key + "[主键]";
                        else
                            l.Text = key;
                        l.AutoSize = false;
                        l.AutoEllipsis = true;
                        l.Size = new Size(100, 21);
                        l.TextAlign = ContentAlignment.MiddleRight;
                        l.Location = new Point(x, y);
                        this.Controls.Add(l);
                        if (record[key].IsReference)
                        {
                            ObjectList o = new ObjectList();
                            o.Name = key;
                            o.Size = new Size(150, 21);
                            o.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                            o.Location = new Point(x2, y);
                            o.Tag = new IsNumericIsNullable(isNumeric, nullable);
                            o.LoadAllObjects(key, record[key].RefObject, value, nullable);
                            if (key == primaryKeyShowField.PrimaryKey || disabledItems.Contains(key.ToLower()))
                                o.Enabled = false;
                            if (refEventField.Count > 0 && refEventField.ContainsKey(key))
                            {
                                o.SelectedIndexChanged += new EventHandler<RefFieldArgs>(o_SelectedIndexChanged);
                            }
                            this.Controls.Add(o);
                        }
                        else
                        {
                            TextBox t = new TextBox();
                            t.Name = key;
                            t.Text = value;
                            if (isNumeric && t.Text.Trim() == "")
                                t.Text = "0";
                            t.Size = new Size(150, 21);
                            t.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                            t.Location = new Point(x2, y);
                            t.Tag = new IsNumericIsNullable(isNumeric, nullable);
                            if (key == primaryKeyShowField.PrimaryKey || disabledItems.Contains(key.ToLower()))
                                t.Enabled = false;
                            if (refEventField.Count > 0 && refEventField.ContainsKey(key))
                            {
                                t.TextChanged += new EventHandler(t_TextChanged);
                            }
                            this.Controls.Add(t);
                        }
                        CheckBox c = new CheckBox();
                        c.Name = "C_" + key;
                        c.Text = "数字";
                        c.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        c.Location = new Point(x3, y);
                        c.Checked = isNumeric;
                        c.Enabled = false;
                        this.Controls.Add(c);
                        y += 26;
                    }
                }
            }
        }

        public TreeNode Result
        {
            get
            {
                string keyValue = "";
                string showValue = "";
                TreeNode node = new TreeNode();
                Dictionary<string, ObjectReference> current = new Dictionary<string, ObjectReference>();
                foreach (Control c in this.Controls)
                {
                    ObjectReference or = null;
                    if (c is TextBox)
                    {
                        TextBox t = c as TextBox;
                        IsNumericIsNullable nn = t.Tag as IsNumericIsNullable;
                        or = new ObjectReference(t.Text.Trim(), nn.IsNumeric, nn.Nullable);
                        current.Add(t.Name, or);
                        if (t.Name == primaryKeyShowField.PrimaryKey)
                            keyValue = t.Text.Trim();
                        if (t.Name == primaryKeyShowField.ShowField)
                            showValue = t.Text.Trim();
                    }
                    else if (c is ObjectList)
                    {
                        ObjectList o = c as ObjectList;
                        IsNumericIsNullable nn = o.Tag as IsNumericIsNullable;
                        or = new ObjectReference(o.SelectedObject.Value, nn.IsNumeric, nn.Nullable, o.ObjectClass);
                        current.Add(o.Name, or);
                        if (o.Name == primaryKeyShowField.PrimaryKey)
                            keyValue = o.SelectedObject.Value.ToString();
                        if (o.Name == primaryKeyShowField.ShowField)
                            showValue = o.SelectedObject.Text;
                    }
                }
                node.Name = keyValue;
                node.Text = showValue;
                node.Tag = current;
                return node;
            }
        }

        public void NewNode(TreeNode parentNode, PrimaryKeyShowField primaryKeyShowField)
        {
            this.primaryKeyShowField = primaryKeyShowField;
            if (parentNode != null)
            {
                int x = 4;
                int x2 = 110;
                int x3 = 262;
                int y = 4;
                this.Controls.Clear();
                Dictionary<string, ObjectReference> record = parentNode.Tag as Dictionary<string, ObjectReference>;
                if (record != null)
                {
                    foreach (string key in record.Keys)
                    {
                        bool isNumeric = record[key].IsNumeric;
                        bool nullable = record[key].Nullable;
                        string val = "";
                        if (record[key].Value != null)
                            val = record[key].Value.ToString();
                        string value = isNumeric ? val : (val.Length > 1 && val.StartsWith("'") && val.EndsWith("'") ? val.Substring(1, val.Length - 2) : val);
                        Label l = new Label();
                        l.Name = "L_" + key;
                        if (key == primaryKeyShowField.PrimaryKey)
                            l.Text = key + "[主键]";
                        else
                            l.Text = key;
                        l.AutoSize = false;
                        l.AutoEllipsis = true;
                        l.Size = new Size(100, 21);
                        l.TextAlign = ContentAlignment.MiddleRight;
                        l.Location = new Point(x, y);
                        this.Controls.Add(l);
                        if (record[key].IsReference)
                        {
                            ObjectList o = new ObjectList();
                            o.Name = key;
                            o.Size = new Size(150, 21);
                            o.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                            o.Location = new Point(x2, y);
                            o.Tag = new IsNumericIsNullable(isNumeric, nullable);
                            object selected = value;
                            if (key == "parent_id")
                                selected = record["object_id"].Value;
                            else if (key == "object_type_id")
                                selected = Common.GetFirstSubObjectType(Common.GetObjectLevelByObjectTypeId(record["object_type_id"].Value));
                            o.LoadAllObjects(key, record[key].RefObject, selected, nullable);
                            if (key == primaryKeyShowField.PrimaryKey)
                                o.Enabled = false;
                            if (refEventField.Count > 0 && refEventField.ContainsKey(key))
                            {
                                o.SelectedIndexChanged += new EventHandler<RefFieldArgs>(o_SelectedIndexChanged);
                            }
                            this.Controls.Add(o);
                        }
                        else
                        {
                            TextBox t = new TextBox();
                            t.Name = key;
                            t.Text = "";
                            DateTime dt;
                            if (isNumeric)
                            {
                                if (key == "order_id")
                                    t.Text = Common.GetObjectCount(Convert.ToInt32(record["object_type_id"].Value)).ToString();
                                else
                                    t.Text = "0";
                            }
                            else if (!isNumeric && DateTime.TryParse(record[key].Value.ToString(), out dt))
                            {
                                t.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            t.Size = new Size(150, 21);
                            t.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                            t.Location = new Point(x2, y);
                            t.Tag = new IsNumericIsNullable(isNumeric, nullable);
                            if (key == primaryKeyShowField.PrimaryKey)
                                t.Enabled = false;
                            if (refEventField.Count > 0 && refEventField.ContainsKey(key))
                            {
                                t.TextChanged += new EventHandler(t_TextChanged);
                            }
                            this.Controls.Add(t);
                        }
                        CheckBox c = new CheckBox();
                        c.Name = "C_" + key;
                        c.Text = "数字";
                        c.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        c.Location = new Point(x3, y);
                        c.Checked = isNumeric;
                        c.Enabled = false;
                        this.Controls.Add(c);
                        y += 26;
                    }
                }
            }
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            string refField = refEventField[t.Name];
            string[] refFC = refField.Split('=');
            if (refFC.Length > 1)
            {
                Control[] cs = this.Controls.Find(refFC[0], false);
                if (cs.Length > 0)
                {
                    string FC = refFC[1];
                    string FieldType = FC.Substring(0, 3);
                    if (FieldType == "[0]")//文本框
                    {
                        TextBox tb = cs[0] as TextBox;
                        if (tb != null)
                        {
                            string con = FC.Substring(3);
                            string[] cons = con.Split('+');
                            StringBuilder sb = new StringBuilder();
                            bool lastIsRoot = false;
                            foreach (string s in cons)
                            {
                                string f = s;
                                if (s.EndsWith("{%_}"))
                                {
                                    int a = s.IndexOf("{%_}");
                                    f = s.Substring(0, a);
                                }
                                else if (s.EndsWith("{_%}"))
                                {
                                    int a = s.IndexOf("{_%}");
                                    f = s.Substring(0, a);
                                }
                                Control[] cs1 = this.Controls.Find(f, false);
                                if (cs1.Length > 0)
                                {
                                    string val = "";
                                    if (cs1[0] is TextBox)
                                    {
                                        TextBox tb1 = cs1[0] as TextBox;
                                        val = tb1.Text;
                                        if (s.EndsWith("{%_}"))
                                        {
                                            int index = val.IndexOf("_");
                                            if (index > -1)
                                                val = val.Substring(index + 1);
                                        }
                                        else if (s.EndsWith("{_%}"))
                                        {
                                            int index = val.IndexOf("_");
                                            if (index > -1)
                                                val = val.Substring(0, index);
                                        }
                                        else
                                        {
                                            sb.Append(val);
                                        }
                                    }
                                    else if (cs1[0] is ObjectList)
                                    {
                                        ObjectList cmb1 = cs1[0] as ObjectList;
                                        val = cmb1.SelectedObject.Text;
                                        if (val == "请选择")
                                        {
                                            lastIsRoot = true;
                                        }
                                        else
                                        {
                                            val = cmb1.SelectedObject.Text;
                                            if (s.EndsWith("{%_}"))
                                            {
                                                int index = val.IndexOf("_");
                                                if (index > -1)
                                                    val = val.Substring(index + 1);
                                            }
                                            else if (s.EndsWith("{_%}"))
                                            {
                                                int index = val.IndexOf("_");
                                                if (index > -1)
                                                    val = val.Substring(0, index);
                                            }
                                            else
                                            {
                                                sb.Append(val);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!lastIsRoot)
                                        sb.Append(s);
                                }
                            }
                            if (sb.Length > 0)
                                tb.Text = sb.ToString();
                        }
                    }
                    //else if (FieldType == "[1]")//下拉菜单，一般是上下级联动，暂时没有应用场景
                    //{
                    //    ObjectList cmb = cs[0] as ObjectList;
                    //    if (cmb != null)
                    //    {

                    //    }
                    //}
                }
            }
        }

        void o_SelectedIndexChanged(object sender, RefFieldArgs e)
        {
            //ObjectList ol = sender as ObjectList;
            string refField = refEventField[e.MasterField];
            string[] refFC = refField.Split('=');
            if (refFC.Length > 1)
            {
                Control[] cs = this.Controls.Find(refFC[0], false);
                if (cs.Length > 0)
                {
                    string FC = refFC[1];
                    string FieldType = FC.Substring(0, 3);
                    if (FieldType == "[0]")//文本框
                    {
                        TextBox tb = cs[0] as TextBox;
                        if (tb != null)
                        {
                            string con = FC.Substring(3);
                            string[] cons = con.Split('+');
                            StringBuilder sb = new StringBuilder();
                            bool lastIsRoot = false;
                            foreach (string s in cons)
                            {
                                string f = s;
                                if (s.EndsWith("{%_}"))
                                {
                                    int a = s.IndexOf("{%_}");
                                    f = s.Substring(0, a);
                                }
                                else if (s.EndsWith("{_%}"))
                                {
                                    int a = s.IndexOf("{_%}");
                                    f = s.Substring(0, a);
                                }
                                Control[] cs1 = this.Controls.Find(f, false);
                                if (cs1.Length > 0)
                                {
                                    if (cs1[0] is TextBox)
                                    {
                                        TextBox tb1 = cs1[0] as TextBox;
                                        string val = tb1.Text;
                                        if (s.EndsWith("{%_}"))
                                        {
                                            int index = val.IndexOf("_");
                                            if (index > -1)
                                                val = val.Substring(index + 1);
                                        }
                                        else if (s.EndsWith("{_%}"))
                                        {
                                            int index = val.IndexOf("_");
                                            if (index > -1)
                                                val = val.Substring(0, index);
                                        }
                                        sb.Append(val);
                                    }
                                    else if (cs1[0] is ObjectList)
                                    {
                                        ObjectList cmb1 = cs1[0] as ObjectList;
                                        if (cmb1.SelectedObject.Text == "请选择")
                                        {
                                            lastIsRoot = true;
                                        }
                                        else
                                        {
                                            string val = cmb1.SelectedObject.Text;
                                            if (s.EndsWith("{%_}"))
                                            {
                                                int index = val.IndexOf("_");
                                                if (index > -1)
                                                    val = val.Substring(index + 1);
                                            }
                                            else if (s.EndsWith("{_%}"))
                                            {
                                                int index = val.IndexOf("_");
                                                if (index > -1)
                                                    val = val.Substring(0, index);
                                            }
                                            sb.Append(val);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!lastIsRoot)
                                        sb.Append(s);
                                }
                            }
                            if (sb.Length > 0)
                                tb.Text = sb.ToString();
                        }
                    }
                    //else if (FieldType == "[1]")//下拉菜单，一般是上下级联动，暂时没有应用场景
                    //{
                    //    ObjectList cmb = cs[0] as ObjectList;
                    //    if (cmb != null)
                    //    {

                    //    }
                    //}
                }
            }
        }
    }
}
