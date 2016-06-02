using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class EditForm : Form
    {
        public EditForm(MainForm owner, string table, string primaryKey, List<string> cantEditFields)
        {
            InitializeComponent();
            this.owner = owner;
            this.table = table;
            this.primaryKey = primaryKey;
            this.cantEditFields = cantEditFields;
            LoadTable(table);
        }

        MainForm owner;
        string table;
        string primaryKey;
        List<string> cantEditFields;

        public Dictionary<string, ObjectReference> Record
        {
            get
            {
                return settingEditor1.Result;
            }
        }

        private void Edit(Dictionary<string, ObjectReference> setting, string primaryKey, List<string> cantEditFields)
        {
            settingEditor1.EditSetting(setting, primaryKey, cantEditFields);
        }

        private void LoadTable(string table)
        {
            DataTable dt = Common.LoadTableFromDB(table);
            settingList.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string where = " and object_id=" + settingEditor1.Result["object_id"].Value;
            flag = Common.UpdateRecordDB(settingEditor1.Result, table, where);
            if (flag)
            {
                LoadTable(table);
                MessageBox.Show("更新到数据库成功！");
            }
            else
            {
                MessageBox.Show("更新到数据库失败！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void settingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataTable dt = settingList.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<string, ObjectReference> setting = new Dictionary<string, ObjectReference>();
                    Dictionary<string, ObjectClass> foc = null;
                    if (owner.Ocss.ContainsKey(dt.TableName))
                        foc = owner.Ocss[dt.TableName];
                    Dictionary<string, bool> nullable = null;
                    if (owner.Nullables.ContainsKey(dt.TableName))
                        nullable = owner.Nullables[dt.TableName];
                    DataRow row = dt.Rows[e.RowIndex];
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        ObjectReference or = null;
                        bool isNullable = true;
                        if (foc != null && foc.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            if (nullable != null && nullable.ContainsKey(row.Table.Columns[i].ColumnName))
                                isNullable = nullable[row.Table.Columns[i].ColumnName];
                            ObjectClass oc = foc[row.Table.Columns[i].ColumnName];
                            or = new ObjectReference(row[i], Common.IsNumericType(row.Table.Columns[i].DataType), isNullable, oc);
                        }
                        else
                        {
                            if (nullable != null && nullable.ContainsKey(row.Table.Columns[i].ColumnName))
                                isNullable = nullable[row.Table.Columns[i].ColumnName];
                            or = new ObjectReference(row[i], Common.IsNumericType(row.Table.Columns[i].DataType), isNullable);
                        }
                        setting.Add(row.Table.Columns[i].ColumnName, or);
                    }
                    Edit(setting, primaryKey, cantEditFields);
                }
            }
        }
    }
}
