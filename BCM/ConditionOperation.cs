using System;
using System.Collections.Generic;
using System.Text;

namespace BCM
{
    public enum OperConcat
    {
        AND,
        OR
    }
    public class ConditionOperation
    {
        OperConcat operConcat;
        string operObj;
        string oper;
        string operVal;

        public ConditionOperation(OperConcat operConcat, string operObj, string oper, string operVal)
        {
            this.operConcat = operConcat;
            this.operObj = operObj;
            this.oper = oper;
            this.operVal = operVal;
        }

        public string Result
        {
            get
            {
                return " " + operConcat.ToString() + " (" + operObj + " " + oper + " " + operVal + ") ";
            }
        }

        public override string ToString()
        {
            return this.Result;
        }
    }
}
