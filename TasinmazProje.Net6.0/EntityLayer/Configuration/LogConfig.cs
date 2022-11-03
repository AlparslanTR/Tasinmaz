using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Configuration
{
    public class LogConfig : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Durum).IsRequired();
            builder.Property(x => x.IslemTipi).IsRequired();
            builder.Property(x => x.Aciklama).IsRequired();
            builder.Property(x => x.DateTime).IsRequired();
            builder.Property(x => x.UserIp).IsRequired();
        }
    }
}
