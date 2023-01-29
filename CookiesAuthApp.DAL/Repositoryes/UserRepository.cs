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
    public class UserRepository : IUserRepository
    {
        AppDbContext db;
        public UserRepository(AppDbContext _db)
        {
            db = _db;
        }

        public void Create(User model)
        {
            if (model == null)
                return;
            db.Users.Add(model);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            if (id < 0)
                return;

            var user = db.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
                return;
            
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public IEnumerable<User> FindByName(string name)
        {
            if (name == string.Empty)
                return null;

            var users = db.Users
                .Include(u => u.Role)
                .Where(u => u.Name == name);
            return users;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users
                .Include(u => u.Role);
        }

        public User GetById(int id)
        {
            if (id < 0)
                return null;

            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByLogin(string login)
        {
            if (login == string.Empty)
                return null;

            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login);
        }

        public User GetByName(string name)
        {
            if (name == string.Empty)
                return null;

            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Name == name);
        }
        public User Update(User model)
        {
            if (model == null)
                return null;

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return model;
        }
    }
}
