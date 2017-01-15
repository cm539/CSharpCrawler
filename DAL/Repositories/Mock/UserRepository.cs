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
    class UserRepository : IUserRepository
    {
        public void Create(User item)
        {
            if (item != null)
            {
                item.Id = MockContext.Users.Count;
                MockContext.Users.Add(item);
            }
        }

        public void Delete(User item)
        {
            if (item != null && MockContext.Users.Contains(item))
            {
                MockContext.Users.Remove(item);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return MockContext.Users;
        }

        public User GetById(int id)
        {
            return MockContext.Users.Find(x => x.Id == id);
        }

        public void Update(User item)
        {
            if (item != null)
            {
                var user = GetById(item.Id);

                if (user != null)
                {
                    user.Username = item.Username;
                    user.Password = item.Password;
                    user.IsAdmin = item.IsAdmin;
                }
            }
        }
    }
}
