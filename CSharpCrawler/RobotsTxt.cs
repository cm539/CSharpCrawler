using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class RobotsTxt
    {
        public bool HasDisallowedPaths
        {
            get
            {
                return DisallowedPaths.Count > 0;
            }
        }

        public bool HasSitemaps
        {
            get
            {
                return Sitemaps.Count > 0;
            }
        }

        // Задержка в мс
        public long CrawlDelay { get; set; }
        public List<string> DisallowedPaths { get; }
        public List<string> AllowedPaths { get; }
        public List<string> Sitemaps { get; }
        public List<string> UnknownLines { get;  }
        public List<string> UnknownDirectives { get; }

        public RobotsTxt()
        {
            CrawlDelay = 0;
            DisallowedPaths = new List<string>();
            AllowedPaths = new List<string>();
            Sitemaps = new List<string>();
            UnknownLines = new List<string>();
            UnknownDirectives = new List<string>();
        }
    }
}
