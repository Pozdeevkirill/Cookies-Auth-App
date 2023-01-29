using CookiesAuthApp.DAL.Data;
using CookiesAuthApp.DAL.Interfaces;
using CookiesAuthApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Repositoryes
{
    public class RoleRepository : IRoleRepository
    {
        AppDbContext db;
        public RoleRepository(AppDbContext _db)
        {
            db = _db;
        }

        public void Create(Role model)
        {
            if (model == null)
                return;

            db.Roles.Add(model);
        }

        public void Delete(int id)
        {
            if (id < 0)
                return;

            var role = db.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return;
            db.Roles.Remove(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles.Include(r => r.Users);
        }

        public Role GetById(int id)
        {
            if (id < 0)
                return null;

            return db.Roles
                .Include(r => r.Users)
                .FirstOrDefault(r => r.Id == id);
        }

        public Role GetByName(string name)
        {
            if (name == string.Empty)
                return null;

            return db.Roles
                .Include(r => r.Users)
                .FirstOrDefault(r => r.Name == name);
        }

        public Role Update(Role model)
        {
            if (model == null)
                return null ;

            db.Entry(model).State = EntityState.Modified;
            return model;
        }
    }
}
