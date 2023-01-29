using CookiesAuthApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public IEnumerable<User> FindByName(string name);
        public User GetByName(string name);
        public User GetByLogin(string login);
    }
}
