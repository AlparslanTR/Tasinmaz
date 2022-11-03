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
    public class TasinmazService : ITasinmazService
    {
        private readonly AppDbContext _context;

        public TasinmazService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Tasinmaz tasinmaz)
        {
            _context.Tasinmazs.Add(tasinmaz);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var getId=await _context.Tasinmazs.FindAsync(id);
            _context.Tasinmazs.Remove(getId);
            await _context.SaveChangesAsync();
        }

        public async Task<Tasinmaz> GetById(int id)
        {
            return await _context.Tasinmazs.FindAsync(id);
        }

        public async Task<IEnumerable<Tasinmaz>> GetAll()
        {
            return await _context.Tasinmazs.Include(x=>x.Mahalle).ThenInclude(x=>x.Ilceler).ThenInclude(x=>x.Iller).ToListAsync();
        }

        public async Task<IEnumerable<Il>> GetAllCities()
        {
            return await _context.Ils.ToListAsync();
        }

        public async Task<IEnumerable<Ilce>> GetAllDistricts(int id)
        {
            return await _context.Ilces.Include(x => x.Iller).Where(x => x.IlId == id).ToListAsync();
        }

        public async Task<IEnumerable<Mahalle>> GetAllNeighbourhood(int id)
        {
            return await _context.Mahalles.Include(x => x.Ilceler).Where(x => x.IlceId == id).ToListAsync();
        }

        public async Task Update(Tasinmaz tasinmaz)
        {
            var getId = await _context.Tasinmazs.FindAsync(tasinmaz.Id);
            getId.IlId = tasinmaz.IlId;
            getId.IlceId= tasinmaz.IlceId;
            getId.MahalleId = tasinmaz.MahalleId;
            getId.Ada = tasinmaz.Ada;
            getId.Parsel = tasinmaz.Parsel;
            getId.Nitelik = tasinmaz.Nitelik;
            await _context.SaveChangesAsync();
        }
    }
}
