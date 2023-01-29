using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        public void Create(T model);
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public T Update(T model);
        public void Delete(int id);
    }
}
