using CSharpCrawler.Interfaces;
using DAL;
using DAL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class CSCNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataManager>().To<DataManager>();
            Bind<IDownloader>().To<Downloader>();
            Bind<IRobotsTxtParser>().To<RobotsTxtParser>();
            Bind<ISitemapParser>().To<SitemapParser>();
            Bind<IHtmlParser>().To<HtmlParser>();
        }
    }
}
