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
    class KeywordRepository : IKeywordRepository
    {
        public void Create(Keyword item)
        {
            if (item != null)
            {
                item.Id = MockContext.Keywords.Count;
                MockContext.Keywords.Add(item);
            }
        }

        public void Delete(Keyword item)
        {
            if (item != null && MockContext.Keywords.Contains(item))
            {
                MockContext.Keywords.Remove(item);
            }
        }

        public IEnumerable<Keyword> GetAll()
        {
            return MockContext.Keywords;
        }

        public Keyword GetById(int id)
        {
            return MockContext.Keywords.Find(x => x.Id == id);
        }

        public void Update(Keyword item)
        {
            if (item != null)
            {
                var keyword = GetById(item.Id);

                if (keyword != null)
                {
                    keyword.Name = item.Name;
                    keyword.PersonId = item.PersonId;
                }
            }
        }
    }
}
