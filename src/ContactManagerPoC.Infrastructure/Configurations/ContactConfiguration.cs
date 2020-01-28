using System;
using System.Collections.Generic;
using System.Text;
using ContactManagerPoC.Domain.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagerPoC.Infrastructure.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.OwnsOne(c => c.FirstName)
                .Property(x => x.Value)
                .HasColumnName("FirstName");

            builder.OwnsOne(c => c.LastName)
                .Property(x => x.Value)
                .HasColumnName("LastName");
        }
    }
}
