using Countries__Cities.Core.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Repository.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.name).IsRequired().HasMaxLength(40);
            builder.Property(x => x.imageUrl).IsRequired().HasMaxLength(400);
            builder.Property(x => x.capital).IsRequired().HasMaxLength(40);

            builder.ToTable("Countries");

        }
    }
}
