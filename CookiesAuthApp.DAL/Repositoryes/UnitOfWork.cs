using CookiesAuthApp.DAL.Data;
using CookiesAuthApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Repositoryes
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDbContext db;

        UserRepository userRepository;
        RoleRepository roleRepository;
        public UnitOfWork(AppDbContext _db)
        {
            db = _db;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new(db);
                return userRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if(roleRepository == null)
                    roleRepository = new(db);
                return roleRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
