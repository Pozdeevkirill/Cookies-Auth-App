using CookiesAuthApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Role GetByName(string name);
    }
}
