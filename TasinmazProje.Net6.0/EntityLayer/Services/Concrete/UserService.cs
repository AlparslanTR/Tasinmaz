using CoreLayer.Dtos;
using CoreLayer.Models;
using CoreLayer.Services.Abstract;
using EntityLayer.Data;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
           _context.Users.Add(user);
           await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var getId = await _context.Users.FindAsync(id);
            _context.Users.Remove(getId);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<Rol>> GetAllRoles()
        {
            return await _context.Rols.ToListAsync();
        }

        public async Task<User> GetUserProfile(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Login(LoginUserDto loginUserDto)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Mail == loginUserDto.Mail);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task Update(User user)
        {
            var getUser = await _context.Users.FindAsync(user.Id);
            getUser.Name = user.Name;
            getUser.LastName = user.LastName;
            getUser.Mail=user.Mail;
            getUser.RolId=user.RolId;
            getUser.PasswordHash = user.PasswordHash;
            getUser.PasswordSalt = user.PasswordSalt;
            await _context.SaveChangesAsync();
        }

        public async Task<User> UserNameValidate(User user)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Mail == user.Mail);
        }
        public async Task<bool> UserExists(string mail)
        {
            if (await _context.Users.AnyAsync(x => x.Mail == mail))
            {
                return true;
            }
            return false;
        }

        public async Task<Rol> GetRole(int id)
        {
            return await _context.Rols.FindAsync(id);
        }
    }
}
