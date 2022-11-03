using CoreLayer.Dtos;
using CoreLayer.Services.Abstract;
using EntityLayer.Data;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TasinmazApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly AppDbContext _context;

        public LogController(ILogService logService,AppDbContext context)
        {
            _logService = logService;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetAll()
        {
            var get = await _logService.GetAll();
            return Ok(get);
        }
        [HttpPost]
        public async Task<ActionResult> Add(CreateLogDto createLogDto)
        {
            var log = new Log
            {
                Durum = createLogDto.Durum,
                Aciklama = createLogDto.Aciklama,
                IslemTipi = createLogDto.IslemTipi,
                DateTime = createLogDto.DateTime,
                UserIp = createLogDto.UserIp,
            };
            await _logService.Add(log);
            return Ok();
        }
    }
}
