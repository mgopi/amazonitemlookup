using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NKCraddock.AmazonItemLookup.Client
{
    public class AwsXmlParser
    {
        const string NAMESPACE_ALIAS = "aws";
        XmlDocument doc;
        XmlNamespaceManager namespaceManager;
        
        public AwsXmlParser(string xml)
        {
            doc = new XmlDocument();
            doc.LoadXml(xml);

            namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace(NAMESPACE_ALIAS, doc.DocumentElement.NamespaceURI);
        }

        public string SelectNodeValue(string path)
        {
            return SelectNodeValue(doc, path);
        }
        public string SelectNodeValue(XmlNode node, string path)
        {
            XmlNode selectedNode = SelectNode(node, path);
            if (selectedNode == null)
                return null;
            return selectedNode.Value ?? selectedNode.InnerText;
        }

        public XmlNodeList SelectNodes(string path)
        {
            return SelectNodes(doc, path);
        }

        public XmlNodeList SelectNodes(XmlNode node, string path)
        {
            string xpath = BuildXPath(path);
            return node.SelectNodes(xpath, namespaceManager);
        }

        public XmlNode SelectNode(string path)
        {
            return SelectNode(doc, path);
        }

        public XmlNode SelectNode(XmlNode node, string path)
        {
            string xpath = BuildXPath(path);
            return node.SelectSingleNode(xpath, namespaceManager);
        }

        private string BuildXPath(string path)
        {
            string[] elementNames = path.Split('/');
            var sb = new StringBuilder();
            foreach (string elementName in elementNames.Where(x=> String.IsNullOrEmpty(x) == false))
                sb.AppendFormat("/{0}:{1}", NAMESPACE_ALIAS, elementName);
            
            if (!path.StartsWith("/"))
                sb.Remove(0, 1);
            return sb.ToString();
        }
    }
}