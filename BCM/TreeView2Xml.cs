using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace BCM
{
    public static class TreeView2Xml
    {
        public static void ToXml(TreeView tree, string filename)
        {
            if (tree.Nodes.Count > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Tree></Tree>");
                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);
                TreeNode2Xml(tree.Nodes, root);
                doc.Save(filename);
            }
            else
            {
                MessageBox.Show("当对象树为空！不保存文件。");
            }
        }
        private static void TreeNode2Xml(TreeNodeCollection treeNodes, XmlNode xmlNode)
        {
            XmlDocument doc = xmlNode.OwnerDocument;
            foreach (TreeNode treeNode in treeNodes)
            {
                XmlNode element = doc.CreateNode("element", "Item", "");
                XmlAttribute attr = doc.CreateAttribute("Name");
                attr.Value = treeNode.Name;
                element.Attributes.Append(attr);
                XmlAttribute attr1 = doc.CreateAttribute("Text");
                attr1.Value = treeNode.Text;
                element.Attributes.Append(attr1);
                if (treeNode.Tag != null)
                    element.AppendChild(doc.CreateCDataSection(Extensions.ConvertToString(treeNode.Tag as Dictionary<string, ObjectReference>)));
                xmlNode.AppendChild(element);

                if (treeNode.Nodes.Count > 0)
                {
                    TreeNode2Xml(treeNode.Nodes, element);
                }
            }
        }
    }
    public static class Xml2TreeView
    {
        public static void ToTreeView(TreeView tree, string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            XmlNodeList xmlNodes = xmlDoc.DocumentElement.ChildNodes;
            tree.BeginUpdate();
            tree.Nodes.Clear();
            XmlNode2TreeNode(xmlNodes, tree.Nodes);
            tree.EndUpdate();
            tree.Nodes[0].Expand();
        }
        private static void XmlNode2TreeNode(XmlNodeList xmlNode, TreeNodeCollection treeNode)
        {
            foreach (XmlNode var in xmlNode)
            {
                if (var.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Name = var.Attributes["Name"].Value;
                newTreeNode.Text = var.Attributes["Text"].Value;
                if (var.HasChildNodes)
                {
                    if (var.ChildNodes[0].NodeType == XmlNodeType.CDATA)
                    {
                        newTreeNode.Tag = Extensions.ConvertToDictionary(((XmlCDataSection)var.ChildNodes[0]).Data);
                    }
                    XmlNode2TreeNode(var.ChildNodes, newTreeNode.Nodes);
                }
                treeNode.Add(newTreeNode);
            }
        }
    }
}
