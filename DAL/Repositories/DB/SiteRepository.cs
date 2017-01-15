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
    class SiteRepository : ISiteRepository
    {
        private DBContext db;

        public SiteRepository(DBContext context)
        {
            this.db = context;
        }

        #region CRUD
        public void Create(Site item)
        {
            if (item != null)
            {
                db.Sites.Add(item);
            }
        }

        public void Delete(Site item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.Sites.Remove(item);
            }
        }

        public IEnumerable<Site> GetAll()
        {
            return db.Sites.ToList();
        }

        public Site GetById(int id)
        {
            return db.Sites.Find(id);
        }

        public void Update(Site item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
        #endregion
    }
}
