using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Add(User user);
        User Get(int id);
        void Remove(int id);
        User Update(User user);
        User GetFromEmail(string email);
        List<User> GetAll();
    }
}
