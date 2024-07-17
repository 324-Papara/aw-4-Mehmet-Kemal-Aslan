﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.Configuration
{
    public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
    {
        public void Configure(EntityTypeBuilder<CustomerPhone> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(x => x.CustomerId).IsRequired(true);
            builder.Property(x => x.CountyCode).IsRequired(true).HasMaxLength(3);
            builder.Property(x => x.Phone).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.IsDefault).IsRequired(true);

            builder.HasIndex(x => new { x.CountyCode, x.Phone }).IsUnique(true);
        }
    }
}
