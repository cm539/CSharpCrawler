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
    class PersonPageRankRepository : IPersonPageRankRepository
    {
        private DBContext db;

        public PersonPageRankRepository(DBContext context)
        {
            this.db = context;
        }

        #region CRUD
        public void Create(PersonPageRank item)
        {
            if (item != null)
            {
                db.PersonPageRanks.Add(item);
            }
        }

        public void Delete(PersonPageRank item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.PersonPageRanks.Remove(item);
            }
        }

        public IEnumerable<PersonPageRank> GetAll()
        {
            return db.PersonPageRanks.ToList();
        }

        public PersonPageRank GetById(int id)
        {
            return db.PersonPageRanks.Find(id);
        }

        public void Update(PersonPageRank item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
        #endregion

        public void AddRange(IEnumerable<PersonPageRank> personPageRanks)
        {
            if (personPageRanks != null)
            {
                db.PersonPageRanks.AddRange(personPageRanks);
            }
        }
    }
}
