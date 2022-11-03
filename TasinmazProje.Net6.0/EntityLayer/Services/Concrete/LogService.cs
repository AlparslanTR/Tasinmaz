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
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _context.Logs.ToListAsync();
        }
    }
}
