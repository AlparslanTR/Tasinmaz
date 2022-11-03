using CoreLayer.Models;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        
        public DbSet<Il> Ils { get; set; }
        public DbSet<Ilce> Ilces { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Mahalle> Mahalles { get; set; }
        public DbSet<Tasinmaz> Tasinmazs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
}
