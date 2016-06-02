using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public static class TreeViee2Menu
    {
        public static void TreeView2Menu(TreeNodeCollection nodes, ToolStripItemCollection items, EventHandler OnClick)
        {
            foreach (TreeNode node in nodes)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = node.Text;
                menu.Tag = node.Tag;
                items.Add(menu);
                if (node.Nodes.Count > 0)
                {
                    TreeView2Menu(node.Nodes, menu.DropDownItems, OnClick);
                }
                else
                {
                    menu.Click += OnClick;
                }
            }
        }
        public static void Menu2TreeView(ToolStripItemCollection items, TreeNodeCollection nodes)
        {
            foreach (ToolStripMenuItem item in items)
            {
                TreeNode node = new TreeNode();
                node.Text = item.Text;
                node.Tag = item.Tag;
                nodes.Add(node);
                if (item.DropDownItems.Count > 0)
                {
                    Menu2TreeView(item.DropDownItems, node.Nodes);
                }
            }
        }
    }
}
