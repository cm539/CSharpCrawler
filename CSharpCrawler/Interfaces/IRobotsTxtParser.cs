using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler.Interfaces
{
    interface IRobotsTxtParser
    {
        RobotsTxt Parse(string rawData);
    }
}
