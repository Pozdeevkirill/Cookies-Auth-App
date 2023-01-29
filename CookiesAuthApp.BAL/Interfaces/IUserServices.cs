using CookiesAuthApp.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.BAL.Interfaces
{
    public interface IUserServices
    {
        public UserDTO Create(UserDTO userDTO);
        public UserDTO Update(UserDTO userDTO);
        public IEnumerable<UserDTO> GetAll();
        public UserDTO Get(int id);
        public IEnumerable<UserDTO> FindByName(string name);
        public UserDTO GetByLogin(string login);
    }
}
