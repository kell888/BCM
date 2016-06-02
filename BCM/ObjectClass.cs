using System;
using System.Collections.Generic;
using System.Text;

namespace BCM
{
    public class ObjectClass
    {
        string table;

        public string Table
        {
            get { return table; }
            set { table = value; }
        }
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
        string orderByField;

        public string OrderByField
        {
            get { return orderByField; }
            set { orderByField = value; }
        }
        bool orderAsc;

        public bool OrderAsc
        {
            get { return orderAsc; }
            set { orderAsc = value; }
        }

        public ObjectClass(string table, string primaryKey, string showField, string orderByField, bool orderAsc)
        {
            this.Table = table;
            this.PrimaryKey = primaryKey;
            this.ShowField = showField;
            this.OrderByField = orderByField;
            this.OrderAsc = orderAsc;
        }

        public override string ToString()
        {
            return "[" + table + ";" + primaryKey + ";" + showField + ";" + orderByField + ";" + orderAsc + "]";
        }
    }
}
