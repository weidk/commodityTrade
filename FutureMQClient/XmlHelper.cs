using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FutureMQClient
{
    public static class XmlHelper
    {
        /// <summary>
        /// 读取xml
        /// </summary>
        /// <param name="docName"></param>
        /// <returns></returns>
        public static XmlDocument ReadXml(string docName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(docName);
            return doc;
        }

        /// <summary>
        /// 获取指定节点及内容
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static List<string> GetNode(XmlDocument doc, string nodeName)
        {
            List<string> patternList = new List<string>();
            XmlNodeList xnl = doc.GetElementsByTagName(nodeName);
            foreach (XmlNode node in xnl)
            {
                patternList.Add(node.InnerText);
            }
            return patternList;
        }

        public static string GetInnerText(XmlDocument doc, string nodeName)
        {
            XmlNodeList xnl = doc.GetElementsByTagName(nodeName);
            return xnl[0].InnerText;
        }
    }
}
