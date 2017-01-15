using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler.Interfaces
{
    interface IHtmlParser
    {
        IDictionary<int, int> GetRanks(string html, IEnumerable<Keyword> keywords);
        IEnumerable<string> GetLinks(string html);
    }
}
