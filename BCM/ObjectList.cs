using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MergeQueryUtil;

namespace BCM
{
    public partial class ObjectList : UserControl
    {
        public ObjectList()
        {
            InitializeComponent();
        }

        public event EventHandler<RefFieldArgs> SelectedIndexChanged;

        private void OnSelectedIndexChanged(RefFieldArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        public ListItem SelectedObject
        {
            get
            {
                return comboBox1.SelectedItem as ListItem;
            }
            set
            {
                comboBox1.SelectedItem = value;
            }
        }
        public void SetItemByKey(object key)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                ListItem li = comboBox1.Items[i] as ListItem;
                if (li.Value == key)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
        }

        public string FieldName
        {
            get
            {
                if (comboBox1.Tag != null)
                    return comboBox1.Tag.ToString();
                return "";
            }
        }
        ObjectClass oc;

        public ObjectClass ObjectClass
        {
            get
            {
                return oc;
            }
        }

        public void LoadAllObjects(string masterField, ObjectClass oc, object selected, bool allowNull = false)
        {
            comboBox1.Tag = masterField;
            comboBox1.Items.Clear();
            this.oc = oc;
            string orderby = "";
            if (!string.IsNullOrEmpty(oc.OrderByField))
                orderby = " order by " + oc.OrderByField + (oc.OrderAsc ? " asc" : " desc");
            string sql = "select * from " + oc.Table + orderby;
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            DataTable dt = SqlHelper.ExecuteQueryDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (allowNull)
                {
                    List<string> DbNulls = Common.GetDbNullFields();
                    if (DbNulls.Contains(oc.PrimaryKey.ToLower()))
                        comboBox1.Items.Add(new ListItem(DBNull.Value, "请选择"));
                    else
                        comboBox1.Items.Add(new ListItem(0, "请选择"));
                }
                int selectedIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string text = "";
                    object value = null;
                    if (dt.Rows[i][oc.ShowField] != null)
                        text = dt.Rows[i][oc.ShowField].ToString();
                    if (dt.Rows[i][oc.PrimaryKey] != null)
                        value = dt.Rows[i][oc.PrimaryKey];
                    comboBox1.Items.Add(new ListItem(value, text));
                    if (selected != null && selectedIndex == 0 && selected.ToString() == value.ToString())
                    {
                        selectedIndex = allowNull ? (i + 1) : i;
                    }
                }
                try
                {
                    comboBox1.SelectedIndex = selectedIndex;
                }
                catch
                {
                    if (comboBox1.Items.Count > 0)
                        comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = comboBox1.SelectedItem as ListItem;
            if (li != null)
                toolTip1.SetToolTip(comboBox1, "[" + li.Value + "]" + li.Text);
            OnSelectedIndexChanged(new RefFieldArgs(this.FieldName));
        }
    }

    public class RefFieldArgs : EventArgs
    {
        string masterField;

        public string MasterField
        {
            get { return masterField; }
            set { masterField = value; }
        }
        List<string> slaveFields;

        public List<string> SlaveFields
        {
            get { return slaveFields; }
            set { slaveFields = value; }
        }

        public RefFieldArgs()
        {
            this.slaveFields = new List<string>();
        }

        public RefFieldArgs(string masterField)
        {
            this.masterField = masterField;
            this.slaveFields = new List<string>();
        }

        public RefFieldArgs(string masterField, List<string> slaveFields)
        {
            this.masterField = masterField;
            if (slaveFields != null)
                this.slaveFields = slaveFields;
            else
                this.slaveFields = new List<string>();
        }
    }

    public class ListItem
    {
        object _value;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public ListItem(object _value, string _text)
        {
            this.Value = _value;
            this.Text = _text;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
