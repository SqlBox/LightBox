using Lightbox.usercontrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.core
{
    public class TabViewContextMenuBuilder
    {
        private TabView tabview;
        public TabViewContextMenuBuilder(TabView tv)
        {
            this.tabview = tv;
        }

        internal MenuItem[] BuildeTableTableMenuItems()
        {
            return TableTableBuilder.Build(tabview);
        }

        internal MenuItem[] BuildTableTriggerMenuItems()
        {
            return TableTriggersBuilder.Build(tabview);
        }

        public class TableTableBuilder
        {
            public static MenuItem[] Build(TabView _this)
            {
                var items = new List<MenuItem>();
                foreach (MenuItem menuItem in TableTableMenuItemGenerator(_this))
                {
                    items.Add(menuItem);
                }
                return items.ToArray();
            }

            private static IEnumerable<MenuItem> TableTableMenuItemGenerator(TabView _this)
            {
                yield return new MenuItem("Show Data", _this.TreeViewTable_MenuItem_ShowData);
                yield return new MenuItem("Send Create Statement", _this.TreeViewTable_MenuItem_ShowCreate);
                yield return new MenuItem("Send Create Statement + Triggers", _this.TreeViewTable_Menuitem_ShowCreateWithTrigger);
                yield return new MenuItem("-");
                yield return new MenuItem("Inspect Table", _this.TreeViewTable_MenuItem_Inspect);
                yield return new MenuItem("-");
                yield return new MenuItem("Drop Table", _this.TreeViewTable_MenuItem_DropTable);
                yield return new MenuItem("Truncate Table", _this.TreeViewTable_MenuItem_TruncateTable);
                yield return new MenuItem("-");
                yield return new MenuItem("Refresh", _this.TreeViewTable_MenuItem_RefreshTable);
            }
        }

        public class TableTriggersBuilder
        {
            public static MenuItem[] Build(TabView _this)
            {
                var items = new List<MenuItem>();
                foreach (MenuItem menuItem in TableTriggerGenerator(_this))
                {
                    items.Add(menuItem);
                }
                return items.ToArray();
            }
            private static IEnumerable<MenuItem> TableTriggerGenerator(TabView _this)
            {
                yield return new MenuItem("Send Create Statement", _this.TreeViewTableTriggers_MenuItem_ShowCreate);
                yield return new MenuItem("-");
                yield return new MenuItem("Drop Trigger", _this.TreeViewTableTriggers_MenuItem_DropTrigger);
            }
        }
    }
}
