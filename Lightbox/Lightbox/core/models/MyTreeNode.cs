using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.core.models
{
    public class MyTreeNode : TreeNode
    {
        public enum NodeType
        {
            Table,
            ParentTrigger,
            Trigger,
            Field,
            Other
        }
        public NodeType Type = NodeType.Other;
        public MyTreeNode() : base()
        {
        }
    }
}
