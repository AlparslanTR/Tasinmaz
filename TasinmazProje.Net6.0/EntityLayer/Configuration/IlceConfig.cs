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
    public class IlceConfig : IEntityTypeConfiguration<Ilce>
    {
        public void Configure(EntityTypeBuilder<Ilce> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IlceName).IsRequired();
            builder.Property(x => x.IlId).IsRequired();
        }
    }
}
