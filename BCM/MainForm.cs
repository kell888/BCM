using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Xml;
using System.Configuration;
using MergeQueryUtil;

namespace BCM
{
    public partial class MainForm : Form
    {
        private string connStr;
        private string Pwd;
        private string table;
        private string tableSetting;
        private string primaryKey;
        private string primaryKeySetting;
        private string showField;
        private string parentKey;
        private string currentObjectFile;
        private string currentSettingFile;
        TreeNode currentNode;
        Dictionary<string, ObjectReference> currentSetting;
        private PrimaryKeyShowField currentPrimaryKeyShowField;
        Dictionary<string, Dictionary<string, bool>> nullables;
        Dictionary<string, Dictionary<string, ObjectClass>> ocss;
        const string RootNodeName = "顶级对象";
        const string RootNodeKey = "0";
        TreeNode rightClickNode;
        //bool loadFileToTree;
        SqlHelper SqlHelper;

        public Dictionary<string, Dictionary<string, bool>> Nullables
        {
            get { return nullables; }
        }

        public Dictionary<string, Dictionary<string, ObjectClass>> Ocss
        {
            get { return ocss; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateConnectionString(string connString)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Application.ExecutablePath + ".config");
            XmlNode xNode;
            XmlElement xElem;
            xNode = xDoc.SelectSingleNode("//connectionStrings");
            xElem = (XmlElement)xNode.SelectSingleNode("//add[@name='connString']");
            xElem.SetAttribute("connectionString", connString);
            xDoc.Save(Application.ExecutablePath + ".config");
        }

