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
    class KeywordRepository : IKeywordRepository
    {
        private DBContext db;

        public KeywordRepository(DBContext context)
        {
            this.db = context;
        }
        
        public void Create(Keyword item)
        {
            if (item != null)
            {
                db.Keywords.Add(item);
            }
        }

        public void Delete(Keyword item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.Keywords.Remove(item);
            }
        }

        public IEnumerable<Keyword> GetAll()
        {
            return db.Keywords.ToList();
        }

        public Keyword GetById(int id)
        {
            return db.Keywords.Find(id);
        }

        public void Update(Keyword item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
