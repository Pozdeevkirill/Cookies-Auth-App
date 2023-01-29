using CookiesAuthApp.BAL.Interfaces;
using CookiesAuthApp.BAL.ModelsDTO;
using CookiesAuthApp.DAL.Interfaces;
using CookiesAuthApp.DAL.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAuthApp.BAL.Services
{
    public class UserServices : IUserServices
    {
        IUnitOfWork db;

        public UserServices(IUnitOfWork _db)
        {
            db = _db;
        }

        public UserDTO Create(UserDTO userDTO)
        {
            if (userDTO == null)
                return null;

            User user = new()
            {
                Name = userDTO.Name,
                Login = userDTO.Login,
                Password = userDTO.Password,
            };

           user.Role = db.RoleRepository.GetByName(userDTO.Role);

            db.UserRepository.Create(user);
            db.SaveAsync();
            return userDTO;
        }

        public IEnumerable<UserDTO> FindByName(string name)
        {
            if (name == string.Empty)
                return null;

            var userList = db.UserRepository.FindByName(name);

            List<UserDTO> usersDTO = new();

            foreach (var user in userList)
            {
                UserDTO userDTO = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Role = user.Role.Name,
                };
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }

        public UserDTO Get(int id)
        {
            if (id < 0)
                return null;

            var user = db.UserRepository.GetById(id);
            if (user == null)
                return null;

            return new()
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role.Name
            };
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var userList = db.UserRepository.GetAll();

            List<UserDTO> usersDTO = new();

            foreach (var user in userList)
            {
                UserDTO userDTO = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Role = user.Role.Name,
                };
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }

        public UserDTO GetByLogin(string login)
        {
            if (login == string.Empty)
                return null;

            var user = db.UserRepository.GetByLogin(login);
            if(user == null)
                return null;

            return new()
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role.Name
            };
        }

        public UserDTO Update(UserDTO userDTO)
        {
            if (userDTO == null)
                return null;

            User user = new()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Login = userDTO.Login,
                Password = userDTO.Password,
            };

            var role = db.RoleRepository.GetByName(userDTO.Role);
            user.Role = role;

            db.UserRepository.Update(user);
            db.SaveAsync();
            return userDTO;
        }
    }
}
