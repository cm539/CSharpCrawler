using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDataManager : IDisposable
    {
        bool DatabaseExists { get; }

        ISiteRepository Sites { get; }

        IPageRepository Pages { get; }

        IKeywordRepository Keywords { get; }

        IPersonPageRankRepository PersonPageRanks { get; }

        IPersonRepository Persons { get; }

        IUserRepository Users { get; }

        void Save();
    }
}
