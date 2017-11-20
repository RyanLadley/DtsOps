using dtso.data.Context;
using dtso.data.Entities;
using dtso.data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dtso.data.Repositories
{
    public class UserRepository : IUserRepository
    {
        MainContext _context;

        public UserRepository(MainContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Get(int id)
        {
            return _context.Users.AsNoTracking()
                .FirstOrDefault(user => user.UserId == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User GetFromEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
