using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MergeQueryUtil;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

namespace BCM
{
    public class PrimaryKeyShowField
    {
        string primaryKey;

        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        string showField;

        public string ShowField
        {
            get { return showField; }
            set { showField = value; }
        }

        public PrimaryKeyShowField(string primaryKey, string showField)
        {
            this.PrimaryKey = primaryKey;
            this.ShowField = showField;
        }
    }
    public class IsNumericIsNullable
    {
        bool isNumeric;

        public bool IsNumeric
        {
            get { return isNumeric; }
            set { isNumeric = value; }
        }
        bool nullable;

        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }

        public IsNumericIsNullable(bool isNumeric, bool nullable)
        {
            this.IsNumeric = isNumeric;
            this.Nullable = nullable;
        }
    }
    public static class Common
    {
        private static readonly object syncObj = new object();
        public static bool IsNumber(string s)
        {
            string pattern = @"^\d+(\.)?\d*$";
            if (!string.IsNullOrEmpty(s))
            {
                if (!Regex.IsMatch(s, pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static Dictionary<string, Dictionary<string, ObjectClass>> GetObjectClassByCfg(out Dictionary<string, Dictionary<string, bool>> nullables)
        {
            nullables = new Dictionary<string, Dictionary<string, bool>>();
            Dictionary<string, Dictionary<string, ObjectClass>> ocss = new Dictionary<string, Dictionary<string, ObjectClass>>();
            string refStr = ConfigurationManager.AppSettings["ref"];
            if (!string.IsNullOrEmpty(refStr))
            {
                string[] tables = refStr.Split('|');
                foreach (string t in tables)
                {
                    int a = t.IndexOf("(");//asd(field1:NotNullable[;;;],field2:Nullable[;;;],field3:Nullable[;;;])
                    string TB = t.Substring(0, a);//asd
                    string TBOC = t.Substring(a + 1);
                    string FS = TBOC.Substring(0, TBOC.Length - 1);
                    string[] refFields = FS.Split(',');
                    Dictionary<string, bool> nullable = new Dictionary<string, bool>();
                    Dictionary<string, ObjectClass> ocs = new Dictionary<string, ObjectClass>();
                    foreach (string f in refFields)//f=field1:NotNullable[;;;]
                    {
                        int b = f.IndexOf(":");
                        string F = f.Substring(0, b);
                        nullable.Add(F, Extensions.IsNullable(f));
                        ObjectClass oc;
                        Extensions.IsReference(f, out oc);
                        if (oc != null)
                            ocs.Add(F, oc);
                    }
                    nullables.Add(TB, nullable);
                    ocss.Add(TB, ocs);
                }
            }
            return ocss;
        }
        public static bool IsNumericType(Type type)
        {
            if (type.Equals(typeof(Int16)) || type.Equals(typeof(Int32)) || type.Equals(typeof(Int64)) || type.Equals(typeof(UInt16)) || type.Equals(typeof(UInt32)) || type.Equals(typeof(UInt64)) || type.Equals(typeof(Byte)) || type.Equals(typeof(SByte)) || type.Equals(typeof(Decimal)) || type.Equals(typeof(Single)) || type.Equals(typeof(Double)))
            {
                return true;
            }
            return false;
        }
        public static bool SaveDataSetToFile(DataSet dataSet, string filename)
        {
            if (dataSet == null)
                return false;
            try
            {
                dataSet.WriteXml(filename, XmlWriteMode.WriteSchema);
                return true;
            }
            catch (Exception e)
            {
                Logs.Error("写入文件出错：" + e.Message);
                return false;
            }
        }

        public static bool SaveTableToFile(DataTable data, string filename)
        {
            if (data == null || data.Columns.Count == 0)
                return false;
            try
            {
                data.WriteXml(filename, XmlWriteMode.WriteSchema);
                return true;
            }
            catch (Exception e)
            {
                Logs.Error("写入文件出错：" + e.Message);
                return false;
            }
        }

        public static DataSet LoadDataSetFromFile(string filename)
        {
            DataSet ds = new DataSet();
            try
            {
                XmlReadMode xrm = ds.ReadXml(filename);
            }
            catch (Exception e)
            {
                Logs.Error("读取文件出错：" + e.Message);
            }
            return ds;
        }

        public static DataTable LoadTableFromFile(string filename)
        {
            DataTable data = new DataTable();
            try
            {
                XmlReadMode xrm = data.ReadXml(filename);
            }
            catch (Exception e)
            {
                Logs.Error("读取文件出错：" + e.Message);
            }
            return data;
        }

        public static DataTable LoadTableFromDB(string table)
        {
            DataTable dt = new DataTable(table);
            try
            {
                SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                string sql = "select * from " + table;
                dt = SqlHelper.ExecuteQueryDataTable(sql);
                dt.TableName = table;
            }
            catch (Exception e)
            {
                Logs.Error("读取文件出错：" + e.Message);
            }
            return dt;
        }
        /// <summary>
        /// 删除指定ID的记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="primaryKey"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteRecordDB(string table, string primaryKey, object id)
        {
            lock (syncObj)
            {
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@id", id));
                    SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                    string sql = "delete from " + table + " where " + primaryKey + "=@id";
                    int i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param.ToArray());
                    return i > 0;
                }
                catch (Exception e)
                {
                    Logs.Error("删除数据出错：" + e.Message);
                    MessageBox.Show("删除数据出错：" + e.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// 此操作会循环将指定的节点删除，连同它下面的所有子节点！
        /// </summary>
        /// <param name="table"></param>
        /// <param name="primaryKey"></param>
        /// <param name="parentKey"></param>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public static bool DeleteRecordDB(string table, string primaryKey, string parentKey, object nodeid)
        {
            lock (syncObj)
            {
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@table", table));
                    param.Add(new SqlParameter("@primaryKey", primaryKey));
                    param.Add(new SqlParameter("@parentKey", parentKey));
                    param.Add(new SqlParameter("@nodeid", nodeid));
                    SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                    int i = SqlHelper.ExecuteNonQuery("usp_DeleteNode", CommandType.StoredProcedure, param.ToArray());
                    return i > 0;
                }
                catch (Exception e)
                {
                    Logs.Error("级联删除数据出错：" + e.Message);
                    MessageBox.Show("级联删除数据出错：" + e.Message);
                }
                return false;
            }
        }

        public static bool UpdateRecordDB(Dictionary<string, ObjectReference> fields, string table, string primaryKey, object id)
        {
            lock (syncObj)
            {
                if (fields.Count > 0)
                {
                    try
                    {
                        Dictionary<string, ObjectReference> record = fields;
                        StringBuilder sb = new StringBuilder();
                        List<SqlParameter> param = new List<SqlParameter>();
                        foreach (string f in record.Keys)
                        {
                            sb.Append(f + "=@" + f + ",");
                            param.Add(new SqlParameter("@" + f, record[f].Value));
                        }
                        string s = sb.ToString().Substring(0, sb.Length - 1) + " where " + primaryKey + "=@id";
                        param.Add(new SqlParameter("@id", id));
                        SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                        string sql = "update " + table + " set " + s;
                        int i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param.ToArray());
                        return i > 0;
                    }
                    catch (Exception e)
                    {
                        Logs.Error("更新数据出错：" + e.Message);
                        MessageBox.Show("更新数据出错：" + e.Message);
                    }
                }
                return false;
            }
        }

        public static bool InsertRecordDB(Dictionary<string, ObjectReference> fields, string table, out object id)
        {
            lock (syncObj)
            {
                id = null;
                if (fields.Count > 0)
                {
                    try
                    {
                        Dictionary<string, ObjectReference> record = fields;
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        List<SqlParameter> param = new List<SqlParameter>();
                        sb.Append("(");
                        sb2.Append("(");
                        foreach (string f in record.Keys)
                        {
                            sb.Append(f + ",");
                            sb2.Append("@" + f + ",");
                            param.Add(new SqlParameter("@" + f, record[f].Value));
                        }
                        string s = sb.ToString().Substring(0, sb.Length - 1);
                        string s2 = sb2.ToString().Substring(0, sb2.Length - 1);
                        s += ")";
                        s2 += ")";
                        SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                        string sql = "insert into " + table + " " + s + " values " + s2;
                        int i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param.ToArray());
                        id = SqlHelper.ExecuteScalar("SELECT IDENT_CURRENT('" + table + "')");
                        return i > 0;
                    }
                    catch (Exception e)
                    {
                        Logs.Error("插入数据出错：" + e.Message);
                        MessageBox.Show("插入数据出错：" + e.Message);
                    }
                }
                return false;
            }
        }

        public static Dictionary<string, ObjectReference> LoadRecordFromDB(string table, string primaryKey, object id, Dictionary<string, Dictionary<string, ObjectClass>> ocss, Dictionary<string, Dictionary<string, bool>> nullables)
        {
            Dictionary<string, ObjectReference> record = new Dictionary<string, ObjectReference>();
            try
            {
                StringBuilder sb = new StringBuilder();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@id", id));
                SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                string sql = "select * from " + table + " where " + primaryKey + "=@id";
                DataTable dt = SqlHelper.ExecuteQueryDataTable(sql, CommandType.Text, param.ToArray());
                if (dt.Rows.Count > 0)
                {
                    Dictionary<string, ObjectClass> foc = null;
                    if (ocss.ContainsKey(table))
                        foc = ocss[table];
                    Dictionary<string, bool> nullable = null;
                    if (nullables.ContainsKey(table))
                        nullable = nullables[table];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ObjectReference or = null;
                        bool isNullable = true;
                        if (nullable != null && nullable.ContainsKey(dt.Columns[i].ColumnName))
                            isNullable = nullable[dt.Columns[i].ColumnName];
                        if (foc != null && foc.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            ObjectClass oc = foc[dt.Columns[i].ColumnName];
                            or = new ObjectReference(dt.Rows[0][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable, oc);
                        }
                        else
                        {
                            or = new ObjectReference(dt.Rows[0][i], Common.IsNumericType(dt.Columns[i].DataType), isNullable);
                        }
                        record.Add(dt.Columns[i].ColumnName, or);
                    }
                }
            }
            catch (Exception e)
            {
                Logs.Error("打开数据出错：" + e.Message);
            }
            return record;
        }

        [Obsolete("暂时不能用")]
        public static DataSet GetDataSetByTree(TreeView tree)
        {
            DataSet ds = new DataSet();
            foreach (TreeNode node in tree.Nodes)
            {
                DataTable dt = GetDataTableByNode(node);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        [Obsolete("暂时不能用，因为函数尚未实现")]
        public static DataTable GetDataTableByNode(TreeNode node)
        {
            throw new NotImplementedException();
        }

        public static List<Dictionary<string, ObjectReference>> GetChildrenById(string table, string parentKey, string parentidvalue, Dictionary<string, Dictionary<string, ObjectClass>> ocss, Dictionary<string, Dictionary<string, bool>> nullables)
        {
            List<Dictionary<string, ObjectReference>> children = new List<Dictionary<string, ObjectReference>>();
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string sql = "select * from " + table + " where " + parentKey + "=" + parentidvalue + " order by order_id asc";
            DataTable dt = SqlHelper.ExecuteQueryDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, ObjectClass> foc = null;
                if (ocss.ContainsKey(table))
                    foc = ocss[table];
                Dictionary<string, bool> nullable = null;
                if (nullables.ContainsKey(table))
                    nullable = nullables[table];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dictionary<string, ObjectReference> child = new Dictionary<string, ObjectReference>();
                    try
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ObjectReference or = null;
                            bool isNullable = true;
                            if (nullable != null && nullable.ContainsKey(dt.Columns[j].ColumnName))
                                isNullable = nullable[dt.Columns[j].ColumnName];
                            if (foc != null && foc.ContainsKey(dt.Columns[j].ColumnName))
                            {
                                ObjectClass oc = foc[dt.Columns[j].ColumnName];
                                or = new ObjectReference(dt.Rows[i][j], Common.IsNumericType(dt.Columns[j].DataType), isNullable, oc);
                            }
                            else
                            {
                                or = new ObjectReference(dt.Rows[i][j], Common.IsNumericType(dt.Columns[j].DataType), isNullable);
                            }
                            child.Add(dt.Columns[j].ColumnName, or);
                        }
                    }
                    catch (Exception e)
                    {
                        Logs.Error("获取子树时查询数据出错：" + e.Message);
                    }
                    children.Add(child);
                }
            }
            return children;
        }

        public static bool DeleteRecordDB(string table, string where)
        {
            try
            {
                SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                string w = "";
                if (!string.IsNullOrEmpty(where))
                    w = " where (1=1) " + where;
                string sql = "delete from " + table + w;
                int i = SqlHelper.ExecuteNonQuery(sql);
                return i > 0;
            }
            catch (Exception e)
            {
                Logs.Error("删除数据出错：" + e.Message);
                MessageBox.Show("删除数据出错：" + e.Message);
            }
            return false;
        }

        public static bool UpdateRecordDB(Dictionary<string, ObjectReference> record, string table, string where)
        {
            if (record.Count > 0)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    List<SqlParameter> param = new List<SqlParameter>();
                    foreach (string f in record.Keys)
                    {
                        sb.Append(f + "=@" + f + ",");
                        param.Add(new SqlParameter("@" + f, record[f].Value));
                    }
                    string s = sb.ToString().Substring(0, sb.Length - 1);
                    SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
                    string w = "";
                    if (!string.IsNullOrEmpty(where))
                        w = " where (1=1) " + where;
                    string sql = "update " + table + " set " + s + w;
                    int i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param.ToArray());
                    return i > 0;
                }
                catch (Exception e)
                {
                    Logs.Error("更新数据出错：" + e.Message);
                    MessageBox.Show("更新数据出错：" + e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// 自上往下查找对应的键值节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TreeNode GetNodeByName(TreeNode node, string name)
        {
            TreeNode nod = null;
            if (node.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            {
                nod = node;
            }
            else
            {
                foreach (TreeNode n in node.Nodes)
                {
                    nod = GetNodeByName(n, name);
                    if (nod != null)
                        break;
                }
            }
            return nod;
        }

        public static object GetFirstIdByWhere(string table, string primaryKeyName, string where)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string w = "";
            if (!string.IsNullOrEmpty(where))
                w = " where (1=1) " + where;
            string sql = "select primaryKeyName from " + table + w;
            return SqlHelper.ExecuteScalar(sql);
        }

        public static int GetIndexByNode(TreeNodeCollection brothers, TreeNode node)
        {
            foreach (TreeNode n in brothers)
            {
                if (n.Name == node.Name)
                    return n.Index;
            }
            return -1;
        }

        public static bool InitSystem()
        {
            string basIni = "createBasedata.sql";
            string initBaseSystem = ConfigurationManager.AppSettings["initBaseSystem"];
            if (!string.IsNullOrEmpty(initBaseSystem))
            {
                basIni = initBaseSystem;
            }
            string ini = "p_test_config_table.sql";
            string initSystem = ConfigurationManager.AppSettings["initSystem"];
            if (!string.IsNullOrEmpty(initSystem))
            {
                ini = initSystem;
            }
            string sql = File.ReadAllText(ini, Encoding.UTF8);
            string sql1 = File.ReadAllText(basIni, Encoding.UTF8);
            if (!string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(sql1))
            {
                SqlHelper SqlHelper = new SqlHelper("connString");
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(sql1);
                    SqlParameter[] paras = new SqlParameter[]
                    {
                        new SqlParameter("@strsql", SqlDbType.Text)
                    };
                    paras[0].Value = sql;
                    i = SqlHelper.ExecuteNonQuery("dbo.CreateBaseData", CommandType.StoredProcedure, paras);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("初始化系统的基础数据失败，可能系统数据库中已经存在基础数据！" + Environment.NewLine + e.Message);
                }
            }
            return false;
        }

        public static List<string> GetDbNullFields()
        {
            List<string> DbNulls = new List<string>();
            string DBNullFields = ConfigurationManager.AppSettings["DBNullFields"];
            if (!string.IsNullOrEmpty(DBNullFields))
            {
                string[] dbNulls = DBNullFields.Split(',');
                foreach (string dbn in dbNulls)
                {
                    DbNulls.Add(dbn.Trim().ToLower());
                }
            }
            return DbNulls;
        }

        public static Dictionary<string,string> GetRefEventField()
        {
            Dictionary<string, string> refFields = new Dictionary<string, string>();
            string RefEventField = ConfigurationManager.AppSettings["RefEventField"];
            if (!string.IsNullOrEmpty(RefEventField))
            {
                string[] refEventFields = RefEventField.Split('|');
                foreach (string r in refEventFields)
                {
                    string[] rr = r.Split(':');
                    refFields.Add(rr[0], rr[1]);
                }
            }
            return refFields;
        }

        public static Dictionary<string, ObjectReference> GetSubTableInfo(string table, Dictionary<string, ObjectReference> record)
        {
            Dictionary<string, ObjectReference> info = new Dictionary<string, ObjectReference>();
            switch (table)
            {
                case "m_device":
                    ObjectReference or = new ObjectReference(record["device_type_id"].Value, true, false);
                    info.Add("device_type_id", or);
                    break;
                case "":
                default:
                    break;
            }
            ObjectReference or1 = new ObjectReference(record["object_id"].Value, true, false);
            info.Add("object_id", or1);
            ObjectReference or2 = new ObjectReference(record["object_name"].Value, false, true);
            info.Add("object_name", or2);
            return info;
        }

        public static void UpdateObjectOrder(int object_id, int order_id)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string sql = "update m_object set order_id=" + order_id + " where object_id=" + object_id;
            SqlHelper.ExecuteNonQuery(sql);
        }

        public static int GetObjectCount(int object_type_id)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string sql = "select count(*) from m_object where object_type_id=" + object_type_id;
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        public static int GetObjectLevelByObjectId(object object_id)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string sql = "select s_object_type.object_level from s_object_type,m_object where s_object_type.object_type_id=m_object.object_type_id and m_object.object_id=" + object_id.ToString();
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        public static int GetObjectLevelByObjectTypeId(object object_type_id)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string sql = "select object_level from s_object_type where object_type_id=" + object_type_id.ToString();
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        public static object GetFirstSubObjectType(int object_level)
        {
            SqlHelper SqlHelper = new MergeQueryUtil.SqlHelper("connString");
            string exists = "select * from s_object_type where object_level=" + object_level.ToString();
            object obj = SqlHelper.ExecuteScalar(exists);
            if (obj == null || obj == DBNull.Value)//找不到的就默认为顶级节点
            {
                exists = "select top 1 object_type_id from s_object_type where object_level=(select min(object_level) from s_object_type) order by object_type_id asc";
                obj = SqlHelper.ExecuteScalar(exists);
                if (obj != null && obj != DBNull.Value)
                {
                    return Convert.ToInt32(obj);
                }
            }
            string sql = "select top 1 object_type_id from s_object_type where object_level>" + object_level + " order by object_level asc";
            obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return obj;
            }
            else
            {
                sql = "select top 1 object_type_id from s_object_type where object_level=(select max(object_level) from s_object_type) order by object_type_id desc";
                obj = SqlHelper.ExecuteScalar(sql);
                if (obj != null && obj != DBNull.Value)
                {
                    return obj;
                }
            }
            return null;
        }
    }
    public static class Extensions
    {
        public static string ConvertToString(Dictionary<string, ObjectReference> me)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in me.Keys)
            {
                //{field,value:Nullable[Table;PrimaryKey;ShowField;OrderByField;OrderAsc]}
                sb.Append("{");
                sb.Append(key);
                sb.Append(",");
                if (!me[key].IsNumeric)
                    sb.Append("'");
                sb.Append(me[key].Value.ToString());
                if (!me[key].IsNumeric)
                    sb.Append("'");
                if (me[key].Nullable)
                    sb.Append(":Nullable");
                else
                    sb.Append(":NotNullable");
                if (me[key].IsReference)
                {
                    sb.Append("[");
                    sb.Append(me[key].RefObject.Table);
                    sb.Append(";" + me[key].RefObject.PrimaryKey);
                    sb.Append(";" + me[key].RefObject.ShowField);
                    sb.Append(";" + me[key].RefObject.OrderByField);
                    sb.Append(";" + me[key].RefObject.OrderAsc);
                    sb.Append("]");
                }
                sb.Append("}");
            }
            return sb.ToString();
        }

        public static Dictionary<string, ObjectReference> ConvertToDictionary(string me)
        {
            Dictionary<string, ObjectReference> dict = new Dictionary<string, ObjectReference>();
            try
            {
                string[] mes = me.Split(new string[] {"}{"}, StringSplitOptions.None);
                string first = mes[0].Substring(1);//去掉{
                string[] ele = first.Split(',');
                ObjectClass oc;
                bool isReference = IsReference(first, out oc);
                ObjectReference or = null;
                if (isReference)
                    or = new ObjectReference(GetValue(ele[1]), IsNumeric(ele[1]), IsNullable(ele[1]), oc);
                else
                    or = new ObjectReference(GetValue(ele[1]), IsNumeric(ele[1]), IsNullable(ele[1]));
                dict.Add(ele[0], or);
                for (int i = 1; i < mes.Length - 1; i++)
                {
                    string item = mes[i];
                    string[] its = item.Split(',');
                    ObjectClass oc1;
                    isReference = IsReference(item, out oc1);
                    ObjectReference or1 = null;
                    if (isReference)
                        or1 = new ObjectReference(GetValue(its[1]), IsNumeric(its[1]), IsNullable(its[1]), oc1);
                    else
                        or1 = new ObjectReference(GetValue(its[1]), IsNumeric(its[1]), IsNullable(its[1]));
                    dict.Add(its[0], or1);
                }
                if (mes.Length > 1)
                {
                    string last = mes[mes.Length - 1].Substring(0, mes[mes.Length - 1].Length - 1);//去掉}
                    string[] els = last.Split(',');
                    ObjectClass oc2;
                    isReference = IsReference(last, out oc2);
                    ObjectReference or2 = null;
                    if (isReference)
                        or2 = new ObjectReference(GetValue(els[1]), IsNumeric(els[1]), IsNullable(els[1]), oc2);
                    else
                        or2 = new ObjectReference(GetValue(els[1]), IsNumeric(els[1]), IsNullable(els[1]));
                    dict.Add(els[0], or2);
                }
            }
            catch (Exception e)
            {
                Logs.Error("ConvertToDictionary出错：" + e.Message);
            }
            return dict;
        }

        public static bool IsReference(string e, out ObjectClass oc)
        {
            oc = null;
            bool isReference = false;
            if (e.EndsWith("]"))
            {
                isReference = true;
                try
                {
                    //{field,value:Nullable[Table;PrimaryKey;ShowField;OrderByField;OrderAsc]}
                    int index = e.IndexOf("[");
                    if (index > -1)
                    {
                        string ocStr = e.Substring(index + 1, e.Length - index - 2);
                        string[] ocs = ocStr.Split(';');
                        if (ocs.Length == 5)
                        {
                            string table = ocs[0];
                            string primaryKey = ocs[1];
                            string showField = ocs[2];
                            string orderByField = ocs[3];
                            bool orderAsc;
                            bool.TryParse(ocs[4], out orderAsc);
                            oc = new ObjectClass(table, primaryKey, showField, orderByField, orderAsc);
                        }
                    }
                }
                catch { }
            }
            return isReference;
        }

        public static bool IsNullable(string e)
        {
            bool isNullable = false;
            if (e.IndexOf(":Nullable") > -1)
                isNullable = true;
            return isNullable;
        }

        private static bool IsNumeric(string e)
        {
            bool isNumeric = true;
            if (e.StartsWith("'"))
                isNumeric = false;
            return isNumeric;
        }

        public static string GetValue(string rawValue)
        {
            string ret = "";
            if (rawValue != null)
            {
                ret = rawValue;
                int index = ret.IndexOf("[");
                if (index > -1)
                {
                    ret = ret.Substring(0, index);
                }
                int a = ret.IndexOf(":Nullable");
                if (a > -1)
                    ret = ret.Substring(0, a);
                int b = ret.IndexOf(":NotNullable");
                if (b > -1)
                    ret = ret.Substring(0, b);
            }
            return ret;
        }
    }
}
