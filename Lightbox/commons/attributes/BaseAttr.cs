using System;
using System.Collections.Generic;
using System.Text;

namespace sqlbox.commons.attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class BaseAttr : Attribute
    {
        public string Label { get; }

        public BaseAttr() { }

        public BaseAttr(string label) : this()
        {
            Label = label;
        }
    }
}
