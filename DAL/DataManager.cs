using DAL.Contexts;
using DAL.Interfaces;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataManager : IDataManager
    {
        public static bool testing = false;

        private DBContext context;
        private IKernel kernel;
        private ISiteRepository siteRepo;
        private IPageRepository pageRepo;
        private IKeywordRepository keywordRepo;
        private IPersonPageRankRepository personPageRankRepo;
        private IPersonRepository personRepo;
        private IUserRepository userRepo;
        private bool disposed = false;

        public DataManager()
        {
            kernel = new StandardKernel(new DALNinjectModule(testing));

            if (!testing)
            {
                context = new DBContext();
                context.Database.CreateIfNotExists();
            }
        }

        public bool DatabaseExists
        {
            get
            {
                return testing ? true
                               : context.Database.Exists();
            }
        }

        public ISiteRepository Sites
        {
            get
            {
                if (siteRepo == null)
                {
                    siteRepo = testing ? kernel.Get<ISiteRepository>()
                                       : kernel.Get<ISiteRepository>(new ConstructorArgument("context", context));
                }

                return siteRepo;
            }
        }

        public IPageRepository Pages
        {
            get
            {
                if (pageRepo == null)
                {
                    pageRepo = testing ? kernel.Get<IPageRepository>()
                                       : kernel.Get<IPageRepository>(new ConstructorArgument("context", context));
                }

                return pageRepo;
            }
        }

        public IKeywordRepository Keywords
        {
            get
            {
                if (keywordRepo == null)
                {
                    keywordRepo = testing ? kernel.Get<IKeywordRepository>()
                                          : kernel.Get<IKeywordRepository>(new ConstructorArgument("context", context));
                }

                return keywordRepo;
            }
        }

        public IPersonPageRankRepository PersonPageRanks
        {
            get
            {
                if (personPageRankRepo == null)
                {
                    personPageRankRepo = testing ? kernel.Get<IPersonPageRankRepository>()
                                                 : kernel.Get<IPersonPageRankRepository>(new ConstructorArgument("context", context));
                }

                return personPageRankRepo;
            }
        }

        public IPersonRepository Persons
        {
            get
            {
                if (personRepo == null)
                {
                    personRepo = testing ? kernel.Get<IPersonRepository>()
                                         : kernel.Get<IPersonRepository>(new ConstructorArgument("context", context));
                }

                return personRepo;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = testing ? kernel.Get<IUserRepository>()
                                       : kernel.Get<IUserRepository>(new ConstructorArgument("context", context));
                }

                return userRepo;
            }
        }

        public void Save()
        {
            if (testing)
            {
                return;
            }

            bool success = false;

            do
            {
                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var item in ex.EntityValidationErrors)
                    {
                        RollbackChanges(item.Entry);
                    }

                    Console.WriteLine(ex.ToString());
                }
                catch (DbUpdateException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        RollbackChanges(entry);
                    }

                    Console.WriteLine(ex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            } while (!success);
        }

        private void RollbackChanges(DbEntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            if (!testing)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
