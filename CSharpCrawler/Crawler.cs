using CSharpCrawler.Interfaces;
using DAL;
using DAL.Interfaces;
using Domain.Entites;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCrawler
{
    class Crawler
    {
        private IKernel kernel;
        private IDownloader downloader;
        private IRobotsTxtParser robotsParser;
        private ISitemapParser sitemapParser;
        private IHtmlParser htmlParser;

        public Crawler()
        {
            kernel = new StandardKernel(new CSCNinjectModule());
            downloader = kernel.Get<IDownloader>();
            robotsParser = kernel.Get<IRobotsTxtParser>();
            sitemapParser = kernel.Get<ISitemapParser>();
            htmlParser = kernel.Get<IHtmlParser>();

            #region testing
            //DataManager.testing = true;
            using (var dataManager = kernel.Get<IDataManager>())
            {
                dataManager.Sites.Create(new Site() { Name = "lenta.ru" });
                dataManager.Persons.Create(new Person() { Name = "путин" });
                dataManager.Keywords.Create(new Keyword() { PersonId = 1, Name = "путин" });
                dataManager.Save();
            }
            #endregion
        }

        public void CrawlNewPages()
        {
            CheckDatabase();
            AddNewRobotsTxtPagesToDatabase(GetNewSites());

            IEnumerable<Page> newPages;
            IEnumerable<Keyword> keywords;

            using (var dataManager = kernel.Get<IDataManager>())
            {
                newPages = dataManager.Pages.GetThousandNewPages().ToList();
                keywords = dataManager.Keywords.GetAll().ToList();
            }

            while (newPages != null && newPages.Count() > 0)
            {
                foreach (var page in newPages)
                {
                    PageHandler(page, keywords);
                }

                using (var dataManager = kernel.Get<IDataManager>())
                {
                    newPages = dataManager.Pages.GetThousandNewPages().ToList();
                }
            }
        }

        // TODO: Реализовать метод
        public void CrawlOldPages()
        {
            throw new NotImplementedException();
        }

        public void CrawlAllPages()
        {
            CrawlNewPages();
            CrawlOldPages();
        }

        private void CheckDatabase()
        {
            using (var dataManager = kernel.Get<IDataManager>())
            {
                if (!dataManager.DatabaseExists)
                {
                    throw new Exception("ERROR: Database is not exists!");
                }
            }
        }

        private IEnumerable<Site> GetNewSites()
        {
            var newSites = new List<Site>();
            IEnumerable<Site> allSites;
            IEnumerable<int> uniqueSiteIdList;

            using (var dataManager = kernel.Get<IDataManager>())
            {
                allSites = dataManager.Sites.GetAll().ToList();
                uniqueSiteIdList = dataManager.Pages.GetUniqueSiteIdList().ToList();
            }

            if (allSites != null && uniqueSiteIdList != null)
            {
                foreach (var site in allSites)
                {
                    if (!uniqueSiteIdList.Contains(site.Id))
                    {
                        newSites.Add(site);
                    }
                }
            }

            return newSites;
        }

        private void AddNewRobotsTxtPagesToDatabase(IEnumerable<Site> newSites)
        {
            if (newSites != null)
            {
                var newRobotsTxtPages = new List<Page>();

                foreach (var site in newSites)
                {
                    newRobotsTxtPages.Add(CreateRobotsTxtPage(site));
                }

                if (newRobotsTxtPages.Count > 0)
                {
                    using (var dataManager = kernel.Get<IDataManager>())
                    {
                        dataManager.Pages.AddRange(newRobotsTxtPages);
                        dataManager.Save();
                    }
                }
            }
        }

        private void PageHandler(Page page, IEnumerable<Keyword> keywords)
        {
            string data = downloader.Download(page.Url);

            if (string.IsNullOrEmpty(data))
            {
                switch (downloader.StatusCode)
                {
                    case 200:
                        UpdatePage(page);
                        break;
                    case 404:
                        DeletePage(page);
                        break;
                }

                return;
            }

            if (IsRobotsTxt(page.Url))
            {
                RobotsTxtHandler(data, page.SiteId);
            }
            else if (IsSitemap(page.Url))
            {
                SitemapHandler(data, page.SiteId);
            }
            else
            {
                HtmlHandler(data, page.Id, keywords);
            }

            UpdatePage(page);
        }

        private void RobotsTxtHandler(string data, int siteId)
        {
            RobotsTxt robot = robotsParser.Parse(data);

            if (robot.HasSitemaps)
            {
                var sitemaps = new List<Page>();

                foreach (var sitemap in robot.Sitemaps)
                {
                    sitemaps.Add(CreatePage(sitemap, siteId));
                }                

                using (var dataManager = kernel.Get<IDataManager>())
                {
                    dataManager.Pages.AddRange(sitemaps);
                    dataManager.Save();
                }
            }
        }

        private void SitemapHandler(string data, int siteId)
        {
            Sitemap sitemap = sitemapParser.Parse(data);
            var urls = new List<Page>();

            if (sitemap.Urls.Count > 0)
            {
                foreach (var url in sitemap.Urls)
                {
                    urls.Add(CreatePage(url, siteId));
                }

                using (var dataManager = kernel.Get<IDataManager>())
                {
                    dataManager.Pages.AddRange(urls);
                    dataManager.Save();
                }
            }
        }

        private void HtmlHandler(string data, int pageId, IEnumerable<Keyword> keywords)
        {
            if (keywords == null || keywords.Count() == 0)
            {
                return;
            }
            
            IDictionary<int, int> ranks = htmlParser.GetRanks(data, keywords);

            if (ranks.Count > 0)
            {
                var personPageRanks = new List<PersonPageRank>();

                foreach (var rank in ranks)
                {
                    personPageRanks.Add(CreatePersonPageRank(rank.Key, pageId, rank.Value));
                }

                using (var dataManager = kernel.Get<IDataManager>())
                {
                    dataManager.PersonPageRanks.AddRange(personPageRanks);
                    dataManager.Save();
                }
            }
        }

        private bool IsRobotsTxt(string url)
        {
            return url.Trim().Contains("robots.txt");
        }

        private bool IsSitemap(string url)
        {
            return url.Trim().Contains("sitemap") && (url.Trim().Contains(".xml") || url.Trim().Contains(".gz"));
        }

        #region Data helpers
        private Page CreateRobotsTxtPage(Site site)
        {
            return new Page()
            {
                Url = "https://" + site.Name + "/robots.txt",
                SiteId = site.Id,
                FoundDateTime = DateTime.Now,
                LastScanDate = null
            };
        }

        private Page CreatePage(string url, int siteId)
        {
            return new Page()
            {
                Url = url,
                SiteId = siteId,
                FoundDateTime = DateTime.Now,
                LastScanDate = null
            };
        }

        private void DeletePage(Page page)
        {
            if (page != null)
            {
                using (var dataManager = kernel.Get<IDataManager>())
                {
                    dataManager.Pages.Delete(page);
                    dataManager.Save();
                }
            }
        }

        private void UpdatePage(Page page)
        {
            if (page != null)
            {
                page.LastScanDate = DateTime.Now;

                using (var dataManager = kernel.Get<IDataManager>())
                {
                    dataManager.Pages.Update(page);
                    dataManager.Save();
                }
            }
        }

        private PersonPageRank CreatePersonPageRank(int personId, int pageId, int rank)
        {
            return new PersonPageRank()
            {
                PersonId = personId,
                PageId = pageId,
                Rank = rank
            };
        }
        #endregion
    }
}
