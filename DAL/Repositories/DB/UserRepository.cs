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
    class UserRepository : IUserRepository
    {
        private DBContext db;

        public UserRepository(DBContext context)
        {
            this.db = context;
        }
        
        public void Create(User item)
        {
            if (item != null)
            {
                db.Users.Add(item);
            }
        }

        public void Delete(User item)
        {
            if (item != null && GetById(item.Id) != null)
            {
                db.Users.Remove(item);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public void Update(User item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
