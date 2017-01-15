using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class Sitemap
    {
        public List<string> Urls { get; }

        public Sitemap()
        {
            Urls = new List<string>();
        }
    }
}
