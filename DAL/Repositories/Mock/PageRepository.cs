using DAL.Contexts;
using DAL.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Mock
{
    class PageRepository : IPageRepository
    {
        #region CRUD
        public void Create(Page item)
        {
            if (item != null)
            {
                item.Id = MockContext.Pages.Count;
                MockContext.Pages.Add(item);
            }
        }

        public void Delete(Page item)
        {
            if (item != null && MockContext.Pages.Contains(item))
            {
                MockContext.Pages.Remove(item);
            }
        }

        public IEnumerable<Page> GetAll()
        {
            return MockContext.Pages;
        }

        public Page GetById(int id)
        {
            return MockContext.Pages.Find(x => x.Id == id);
        }

        public void Update(Page item)
        {
            if (item != null)
            {
                var page = GetById(item.Id);

                if (page != null)
                {
                    page.Url = item.Url;
                    page.SiteId = item.SiteId;
                    page.FoundDateTime = item.FoundDateTime;
                    page.LastScanDate = item.LastScanDate;
                }
            }
        }
        #endregion

        public void AddRange(IEnumerable<Page> pages)
        {
            if (pages != null)
            {
                MockContext.Pages.AddRange(pages);
            }
        }

        public IEnumerable<Page> GetThousandNewPages()
        {
            return MockContext.Pages.Where(x => x.LastScanDate == null).Take(1000);
        }

        public IEnumerable<int> GetUniqueSiteIdList()
        {
            //db.Pages.Any();
            return MockContext.Pages.Select(x => x.SiteId).Distinct().ToList();
        }
    }
}
