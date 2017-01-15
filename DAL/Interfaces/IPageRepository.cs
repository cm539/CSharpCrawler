using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPageRepository : ICRUDRepository<Page>
    {
        void AddRange(IEnumerable<Page> pages);
        IEnumerable<Page> GetThousandNewPages();
        IEnumerable<int> GetUniqueSiteIdList();
    }
}
