using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    static class MockContext
    {
        public static List<Keyword> Keywords = new List<Keyword>();
        public static List<Page> Pages = new List<Page>();
        public static List<Person> Persons = new List<Person>();
        public static List<PersonPageRank> PersonPageRanks = new List<PersonPageRank>();
        public static List<Site> Sites = new List<Site>();
        public static List<User> Users = new List<User>();

        static MockContext()
        {
            #region Таблица Sites
            Sites.Add(new Site() { Id = 0, Name = "lenta.ru" });
            #endregion

            #region Таблица Pages
            //Pages.Add(new Page()
            //{
            //    Id = 0,
            //    Url = "http://lenta.ru/robots.txt",
            //    SiteId = 0,
            //    FoundDateTime = new DateTime(2016, 10, 01, 12, 41, 34),
            //    LastScanDate = DateTime.Today
            //});
            #endregion

            #region Таблица Persons
            Persons.Add(new Person() { Id = 0, Name = "Путин" });
            Persons.Add(new Person() { Id = 1, Name = "Медведев" });
            #endregion

            #region Таблица Keywords
            Keywords.Add(new Keyword() { Id = 0, Name = "Путин", PersonId = 0 });
            Keywords.Add(new Keyword() { Id = 1, Name = "Путину", PersonId = 0 });
            Keywords.Add(new Keyword() { Id = 2, Name = "Путиным", PersonId = 0 });
            Keywords.Add(new Keyword() { Id = 3, Name = "Путина", PersonId = 0 });
            Keywords.Add(new Keyword() { Id = 4, Name = "Медведев", PersonId = 1 });
            Keywords.Add(new Keyword() { Id = 5, Name = "Медведеву", PersonId = 1 });
            Keywords.Add(new Keyword() { Id = 6, Name = "Медведевым", PersonId = 1 });
            Keywords.Add(new Keyword() { Id = 7, Name = "Медведева", PersonId = 1 });
            #endregion

            #region Таблица PersonPageRanks
            //PersonPageRanks.Add(new PersonPageRank() { PersonId = 0, PageId = 0, Rank = 3 });
            //PersonPageRanks.Add(new PersonPageRank() { PersonId = 1, PageId = 0, Rank = 2 });
            #endregion
        }
    }
}
