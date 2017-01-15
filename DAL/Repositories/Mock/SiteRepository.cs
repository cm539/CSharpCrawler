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
    class SiteRepository : ISiteRepository
    {
        public void Create(Site item)
        {
            if (item != null)
            {
                item.Id = MockContext.Sites.Count;
                MockContext.Sites.Add(item);
            }
        }

        public void Delete(Site item)
        {
            if (item != null && MockContext.Sites.Contains(item))
            {
                MockContext.Sites.Remove(item);
            }
        }

        public IEnumerable<Site> GetAll()
        {
            return MockContext.Sites;
        }

        public Site GetById(int id)
        {
            return MockContext.Sites.Find(x => x.Id == id);
        }

        public void Update(Site item)
        {
            if (item != null)
            {
                var site = GetById(item.Id);

                if (site != null)
                {
                    site.Name = item.Name;
                }
            }
        }
    }
}
