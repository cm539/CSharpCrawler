using DAL.Contexts;
using DAL.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DB
{
    class PageRepository : IPageRepository
    {
        private DBContext db;

        public PageRepository(DBContext context)
        {
            this.db = context;
        }

        #region CRUD
        public void Create(Page item)
        {
            if (item != null)
            {
                db.Pages.Add(item);
            }
        }

        public void Delete(Page item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.Pages.Remove(item);
            }
        }

        public IEnumerable<Page> GetAll()
        {
            return db.Pages.ToList();
        }

        public Page GetById(int id)
        {
            return db.Pages.Find(id);
        }

        public void Update(Page item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
        #endregion
        
        public void AddRange(IEnumerable<Page> pages)
        {
            if (pages != null)
            {
                db.Pages.AddRange(pages);
            }
        }

        public IEnumerable<Page> GetThousandNewPages()
        {
            return db.Pages.Where(x => x.LastScanDate == null).Take(1000);
        }

        public IEnumerable<int> GetUniqueSiteIdList()
        {
            //db.Pages.Any();
            return db.Pages.Select(x => x.SiteId).Distinct().ToList();
        }
    }
}
