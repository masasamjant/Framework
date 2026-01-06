using Masasamjant.Repositories.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    public class PersonTable : DbTableConfiguration<Person>
    {
        public PersonTable()
            : base("Framework", nameof(Person))
        { }

        protected override void ConfigureTable(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.Identifier).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(Person.MaxFirstNameLength);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(Person.MaxLastNameLength);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.CreatedBy).HasMaxLength(30);
            builder.Property(x => x.ModifiedAt);
            builder.Property(x => x.ModifiedBy).HasMaxLength(30);
            builder.HasKey(x => x.Identifier);
        }
    }
}
