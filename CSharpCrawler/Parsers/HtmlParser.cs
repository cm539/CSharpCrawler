using CSharpCrawler.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class HtmlParser : IHtmlParser
    {
        AngleSharp.Parser.Html.HtmlParser htmlParser = new AngleSharp.Parser.Html.HtmlParser();

        public IDictionary<int, int> GetRanks(string html, IEnumerable<Keyword> keywords)
        {
            if (string.IsNullOrEmpty(html) || keywords == null)
            {
                throw new ArgumentException("HtmlParser: GetRanks: Input data is null or empty");
            }

            var ranks = new Dictionary<int, int>();
            var document = htmlParser.Parse(html);
            var textContent = document.Body.TextContent.ToLowerInvariant();

            if (string.IsNullOrEmpty(textContent))
            {
                return ranks;
            }

            foreach (var keyword in keywords)
            {
                int count = Regex.Matches(textContent, keyword.Name.ToLowerInvariant()).Count;

                if (count == 0)
                {
                    continue;
                }

                if (ranks.ContainsKey(keyword.PersonId))
                {
                    ranks[keyword.PersonId] += count;
                }
                else
                {
                    ranks.Add(keyword.PersonId, count);
                }
            }

            return ranks;
        }

        public IEnumerable<string> GetLinks(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                throw new ArgumentException("HtmlParser: GetLinks: Input data is null or empty");
            }

            var links = new List<string>();
            var document = htmlParser.Parse(html);

            foreach (var elment in document.QuerySelectorAll("a"))
            {
                var url = elment.GetAttribute("href");

                if (string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                url = url.Trim();

                if (url != string.Empty || !url.StartsWith("#"))
                {
                    if (url.IndexOf('#') != -1)
                    {
                        url = url.Split('#')[0];
                    }

                    if (!links.Contains(url))
                    {
                        links.Add(url);
                    }
                }
            }

            return links;
        }
    }
}
