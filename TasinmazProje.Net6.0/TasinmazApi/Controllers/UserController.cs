using System.Net;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using EntityLayer.Data;
using CoreLayer.Services.Abstract;
using CoreLayer.Dtos;
using CoreLayer.Security;
using EntityLayer.Models;
using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using CoreLayer.Services.Concrete;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace TasinmazApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IUserService userService, ILogService logService, IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _logService = logService;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            if (await _userService.UserExists(createUserDto.Mail))
            {
                ModelState.AddModelError("Mail", "Bu mail zaten sisteme kayıtlı");
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userCreate = new User
            {
                Name = createUserDto.Name,
                LastName=createUserDto.LastName,
                Mail=createUserDto.Mail,
                Password=createUserDto.Password,
                RolId=createUserDto.RolId
            };

            var createdUser = await _userService.Register(userCreate, createUserDto.Password);
            await _logService.Add(
                     new Log
                     {
                         Durum = "Başarılı",
                         UserId=userCreate.Id,
                         Aciklama = "Kullanıcı Ekleme Başarılı bir şekilde gerçekleşti",
                         IslemTipi = "Kullanıcı Ekleme",
                         DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                         UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                     }
                 );
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            var getAll = await _userService.GetAll();
            return Ok(getAll);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Login( LoginUserDto loginUserDto)
        {       
            var user = await _userService.Login(loginUserDto);
            if (user == null)
            {
                return Unauthorized("Yetkisiz İşlem");
            }
            else
            {
                
                await _logService.Add(

                     new Log
                     {
                         Durum = "Başarılı",
                         UserId = user.Id,
                         Aciklama = "Kullanıcı Giriş Yaptı",
                         IslemTipi = "Kullanıcı Giriş",
                         DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                         UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                     }
                 );
                var role = await _userService.GetRole(user.RolId);
                IdentityOptions _options = new IdentityOptions();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserName",user.Name.ToString()),
                    new Claim("UserMail",user.Mail.ToString()),
                    new Claim(_options.ClaimsIdentity.RoleClaimType,user.Rol.RolName.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(tokenString);
            }

        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>>GetUser(int id)
        {

                var user = await _userService.GetById(id);
                if (user == null)
                return NotFound("Girinlen Bilgilerde Kullanıcı Yoktur.!");
                return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            await _logService.Add(
                        new Log
                        {
                            Durum = "Başarılı",
                            UserId = userId,
                            Aciklama = "Kullanıcı Silindi",
                            IslemTipi = "Kullanıcı Silme",
                            DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                            UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        }
                    );
                await _userService.Delete(id);
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult>Update(int id, UpdateUserDto updateUserDto)
        {
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updateUserDto.Password, out passwordHash, out passwordSalt);
            User user = new()
                {
                    Id = id,
                    RolId = updateUserDto.RolId,
                    Name = updateUserDto.Name,
                    LastName = updateUserDto.LastName,
                    Mail = updateUserDto.Mail,
                    PasswordHash= passwordHash,
                    PasswordSalt=passwordSalt
                };

                await _logService.Add(
                     new Log
                     {
                         Durum = "Başarılı",
                         UserId=userId,
                         Aciklama = "Kullanıcı Güncellendi",
                         IslemTipi = "Kullanıcı Güncelleme",
                         DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                         UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                     }
                 );
                await _userService.Update(user);
                return Ok();
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var getRoles =await _userService.GetAllRoles();
            return Ok(getRoles);
        }
    }
    }

