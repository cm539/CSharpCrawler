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
    class PersonRepository : IPersonRepository
    {
        public void Create(Person item)
        {
            if (item != null)
            {
                item.Id = MockContext.Persons.Count;
                MockContext.Persons.Add(item);
            }
        }

        public void Delete(Person item)
        {
            if (item != null && MockContext.Persons.Contains(item))
            {
                MockContext.Persons.Remove(item);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return MockContext.Persons;
        }

        public Person GetById(int id)
        {
            return MockContext.Persons.Find(x => x.Id == id);
        }

        public void Update(Person item)
        {
            if (item != null)
            {
                var person = GetById(item.Id);

                if (person != null)
                {
                    person.Name = item.Name;
                }
            }
        }
    }
}
