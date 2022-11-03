using CoreLayer.Services.Abstract;
using EntityLayer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TasinmazApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public UserProfileController(IUserService userService,AppDbContext appDbContext)
        {
            _userService = userService;
            _context = appDbContext;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Object> GetUserProfile()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            int id = Convert.ToInt16(userId);
            var user = await _userService.GetUserProfile(id);
            return new
            {
                user.Name,
                user.LastName,
                user.Mail,
                user.RolId
            };
        }
    }
}
