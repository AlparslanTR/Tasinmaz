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
    public class TasinmazConfig : IEntityTypeConfiguration<Tasinmaz>
    {
        public void Configure(EntityTypeBuilder<Tasinmaz> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ada).IsRequired();
            builder.Property(x=>x.Parsel).IsRequired();
            builder.Property(x=>x.Nitelik).IsRequired();
            builder.Property(x=>x.XCoordinate).IsRequired();
            builder.Property(x=>x.YCoordinate).IsRequired();
            builder.Property(x=>x.MahalleId).IsRequired();
            builder.Property(x=>x.IlceId).IsRequired();
            builder.Property(x=>x.IlId).IsRequired();
        }
    }
}
