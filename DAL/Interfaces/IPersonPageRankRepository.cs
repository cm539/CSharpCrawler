using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPersonPageRankRepository : ICRUDRepository<PersonPageRank>
    {
        void AddRange(IEnumerable<PersonPageRank> personPageRanks);
    }
}
