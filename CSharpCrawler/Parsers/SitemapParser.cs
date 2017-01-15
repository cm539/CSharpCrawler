using CSharpCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CSharpCrawler
{
    class SitemapParser : ISitemapParser
    {
        public Sitemap Parse(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                throw new ArgumentException("SitemapParser: Parse: Input data is null or empty");
            }

            var sitemap = new Sitemap();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(rawData);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList locNodes;

            switch (root.LocalName)
            {
                case "urlset":
                    locNodes = root.SelectNodes("//*[local-name()='url']/*[local-name()='loc']");
                    break;
                case "sitemapindex":
                    locNodes = root.SelectNodes("//*[local-name()='sitemap']/*[local-name()='loc']");
                    break;
                default:
                    throw new Exception("Unknown root element: " + root.LocalName);
            }

            foreach (XmlNode node in locNodes)
            {
                sitemap.Urls.Add(node.InnerText.Trim());
            }

            return sitemap;
        }
    }
}
