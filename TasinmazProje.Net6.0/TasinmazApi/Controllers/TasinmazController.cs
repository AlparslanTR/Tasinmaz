using CoreLayer.Dtos;
using CoreLayer.Services.Abstract;
using EntityLayer.Data;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TasinmazApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly ITasinmazService _tasinmazService;
        private readonly AppDbContext _context;

        public TasinmazController(ILogService logService, ITasinmazService tasinmazService,AppDbContext appDbContext)
        {
            _logService = logService;
            _tasinmazService = tasinmazService;
            _context = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var get = await _tasinmazService.GetAll();
            return Ok(get);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var login = User.Identity.Name;
            var writer = _context.Users.Where(x => x.Mail == login).Select(x => x.Id).FirstOrDefault();
            var get = await _tasinmazService.GetById(id);
            if (get == null)
            {
                return NotFound("Taşınmaz Bulunamadı Tekrar Deneyin");
            }
            return Ok(get);
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreateTasinmazDto createTasinmazDto)
        {
            var login = User.Identity.Name;
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            await _logService.Add(
                new Log
                {
                    Durum = "Başarılı",
                    UserId = userId,
                    Aciklama = "Taşınmaz Eklendi",
                    IslemTipi = "Taşınmaz Ekleme",
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                    UserIp = HttpContext.Connection.RemoteIpAddress?.ToString()
                });
            var add = new Tasinmaz
            {
                IlId = createTasinmazDto.IlId,
                IlceId = createTasinmazDto.IlceId,
                MahalleId = createTasinmazDto.MahalleId,
                Ada = createTasinmazDto.Ada,
                Parsel = createTasinmazDto.Parsel,
                Nitelik = createTasinmazDto.Nitelik,
                XCoordinate = createTasinmazDto.XCoordinate,
                YCoordinate = createTasinmazDto.YCoordinate,
            };
            await _tasinmazService.Add(add);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var login = User.Identity.Name;
            var writer = _context.Users.Where(x => x.Mail == login).Select(x => x.Id).FirstOrDefault();
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            await _tasinmazService.Delete(id);

            await _logService.Add(
                new Log
                {
                    UserId = userId,
                    Durum = "Başarılı",
                    Aciklama = "Taşınmaz Silindi",
                    IslemTipi = "Taşınmaz Silme",
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                    UserIp = HttpContext.Connection.RemoteIpAddress?.ToString()
                });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTasinmazDto updateTasinmazDto)
        {
            var login = User.Identity.Name;
            var writer = _context.Users.Where(x => x.Mail == login).Select(x => x.Id).FirstOrDefault();
            var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            Tasinmaz update = new()
            {
                Id = id,
                IlId = updateTasinmazDto.IlId,
                IlceId = updateTasinmazDto.IlceId,
                MahalleId = updateTasinmazDto.MahalleId,
                Ada = updateTasinmazDto.Ada,
                Parsel = updateTasinmazDto.Parsel,
                Nitelik = updateTasinmazDto.Nitelik
            };
            await _logService.Add(new Log
            {
                Durum = "Başarılı",
                UserId=userId,
                Aciklama = "Taşınmaz Güncelleme Başarılı bir şekilde gerçekleşti",
                IslemTipi = "Taşınmaz Güncelleme",
                DateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                UserIp = HttpContext.Connection.RemoteIpAddress?.ToString()
            });
            await _tasinmazService.Update(update);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Il>>> GetAllCities()
        {
            var get = await _tasinmazService.GetAllCities();
            return Ok(get);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetAllDistricts(int id)
        {
            var get = await _tasinmazService.GetAllDistricts(id);
            return Ok(get);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetAllNeighbourhood(int id)
        {
            var get = await _tasinmazService.GetAllNeighbourhood(id);
            return Ok(get);
        }
    }
}
