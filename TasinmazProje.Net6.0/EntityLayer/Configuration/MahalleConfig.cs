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
    public class MahalleConfig : IEntityTypeConfiguration<Mahalle>
    {
        public void Configure(EntityTypeBuilder<Mahalle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MahalleAdi).IsRequired();
            builder.Property(x => x.IlceId).IsRequired();
        }
    }
}
