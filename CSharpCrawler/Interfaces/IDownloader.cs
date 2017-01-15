using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler.Interfaces
{
    interface IDownloader
    {
        int StatusCode { get; }
        string UserAgent { get; set; }
        string Download(string url);
    }
}
