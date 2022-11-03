using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Abstract
{
    public interface IUserService
    {
        Task<User> Get(int id);
        Task<User> GetUserProfile(int id);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<Rol>> GetAllRoles();
        Task Add(User user);
        Task<User> Login(LoginUserDto loginUserDto);
        Task<User> UserNameValidate(User user);
        Task Delete(int id);
        Task Update(User user);
    }
}
