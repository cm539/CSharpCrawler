using DAL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mock = DAL.Repositories.Mock;
using DB = DAL.Repositories.DB;

namespace DAL
{
    class DALNinjectModule : NinjectModule
    {
        bool testing;

        public DALNinjectModule(bool testing = false)
        {
            this.testing = testing;
        }

        public override void Load()
        {
            if (testing)
            {
                Bind<ISiteRepository>().To<Mock.SiteRepository>();
                Bind<IPageRepository>().To<Mock.PageRepository>();
                Bind<IKeywordRepository>().To<Mock.KeywordRepository>();
                Bind<IPersonPageRankRepository>().To<Mock.PersonPageRankRepository>();
                Bind<IPersonRepository>().To<Mock.PersonRepository>();
                Bind<IUserRepository>().To<Mock.UserRepository>();
            }
            else
            {
                Bind<ISiteRepository>().To<DB.SiteRepository>();
                Bind<IPageRepository>().To<DB.PageRepository>();
                Bind<IKeywordRepository>().To<DB.KeywordRepository>();
                Bind<IPersonPageRankRepository>().To<DB.PersonPageRankRepository>();
                Bind<IPersonRepository>().To<DB.PersonRepository>();
                Bind<IUserRepository>().To<DB.UserRepository>();
            }
        }
    }
}