        private void UpdatePwd(string pwd)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Application.ExecutablePath + ".config");
            XmlNode xNode;
            XmlElement xElem;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='pwd']");
            xElem.SetAttribute("value", pwd);
            xDoc.Save(Application.ExecutablePath + ".config");
        }

        private void ChangeDB(string connString)
        {
            ConnectDB();
        }

        private void ConnectDB()
        {
            bool opened = false;
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            if (SqlHelper.CanConnect)
            {
                if (SqlHelper.Conn.State == ConnectionState.Open)
                {
                    opened = true;
                }
                else
                {
                    SqlHelper.Conn.Open();
                    opened = SqlHelper.IsOpened;
                }
            }
            if (opened)
            {
                打开对象表ToolStripMenuItem.Enabled = 打开配置表ToolStripMenuItem.Enabled = true;
                this.Text = "系统基础配置管理 -- " + SqlHelper.Conn.Database;
                toolStripStatusLabel1.Text = "数据库已连接";
                toolStripStatusLabel1.ForeColor = Color.Blue;
            }
            else
            {
                打开对象表ToolStripMenuItem.Enabled = 打开配置表ToolStripMenuItem.Enabled = true;
                this.Text = "系统基础配置管理 -- 不存在指定的数据库[" + SqlHelper.Conn.Database + "]";
                toolStripStatusLabel1.Text = "数据库已断开";
                toolStripStatusLabel1.ForeColor = SystemColors.ControlText;
            }
        }

        private void 连接配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBSetting db = new DBSetting();
            db.ConnectionString = connStr;
            if (db.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                connStr = db.ConnectionString;
                UpdateConnectionString(connStr);
                //ChangeDB(db.ConnectionString);
                ConnectDB();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出本系统吗？", "退出提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            PWD pwd = new PWD();
            if (pwd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string enableInit = ConfigurationManager.AppSettings["enableInit"];
                if (!string.IsNullOrEmpty(enableInit) && enableInit == "1")
                    初始化基础数据ToolStripMenuItem.Enabled = true;
                connStr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                Pwd = ConfigurationManager.AppSettings["pwd"];
                ocss = Common.GetObjectClassByCfg(out nullables);
                ConnectDB();
                Load_M_Object();
            }
            else
            {
                MessageBox.Show("您取消授权码登录，程序将退出...");
                Environment.Exit(0);
            }
        }

        private void Load_M_Object()
        {
            string m_object = ConfigurationManager.AppSettings["m_object"];
            if (!string.IsNullOrEmpty(m_object))
            {
                string[] paras = m_object.Split(',');
                if (paras.Length == 3)
                {
                    table = "m_object";
                    primaryKey = paras[0];
                    showField = paras[1];
                    parentKey = paras[2];
                    currentNode = LoadTableToTree(table, primaryKey, showField, parentKey);
                    //if (currentNode != null)
                    //    currentNode.Expand();
                    toolStripStatusLabel3.Text = "打开了数据库对象表[" + table + "]";
                    currentPrimaryKeyShowField = new PrimaryKeyShowField(primaryKey, showField);
                    //EditNode(currentNode, currentPrimaryKeyShowField);
                }
            }
        }

        private void 载入对象ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFileToTree(openFileDialog1.FileName);
            }
            openFileDialog1.Dispose();
        }

        private void LoadFileToTree(string filename)
        {
            //loadFileToTree = true;
            nodeEditor1.ClearNodes();
            panel6.Visible = false;
            currentObjectFile = filename;
            Xml2TreeView.ToTreeView(objectTree, filename);
            //DataSet ds = Common.LoadDataSetFromFile(filename);
            //if (ds != null)
            //{
            //    foreach (DataTable dt in ds.Tables)
            //    {
            //        TreeNode node = new TreeNode();
            //        if (dt.Columns.Count > 0 && dt.Rows.Count > 0)
            //        {
            //            Dictionary<string, object> record = new Dictionary<string, object>();
            //            for (int i = 0; i < dt.Columns.Count; i++)
            //            {
            //                record.Add(dt.Columns[i].ColumnName, dt.Rows[0][i]);
            //            }
            //            node.Name = dt.Rows[0][primaryKey].ToString();
            //            node.Text = dt.Rows[0][showField].ToString();
            //            node.Tag = record;
            //        }
            //        objectTree.Nodes.Add(node);
            //    }
            //}
            toolStripStatusLabel3.Text = "打开了对象文件[" + filename + "]";
            tabControl1.SelectedIndex = 0;
        }

        private void LoadFileToSetting(string filename)
        {
            settingEditor1.ClearSettings();
            btnUpdSetting.Enabled = btnDelSetting.Enabled = false;
            currentSettingFile = filename;
            DataTable dt = Common.LoadTableFromFile(filename);
            settingList.DataSource = dt;
            toolStripStatusLabel3.Text = "打开了配置文件[" + filename + "]";
            tabControl1.SelectedIndex = 1;
        }

        private void 保存对象ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TreeView2Xml.ToXml(objectTree, saveFileDialog1.FileName);
                MessageBox.Show("保存对象到文件成功！");
                //DataSet ds = Common.GetDataSetByTree(objectTree);
                //bool flag = Common.SaveDataSetToFile(ds, saveFileDialog1.FileName);
                //if (flag)
                //    MessageBox.Show("保存成功！");
                //else
                //    MessageBox.Show("请检查当前对象树是否无数据，或者保存的目录有无写入权限。保存失败！");
            }
            saveFileDialog1.Dispose();
        }

        private void 载入配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFileToSetting(openFileDialog1.FileName);
            }
            openFileDialog1.Dispose();
        }

        private void 保存配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool flag = Common.SaveTableToFile(settingList.DataSource as DataTable, saveFileDialog1.FileName);
                if (flag)
                    MessageBox.Show("保存配置到文件成功！");
                else
                    MessageBox.Show("请检查当前配置表是否有数据，或者保存的目录无有写入权限。保存失败！");
            }
            saveFileDialog1.Dispose();
        }

        private void 打开对象表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //loadFileToTree = false;
            ObjectTable ot = new ObjectTable();
            ot.TableName = table;
            ot.PrimaryKey = primaryKey;
            ot.ShowField = showField;
            ot.ParentKey = parentKey;
            if (ot.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                table = ot.TableName;
                primaryKey = ot.PrimaryKey;
                showField = ot.ShowField;
                parentKey = ot.ParentKey;
                currentNode = LoadTableToTree(table, primaryKey, showField, parentKey);
                //if (currentNode != null)
                //    currentNode.Expand();
                toolStripStatusLabel3.Text = "打开了数据库对象表[" + table + "]";
                tabControl1.SelectedIndex = 0;
                currentPrimaryKeyShowField = new PrimaryKeyShowField(primaryKey, showField);
                //EditNode(currentNode, currentPrimaryKeyShowField);
            }
        }

        private TreeNode LoadTableToTree(string table, string primaryKey, string showField, string parentKey, string expendNodeName = RootNodeKey)
        {
            nodeEditor1.ClearNodes();
            panel6.Visible = false;
            objectTree.Nodes.Clear();
            TreeNode root = objectTree.Nodes.Add(RootNodeKey, RootNodeName);
            TreeNode expendNode = null;
            string sql = "select * from " + table + " where " + parentKey + "=0 order by order_id asc";
            DataTable dt = SqlHelper.ExecuteQueryDataTable(sql);
            if (dt.Columns.Count > 0)
            {
                Dictionary<string, ObjectClass> foc = null;
                if (ocss.ContainsKey(table))
                    foc = ocss[table];
                Dictionary<string, bool> nullable = null;
                if (nullables.ContainsKey(table))
                    nullable = nullables[table];
                Dictionary<string, ObjectReference> top = new Dictionary<string, ObjectReference>();
                List<string> DbNulls = Common.GetDbNullFields();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string key = dt.Columns[i].ColumnName;
                    ObjectReference or = null;
                    if (foc != null && foc.ContainsKey(key))
                    {
                        bool isNullable = true;
                        if (nullable != null && nullable.ContainsKey(key))
                            isNullable = nullable[key];
                        ObjectClass oc = foc[key];
                        object value = 0;
                        if (DbNulls.Contains(key.ToLower()))
                            value = -1;
                        or = new ObjectReference(value, Common.IsNumericType(dt.Columns[i].DataType), isNullable, oc);
                    }
                    else
                    {
                        bool isNullable = true;
                        if (nullable != null && nullable.ContainsKey(key))
                            isNullable = nullable[key];
                        object value = 0;
                        if (DbNulls.Contains(key.ToLower()))
                            value = "";
                        if (key.ToLower().EndsWith("_time"))
                            value = DateTime.Now;
                        or = new ObjectReference(value, Common.IsNumericType(dt.Columns[i].DataType), isNullable);
                    }
                    top.Add(key, or);
                }
                root.Tag = top;
            }
            if (dt.Columns.Count > 0 && dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    TreeNode node = new TreeNode();
                    Dictionary<string, ObjectClass> foc = null;
                    if (ocss.ContainsKey(table))
                        foc = ocss[table];
                    Dictionary<string, bool> nullable = null;
                    if (nullables.ContainsKey(table))
                        nullable = nullables[table];
                    Dictionary<string, ObjectReference> record = new Dictionary<string, ObjectReference>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ObjectReference or = null;
                        if (foc != null && foc.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            bool isNullable = true;
                            if (nullable != null && nullable.ContainsKey(dt.Columns[i].ColumnName))
                                isNullable = nullable[dt.Columns[i].ColumnName];
                            ObjectClass oc = foc[dt.Columns[i].ColumnName];
                            or = new ObjectReference(dt.Rows[j][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable, oc);
                        }
                        else
                        {
                            bool isNullable = true;
                            if (nullable != null && nullable.ContainsKey(dt.Columns[i].ColumnName))
                                isNullable = nullable[dt.Columns[i].ColumnName];
                            or = new ObjectReference(dt.Rows[j][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable);
                        }
                        record.Add(dt.Columns[i].ColumnName, or);
                    }
                    node.Name = dt.Rows[j][primaryKey].ToString();
                    node.Text = dt.Rows[j][showField].ToString();
                    node.Tag = record;
                    root.Nodes.Add(node);
                    if (node.Name.Equals(expendNodeName, StringComparison.InvariantCultureIgnoreCase))
                        expendNode = node;
                }
            }
            objectTree.ExpandAll();
            return expendNode;
        }

        private void 打开配置表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingTable st = new SettingTable();
            st.TableName = tableSetting;
            if (!string.IsNullOrEmpty(primaryKeySetting))
            {
                st.PrimaryKey = primaryKeySetting;
                st.HasPrimaryKey = true;
            }
            if (st.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tableSetting = st.TableName;
                primaryKeySetting = st.HasPrimaryKey ? st.PrimaryKey : "";
                LoadTableToSetting(tableSetting);
            }
        }

        private void LoadTableToSetting(string tableSetting)
        {
            settingEditor1.ClearSettings();
            btnUpdSetting.Enabled = btnDelSetting.Enabled = false;
            DataTable dt = Common.LoadTableFromDB(tableSetting);
            settingList.DataSource = dt;
            toolStripStatusLabel3.Text = "打开了数据库配置表[" + tableSetting + "]";
            tabControl1.SelectedIndex = 1;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 授权管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auth au = new Auth();
            au.Pwd = Pwd;
            if (au.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Pwd = au.Pwd;
                UpdatePwd(Pwd);
                MessageBox.Show("修改成功！");
            }
        }

        private void settingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdSetting.Enabled = btnDelSetting.Enabled = true;
            if (e.RowIndex > -1)
            {
                DataTable dt = settingList.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<string, ObjectClass> foc = null;
                    if (ocss.ContainsKey(dt.TableName))
                        foc = ocss[dt.TableName];
                    Dictionary<string, bool> nullable = null;
                    if (nullables.ContainsKey(dt.TableName))
                        nullable = nullables[dt.TableName];
                    currentSetting = new Dictionary<string, ObjectReference>();
                    DataRow row = dt.Rows[e.RowIndex];
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        ObjectReference or = null;
                        if (foc != null && foc.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            bool isNullable = true;
                            if (nullable != null && nullable.ContainsKey(row.Table.Columns[i].ColumnName))
                                isNullable = nullable[row.Table.Columns[i].ColumnName];
                            ObjectClass oc = foc[row.Table.Columns[i].ColumnName];
                            or = new ObjectReference(row[i], Common.IsNumericType(row.Table.Columns[i].DataType), isNullable, oc);
                        }
                        else
                        {
                            bool isNullable = true;
                            if (nullable != null && nullable.ContainsKey(row.Table.Columns[i].ColumnName))
                                isNullable = nullable[row.Table.Columns[i].ColumnName];
                            or = new ObjectReference(row[i], Common.IsNumericType(row.Table.Columns[i].DataType), isNullable);
                        }
                        currentSetting.Add(row.Table.Columns[i].ColumnName, or);
                    }
                    EditSetting(currentSetting, primaryKeySetting);
                }
            }
        }

        private void EditSetting(Dictionary<string, ObjectReference> currentSetting, string primaryKeySetting)
        {
            settingEditor1.LoadSetting(currentSetting, primaryKeySetting);
        }

        private void EditNode(TreeNode currentNode, PrimaryKeyShowField pksf)
        {
            nodeEditor1.LoadNode(currentNode, pksf);
        }

        private void objectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panel6.Visible = true;
            btnUpdNode.Enabled = btnDelNode.Enabled = true;
            currentNode = e.Node;
            int object_type_id = 0;
            //if (loadFileToTree)
            //{
            //    Dictionary<string, ObjectReference> record = currentNode.Tag as Dictionary<string, ObjectReference>;
            //    object_type_id = GetObject_Type_Id(ref record);
            //    int parent_id = GetParent_Id(ref record);
            //    int device_type_id = GetDevice_Type_Id(ref record);
            //    int point_type_id = GetPoint_Type_Id(ref record);
            //    int device_id = GetDevice_Id(ref record);
            //    currentNode.Tag = record;
            //}
            //else
            //{
                Dictionary<string, ObjectReference> info = currentNode.Tag as Dictionary<string, ObjectReference>;
                if (info != null && info.ContainsKey("object_type_id"))
                {
                    int RET;
                    if (int.TryParse(info["object_type_id"].Value.ToString(), out RET))
                        object_type_id = RET;
                }
            //}
            if (GetChildrenCount(currentNode) > 0 && object_type_id > 0)
            {
                string deleteDisabledIfHaveChildrenObjectTypeIds = ConfigurationManager.AppSettings["deleteDisabledIfHaveChildrenObjectTypeIds"];
                if (!string.IsNullOrEmpty(deleteDisabledIfHaveChildrenObjectTypeIds))
                {
                    string[] otis = deleteDisabledIfHaveChildrenObjectTypeIds.Split('|');
                    List<int> objids = new List<int>();
                    foreach (string oti in otis)
                    {
                        int RET;
                        if (int.TryParse(oti, out RET))
                        {
                            objids.Add(RET);
                        }
                    }
                    if (objids.Contains(object_type_id))
                    {
                        btnDelNode.Enabled = false;
                    }
                }
            }
            if (currentNode.Name == RootNodeKey && currentNode.Text == RootNodeName)
            {
                btnUpdNode.Enabled = btnDelNode.Enabled = false;
                nodeEditor1.ClearNodes();
                return;
            }
            EditNode(currentNode, currentPrimaryKeyShowField);
            ThreadPool.QueueUserWorkItem(
                delegate
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            if (currentNode != null)
                            {
                                LoadChildren(currentNode);
                            }
                        }));
                    }
                    else
                    {
                        if (currentNode != null)
                        {
                            LoadChildren(currentNode);
                        }
                    }
                });
        }

        private int GetParent_Id(ref Dictionary<string, ObjectReference> record)
        {
            if (record.ContainsKey("parent_id"))
            {
                int parent_id = 0;
                ObjectReference copy = record["parent_id"];
                ObjectClass oc;
                bool IsReference = Extensions.IsReference(copy.Value.ToString(), out oc);
                if (IsReference)
                {
                    string val = Extensions.GetValue(copy.Value.ToString());
                    int RET;
                    if (int.TryParse(val, out RET))
                        parent_id = RET;
                    record["parent_id"] = new ObjectReference(parent_id, copy.IsNumeric, copy.Nullable, oc);
                }
                else
                {
                    int RET;
                    if (int.TryParse(copy.Value.ToString(), out RET))
                        parent_id = RET;
                    record["parent_id"] = new ObjectReference(parent_id, copy.IsNumeric, copy.Nullable);
                }
                return parent_id;
            }
            return 0;
        }

        private int GetDevice_Id(ref Dictionary<string, ObjectReference> record)
        {
            if (record.ContainsKey("device_id"))
            {
                int device_id = 0;
                ObjectReference copy = record["device_id"];
                ObjectClass oc;
                bool IsReference = Extensions.IsReference(copy.Value.ToString(), out oc);
                if (IsReference)
                {
                    string val = Extensions.GetValue(copy.Value.ToString());
                    int RET;
                    if (int.TryParse(val, out RET))
                        device_id = RET;
                    record["device_id"] = new ObjectReference(device_id, copy.IsNumeric, copy.Nullable, oc);
                }
                else
                {
                    int RET;
                    if (int.TryParse(copy.Value.ToString(), out RET))
                        device_id = RET;
                    record["device_id"] = new ObjectReference(device_id, copy.IsNumeric, copy.Nullable);
                }
                return device_id;
            }
            return 0;
        }

        private int GetPoint_Type_Id(ref Dictionary<string, ObjectReference> record)
        {
            if (record.ContainsKey("point_type_id"))
            {
                int point_type_id = 0;
                ObjectReference copy = record["point_type_id"];
                ObjectClass oc;
                bool IsReference = Extensions.IsReference(copy.Value.ToString(), out oc);
                if (IsReference)
                {
                    string val = Extensions.GetValue(copy.Value.ToString());
                    int RET;
                    if (int.TryParse(val, out RET))
                        point_type_id = RET;
                    record["point_type_id"] = new ObjectReference(point_type_id, copy.IsNumeric, copy.Nullable, oc);
                }
                else
                {
                    int RET;
                    if (int.TryParse(copy.Value.ToString(), out RET))
                        point_type_id = RET;
                    record["point_type_id"] = new ObjectReference(point_type_id, copy.IsNumeric, copy.Nullable);
                }
                return point_type_id;
            }
            return 0;
        }

        private int GetDevice_Type_Id(ref Dictionary<string, ObjectReference> record)
        {
            if (record.ContainsKey("device_type_id"))
            {
                int device_type_id = 0;
                ObjectReference copy = record["device_type_id"];
                ObjectClass oc;
                bool IsReference = Extensions.IsReference(copy.Value.ToString(), out oc);
                if (IsReference)
                {
                    string val = Extensions.GetValue(copy.Value.ToString());
                    int RET;
                    if (int.TryParse(val, out RET))
                        device_type_id = RET;
                    record["device_type_id"] = new ObjectReference(device_type_id, copy.IsNumeric, copy.Nullable, oc);
                }
                else
                {
                    int RET;
                    if (int.TryParse(copy.Value.ToString(), out RET))
                        device_type_id = RET;
                    record["device_type_id"] = new ObjectReference(device_type_id, copy.IsNumeric, copy.Nullable);
                }
                return device_type_id;
            }
            return 0;
        }

        private int GetObject_Type_Id(ref Dictionary<string, ObjectReference> record)
        {
            if (record.ContainsKey("object_type_id"))
            {
                int object_type_id = 0;
                ObjectReference copy = record["object_type_id"];
                ObjectClass oc;
                bool IsReference = Extensions.IsReference(copy.Value.ToString(), out oc);
                if (IsReference)
                {
                    string val = Extensions.GetValue(copy.Value.ToString());
                    int RET;
                    if (int.TryParse(val, out RET))
                        object_type_id = RET;
                    record["object_type_id"] = new ObjectReference(object_type_id, copy.IsNumeric, copy.Nullable, oc);
                }
                else
                {
                    int RET;
                    if (int.TryParse(copy.Value.ToString(), out RET))
                        object_type_id = RET;
                    record["object_type_id"] = new ObjectReference(object_type_id, copy.IsNumeric, copy.Nullable);
                }
                return object_type_id;
            }
            return 0;
        }

        private int GetChildrenCount(TreeNode currentNode)
        {
            Dictionary<string, ObjectReference> record = currentNode.Tag as Dictionary<string, ObjectReference>;
            string sql = "select count(*) from " + table + " where parent_id=" + Extensions.GetValue(record["parent_id"].Value.ToString());
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        private void LoadChildren(TreeNode currentNode)
        {
            currentNode.Nodes.Clear();
            if (currentNode.Name == RootNodeKey && currentNode.Text == RootNodeName)
            {
                string sql = "select * from " + table + " where " + parentKey + "=0 order by order_id asc";
                DataTable dt = SqlHelper.ExecuteQueryDataTable(sql);
                if (dt.Columns.Count > 0 && dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        TreeNode node = new TreeNode();
                        Dictionary<string, ObjectClass> foc = null;
                        if (ocss.ContainsKey(table))
                            foc = ocss[table];
                        Dictionary<string, bool> nullable = null;
                        if (nullables.ContainsKey(table))
                            nullable = nullables[table];
                        Dictionary<string, ObjectReference> record = new Dictionary<string, ObjectReference>();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            ObjectReference or = null;
                            if (foc != null && foc.ContainsKey(dt.Columns[i].ColumnName))
                            {
                                bool isNullable = true;
                                if (nullable != null && nullable.ContainsKey(dt.Columns[i].ColumnName))
                                    isNullable = nullable[dt.Columns[i].ColumnName];
                                ObjectClass oc = foc[dt.Columns[i].ColumnName];
                                or = new ObjectReference(dt.Rows[j][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable, oc);
                            }
                            else
                            {
                                bool isNullable = true;
                                if (nullable != null && nullable.ContainsKey(dt.Columns[i].ColumnName))
                                    isNullable = nullable[dt.Columns[i].ColumnName];
                                or = new ObjectReference(dt.Rows[j][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable);
                            }
                            record.Add(dt.Columns[i].ColumnName, or);
                        }
                        node.Name = dt.Rows[j][primaryKey].ToString();
                        node.Text = dt.Rows[j][showField].ToString();
                        node.Tag = record;
                        currentNode.Nodes.Add(node);
                    }
                }
            }
            else
            {
                List<Dictionary<string, ObjectReference>> children = Common.GetChildrenById(table, parentKey, currentNode.Name, ocss, nullables);
                foreach (Dictionary<string, ObjectReference> child in children)
                {
                    TreeNode node = new TreeNode();
                    node.Name = child[primaryKey].Value.ToString();
                    node.Text = child[showField].Value.ToString();
                    node.Tag = child;
                    currentNode.Nodes.Add(node);
                }
            }
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            if (currentNode == null)
            {
                MessageBox.Show("请先选定一个父节点！");
                return;
            }
            NewObjectForm nof = new NewObjectForm(currentNode, currentPrimaryKeyShowField);
            if (nof.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<string, ObjectReference> record = nof.Record;
                if (record != null)
                {
                    Dictionary<string, ObjectReference> trainInfo = Common.GetSubTableInfo("m_train", record);
                    Dictionary<string, ObjectReference> deviceInfo = Common.GetSubTableInfo("m_device", record);
                    Dictionary<string, ObjectReference> stationInfo = Common.GetSubTableInfo("m_station", record);
                    record.Remove(primaryKey);
                    object id;
                    //TreeNode parent = null;
                    //foreach (TreeNode node in objectTree.Nodes)
                    //{
                    //    parent = Common.GetNodeByName(node, record[parentKey].Value.ToString());
                    //    if (parent != null)
                    //        break;
                    //}
                    bool flag = Common.InsertRecordDB(record, table, out id);
                    if (flag)
                    {
                        string object_id = id.ToString();
                        string type = "";
                        bool f = true;
                        if (record["object_type_id"].Value.ToString() == "2")//列车
                        {
                            trainInfo["object_id"].Value = id;
                            f = Common.InsertRecordDB(trainInfo, "m_train", out id);
                            type = "列车";
                        }
                        else if (record["object_type_id"].Value.ToString() == "3")//设备
                        {
                            deviceInfo["object_id"].Value = id;
                            //int sdg_device_type_id=101;
                            //string SDG = ConfigurationManager.AppSettings["SDG"];
                            //if (!string.IsNullOrEmpty(SDG))
                            //{
                            //    sdg_device_type_id = SDG;
                            //}
                            //if (record["device_type_id"].Value.ToString() == sdg_device_type_id)
                            //{
                                string prefix = GetAddressPrefix(Convert.ToInt32(record["device_type_id"].Value));
                                deviceInfo.Add("address", new ObjectReference(prefix, false, true));
                                deviceInfo.Add("Param1", new ObjectReference("", false, true));
                            //}
                            f = Common.InsertRecordDB(deviceInfo, "m_device", out id);
                            type = "设备";
                        }
                        else if (record["object_type_id"].Value.ToString() == "12")//监测站
                        {
                            stationInfo["object_id"].Value = id;
                            f = Common.InsertRecordDB(stationInfo, "m_station", out id);
                            type = "监测站";
                        }
                        TreeNode newNode = LoadTableToTree(table, primaryKey, showField, parentKey, object_id);
                        objectTree.SelectedNode = newNode;
                        if (f)
                            MessageBox.Show("添加到数据库成功！");
                        else
                            MessageBox.Show("添加到数据库主表记录成功，但无法添加到" + type + "子表的记录中！");
                    }
                    else
                    {
                        MessageBox.Show("添加到数据库失败！");
                    }
                }
            }
        }
        /// <summary>
        /// 根据设备类型获取设备地址的前缀
        /// </summary>
        /// <param name="device_type_id"></param>
        /// <returns></returns>
        public static string GetAddressPrefix(int device_type_id)
        {
            string prefix = "";
            switch (device_type_id)
            {
                case 101:
                    prefix = "pantograph_";
                    break;
                case 102:
                    prefix = "train_";
                    break;
                case 103:
                    prefix = "plc_";
                    break;
                case 104:
                    prefix = "alnico_";
                    break;
                case 105:
                    prefix = "camera_";
                    break;
                case 106:
                    prefix = "vidicon_";
                    break;
                case 107:
                    prefix = "laser_";
                    break;
                case 108:
                    prefix = "stress_";
                    break;
            }
            return prefix;
        }

        private void btnUpdNode_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjectReference> record = nodeEditor1.Result.Tag as Dictionary<string, ObjectReference>;
            if (record != null)
            {
                Dictionary<string, ObjectReference> trainInfo = Common.GetSubTableInfo("m_train", record);
                Dictionary<string, ObjectReference> deviceInfo = Common.GetSubTableInfo("m_device", record);
                Dictionary<string, ObjectReference> stationInfo = Common.GetSubTableInfo("m_station", record);
                object object_id = record["object_id"].Value;
                object id = record[primaryKey].Value;
                record.Remove(primaryKey);
                bool flag = Common.UpdateRecordDB(record, table, primaryKey, id);
                if (flag)
                {
                    bool f = true;
                    if (record["object_type_id"].Value.ToString() == "2")//列车
                    {
                        trainInfo.Remove("object_id");
                        f = Common.UpdateRecordDB(trainInfo, "m_train", "object_id", object_id);
                    }
                    else if (record["object_type_id"].Value.ToString() == "3")//设备
                    {
                        deviceInfo.Remove("object_id");
                        f = Common.UpdateRecordDB(deviceInfo, "m_device", "object_id", object_id);
                    }
                    else if (record["object_type_id"].Value.ToString() == "12")//监测站
                    {
                        stationInfo.Remove("object_id");
                        f = Common.UpdateRecordDB(stationInfo, "m_station", "object_id", object_id);
                    }
                    currentNode.Text = nodeEditor1.Result.Text;
                    currentNode.Tag = nodeEditor1.Result.Tag;
                    if (f)
                        MessageBox.Show("更新到数据库成功！");
                    else
                        MessageBox.Show("更新到数据库主表记录成功，但无法更新到相应的子表记录！");
                }
                else
                {
                    MessageBox.Show("更新到数据库失败！");
                }
            }
        }

        private void btnDelNode_Click(object sender, EventArgs e)
        {
            if (currentNode == null)
            {
                MessageBox.Show("请先选定要删除的节点！");
                return;
            }
            if (MessageBox.Show("确定要将[" + currentNode.Text + "]删除吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (currentNode.Nodes.Count > 0)
                {
                    if (MessageBox.Show("节点[" + currentNode.Text + "]下面还有子节点，确定要将这些节点都删除吗？请慎重操作！！！", "子节点删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                Dictionary<string, ObjectReference> record = nodeEditor1.Result.Tag as Dictionary<string, ObjectReference>;
                if (record != null)
                {
                    object id = record[primaryKey].Value;
                    bool flag = Common.DeleteRecordDB(table, primaryKey, parentKey, id);
                    if (flag)
                    {
                        string type = "";
                        bool f = true;
                        object object_id = record["object_id"].Value;
                        if (record["object_type_id"].Value.ToString() == "2")//列车
                        {
                            f = Common.DeleteRecordDB("m_train", "object_id", object_id);
                            type = "列车";
                        }
                        else if (record["object_type_id"].Value.ToString() == "3")//设备
                        {
                            f = Common.DeleteRecordDB("m_device", "object_id", object_id);
                            type = "设备";
                        }
                        else if (record["object_type_id"].Value.ToString() == "12")//监测站
                        {
                            f = Common.DeleteRecordDB("m_station", "object_id", object_id);
                            type = "监测站";
                        }
                        if (currentNode.Parent != null)
                            currentNode.Parent.Nodes.Remove(currentNode);
                        else
                            objectTree.Nodes.Remove(currentNode);
                        currentNode = null;
                        if (f)
                            MessageBox.Show("从数据库中删除成功！");
                        else
                            MessageBox.Show("从数据库中删除主表记录成功，但无法删除" + type + "子表的记录！");
                    }
                    else
                    {
                        MessageBox.Show("从数据库中删除失败！");
                    }
                }
            }
        }

        private void btnAddSetting_Click(object sender, EventArgs e)
        {
            if (currentSetting == null)
            {
                MessageBox.Show("请先选定一个配置项！");
                return;
            }
            NewSettingForm nsf = new NewSettingForm(currentSetting, primaryKeySetting);
            if (nsf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<string, ObjectReference> record = nsf.Record;
                if (record != null)
                {
                    record.Remove(primaryKeySetting);
                    bool flag = false;
                    object id;
                    if (tableSetting.ToLower() == "s_ri_type" || tableSetting.ToLower() == "s_rm_type")
                    {
                        id = Common.GetFirstIdByWhere(tableSetting, primaryKeySetting, "device_type_id=" + record["device_type_id"].Value + " and point_type_id=" + record["point_type_id"].Value);
                        if (id != null && id != DBNull.Value)
                        {
                            if (MessageBox.Show("当前编辑的配置信息在系统中已经存在！是否要覆盖原来的配置？" + Environment.NewLine + "【确定】即覆盖更新" + Environment.NewLine + "【取消】则不覆盖不保存") == System.Windows.Forms.DialogResult.OK)
                            {
                                flag = Common.UpdateRecordDB(record, tableSetting, primaryKeySetting, id);
                            }
                        }
                        else
                        {
                            flag = Common.InsertRecordDB(record, tableSetting, out id);
                        }
                    }
                    else
                    {
                        flag = Common.InsertRecordDB(record, tableSetting, out id);
                    }
                    if (flag)
                    {
                        LoadTableToSetting(tableSetting);
                        currentSetting = Common.LoadRecordFromDB(tableSetting, primaryKeySetting, id, ocss, nullables);
                        MessageBox.Show("添加到数据库成功！");
                    }
                    else
                    {
                        MessageBox.Show("添加到数据库失败！");
                    }
                }
            }
        }

        private void btnUpdSetting_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjectReference> record = settingEditor1.Result;
            if (record != null && record.Count > 0)
            {
                bool flag = false;
                if (!string.IsNullOrEmpty(primaryKeySetting))
                {
                    object id = record[primaryKeySetting].Value;
                    record.Remove(primaryKeySetting);
                    flag = Common.UpdateRecordDB(record, tableSetting, primaryKeySetting, id);
                }
                else
                {
                    WhereTable wt = new WhereTable(record, tableSetting, false);
                    if (wt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string where = wt.Where;
                        flag = Common.UpdateRecordDB(record, tableSetting, where);
                    }
                }
                if (flag)
                {
                    currentSetting = record;
                    LoadTableToSetting(tableSetting);
                    MessageBox.Show("更新到数据库成功！");
                }
                else
                {
                    MessageBox.Show("更新到数据库失败！");
                }
            }
            else
            {
                MessageBox.Show("尚未选定要更新的记录！");
            }
        }

        private void btnDelSetting_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该记录吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<string, ObjectReference> record = settingEditor1.Result;
                if (record != null && record.Count > 0)
                {
                    bool flag = false;
                    if (!string.IsNullOrEmpty(primaryKeySetting))
                    {
                        object id = record[primaryKeySetting].Value;
                        flag = Common.DeleteRecordDB(tableSetting, primaryKeySetting, id);
                    }
                    else
                    {
                        WhereTable wt = new WhereTable(record, tableSetting, true);
                        if (wt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string where = wt.Where;
                            flag = Common.DeleteRecordDB(tableSetting, where);
                        }
                    }
                    if (flag)
                    {
                        currentSetting = null;
                        LoadTableToSetting(tableSetting);
                        MessageBox.Show("从数据库中删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("从数据库中删除失败！");
                    }
                }
                else
                {
                    MessageBox.Show("尚未选定要删除的记录！");
                }
            }
        }

        public bool Admin
        {
            get
            {
                AdminAuth aa = new AdminAuth();
                if (aa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DateTime now = DateTime.Now;
                    if (aa.Admin == now.Year.ToString().PadLeft(4, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0'))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("只有超级管理员才能配置该项。输入的授权码不正确！");
                    }
                }
                return false;
            }
        }

        private void 配置对象类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Admin)
            {
                tableSetting = "s_object_type";
                primaryKeySetting = "id";
                LoadTableToSetting(tableSetting);
            }
        }

        private void 配置设备类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Admin)
            {
                tableSetting = "s_device_type";
                primaryKeySetting = "id";
                LoadTableToSetting(tableSetting);
            }
        }

        private void 配置监测点类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Admin)
            {
                tableSetting = "s_point_type";
                primaryKeySetting = "id";
                LoadTableToSetting(tableSetting);
            }
        }

        private void 配置RMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableSetting = "s_rm_type";
            primaryKeySetting = "id";
            LoadTableToSetting(tableSetting);
        }

        private void 配置RIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableSetting = "s_ri_type";
            primaryKeySetting = "id";
            LoadTableToSetting(tableSetting);
        }

        private void 初始化基础数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("如果之前已经有了数据，这些数据会被覆盖！您确定要恢复原始的基础数据吗？", "数据覆盖提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                bool flag = Common.InitSystem();
                if (flag)
                {
                    MessageBox.Show("初始化数据成功！");
                    Load_M_Object();
                    toolStripStatusLabel3.Text = "初始化数据成功！";
                }
                else
                {
                    toolStripStatusLabel3.Text = "初始化数据完毕！";
                }
            }
        }

        private void 编辑mstationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string primaryKey = "";
            EditForm ef = new EditForm(this, "m_station", primaryKey, new List<string>() { "object_id", "object_name" });
            ef.ShowDialog();
        }

        private void 编辑mtrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string primaryKey = "";
            EditForm ef = new EditForm(this, "m_train", primaryKey, new List<string>() { "object_id", "object_name" });
            ef.ShowDialog();
        }

        private void 编辑mdeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string primaryKey = "";
            EditForm ef = new EditForm(this, "m_device", primaryKey, new List<string>() { "object_id", "object_name" });
            ef.ShowDialog();
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = objectTree.SelectedNode.Index;
            TreeNode parent = objectTree.SelectedNode.Parent;
            if (index > 0)
            {
                TreeNode tn = null;
                TreeNodeCollection tns = null;
                if (parent != null)
                {
                    parent.Nodes.Insert(index - 1, (TreeNode)objectTree.SelectedNode.Clone());
                    parent.Nodes.RemoveAt(index + 1);
                    tn = parent.Nodes[index - 1];
                    tns = parent.Nodes;
                }
                else
                {
                    objectTree.Nodes.Insert(index - 1, (TreeNode)objectTree.SelectedNode.Clone());
                    objectTree.Nodes.RemoveAt(index + 1);
                    tn = objectTree.Nodes[index - 1];
                    tns = objectTree.Nodes;
                }
                objectTree.SelectedNode = tn;
                if (tns != null)
                {
                    for (int i = 0; i < tns.Count; i++)
                    {
                        int object_id = Convert.ToInt32(tns[i].Name);
                        Common.UpdateObjectOrder(object_id, i);
                    }
                }
            }
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = objectTree.SelectedNode.Index;
            TreeNode parent = objectTree.SelectedNode.Parent;
            TreeNode tn = null;
            TreeNodeCollection tns = null;
            if (parent != null)
            {
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.Insert(index + 2, (TreeNode)objectTree.SelectedNode.Clone());
                    parent.Nodes.RemoveAt(index);
                    tn = parent.Nodes[index + 1];
                    tns = parent.Nodes;
                }
            }
            else
            {
                if (index < objectTree.Nodes.Count - 1)
                {
                    objectTree.Nodes.Insert(index + 2, (TreeNode)objectTree.SelectedNode.Clone());
                    objectTree.Nodes.RemoveAt(index);
                    tn = objectTree.Nodes[index + 1];
                    tns = objectTree.Nodes;
                }
            }
            objectTree.SelectedNode = tn;
            if (tns != null)
            {
                for (int i = 0; i < tns.Count; i++)
                {
                    int object_id = Convert.ToInt32(tns[i].Name);
                    Common.UpdateObjectOrder(object_id, i);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (rightClickNode != null)
                objectTree.SelectedNode = rightClickNode;
        }

        private void objectTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                rightClickNode = e.Node;
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTableToTree(table, primaryKey, showField, parentKey);
            if (currentNode != null)
                currentNode.Expand();
        }
    }
}
