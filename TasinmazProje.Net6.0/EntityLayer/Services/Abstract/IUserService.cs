using CoreLayer.Dtos;
using CoreLayer.Models;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<User> GetUserProfile(int id);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<Rol>> GetAllRoles();
        Task Add(User user);
        Task<User> Login(LoginUserDto loginUserDto);
        Task<User> UserNameValidate(User user);
        Task Delete(int id);
        Task Update(User user);
        Task<bool> UserExists(string userName);
        Task<User> Register(User user, string password);
        Task<Rol> GetRole(int id);
    }
}
