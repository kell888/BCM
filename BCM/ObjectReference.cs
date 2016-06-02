using System;
using System.Collections.Generic;
using System.Text;

namespace BCM
{
    public class ObjectReference
    {
        object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
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
        bool isReference;

        public bool IsReference
        {
            get { return isReference; }
        }
        ObjectClass refObject;

        public ObjectClass RefObject
        {
            get { return refObject; }
            set { refObject = value; }
        }

        public ObjectReference(object value, bool isNumeric, bool nullable)
        {
            this.value = value;
            this.isNumeric = isNumeric;
            this.nullable = nullable;
        }

        public ObjectReference(object value, bool isNumeric, bool nullable, ObjectClass refObject)
        {
            this.value = value;
            this.isNumeric = isNumeric;
            this.nullable = nullable;
            this.refObject = refObject;
            this.isReference = true;
        }

        public override string ToString()
        {
            if (isReference)
                return value + ":" + (nullable ? "Nullable" : "NotNullable") + refObject;
            else
                return value + ":" + (nullable ? "Nullable" : "NotNullable");
        }
    }
}
