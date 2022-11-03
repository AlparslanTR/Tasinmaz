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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Mail).IsRequired();
            builder.Property(x => x.RolId).IsRequired();
        }
    }
}
