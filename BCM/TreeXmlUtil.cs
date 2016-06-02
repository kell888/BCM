using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BCM
{
    /// <summary> 
    /// TreeView,TreeNode和XmlElement的相互转换的类
    /// </summary>
    public static class TreeViewHelper 
    { 
        /// <summary> 
        /// 将TreeNode转换为XElement 
        /// </summary> 
        /// <param name="treeNode"></param> 
        /// <returns></returns> 
        public static XmlElement ToXElement(this TreeNode treeNode) 
        {
            return new XmlElement(treeNode.Text, treeNode.Nodes.ToXelement()); 
        }   

        /// <summary> 
        /// 将TreeNode的Nodes转换为XElement的集合 
        /// </summary> 
        /// <param name="nodes"></param> 
        /// <returns></returns> 
        public static IEnumerable<XmlElement> ToXelement(this TreeNodeCollection nodes) 
        { 
            return nodes.OfType<TreeNode>().Select(n => n.ToXElement()); 
        }   

        /// <summary> 
        /// 将XElement转换为TreeNode 
        /// </summary> 
        /// <param name="element"></param> 
        /// <returns></returns> 
        public static TreeNode ToTreeNode(this XmlElement element) 
        { 
            return new TreeNode(element.Name.ToString(), element.Elements().ToTreeNode().ToArray()); 
        }   

        /// <summary> 
        /// 将Element的子元素转换为TreeNode的集合 
        /// </summary> 
        /// <param name="elements"></param> 
        /// <returns></returns> 
        public static IEnumerable<TreeNode> ToTreeNode(this IEnumerable<XmlElement> elements) 
        { 
            return elements.Select(e => e.ToTreeNode()); 
        }   

        /// <summary> 
        /// 将TreeView转换为XDocument 
        /// </summary> 
        /// <param name="treeView"></param> 
        /// <returns></returns> 
        public static XmlDocument ToXml(this TreeView treeView) 
        {
            return new XmlDocument(
                new XmlDeclaration("1.0", "utf-8", "yes"),
                new XmlElement(treeView.Name, treeView.Nodes.ToXelement())); 
        } 
    } 
} 
//应用举例
//private void ToXmlButtonClick(object sender, EventArgs e) 
//{ 
//    treeView1.ToXml().Save("TreeView.XML"); 
//}   

//private void ToTreeViewButtonClick(object sender, EventArgs e) 
//{ 
//    var element = XmlElement.Load("TreeView.XML"); 
//    treeView1.Nodes.AddRange(element.Elements().ToTreeNode().ToArray()); 
//} 
//}
