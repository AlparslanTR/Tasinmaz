using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Configuration
{
    public class RolConfig : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RolName).IsRequired();
        }
    }
}
