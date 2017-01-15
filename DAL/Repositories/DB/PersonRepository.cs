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
    class PersonRepository : IPersonRepository
    {
        private DBContext db;

        public PersonRepository(DBContext context)
        {
            this.db = context;
        }

        public void Create(Person item)
        {
            if (item != null)
            {
                db.Persons.Add(item);
            }
        }

        public void Delete(Person item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.Persons.Remove(item);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return db.Persons.ToList();
        }

        public Person GetById(int id)
        {
            return db.Persons.Find(id);
        }

        public void Update(Person item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
