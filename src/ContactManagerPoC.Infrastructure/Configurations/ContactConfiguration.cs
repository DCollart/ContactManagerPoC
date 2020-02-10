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

            builder.OwnsOne(c => c.Address, b =>
            {
                b.Property(a => a.Street).HasColumnName("Street");
                b.Property(a => a.Number).HasColumnName("Number");
                b.Property(a => a.City).HasColumnName("City");
                b.Property(a => a.Country).HasColumnName("Country");
                b.Property(a => a.ZipCode).HasColumnName("ZipCode");

            });


        }
    }
}
