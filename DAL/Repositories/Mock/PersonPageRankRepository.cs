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
    class PersonPageRankRepository : IPersonPageRankRepository
    {
        #region CRUD
        public void Create(PersonPageRank item)
        {
            if (item != null)
            {
                item.Id = MockContext.PersonPageRanks.Count;
                MockContext.PersonPageRanks.Add(item);
            }
        }

        public void Delete(PersonPageRank item)
        {
            if (item != null && MockContext.PersonPageRanks.Contains(item))
            {
                MockContext.PersonPageRanks.Remove(item);
            }
        }

        public IEnumerable<PersonPageRank> GetAll()
        {
            return MockContext.PersonPageRanks;
        }

        public PersonPageRank GetById(int id)
        {
            return MockContext.PersonPageRanks.Find(x => x.Id == id);
        }

        public void Update(PersonPageRank item)
        {
            if (item != null)
            {
                var personPageRank = GetById(item.Id);

                if (personPageRank != null)
                {
                    personPageRank.Rank = item.Rank;
                    personPageRank.PersonId = item.PersonId;
                    personPageRank.PageId = item.PageId;
                }
            }
        }
        #endregion

        public void AddRange(IEnumerable<PersonPageRank> personPageRanks)
        {
            if (personPageRanks != null)
            {
                MockContext.PersonPageRanks.AddRange(personPageRanks);
            }
        }
    }
}
