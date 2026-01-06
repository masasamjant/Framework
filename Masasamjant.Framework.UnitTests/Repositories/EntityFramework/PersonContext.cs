using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    public class PersonContext : SqlServerContext
    {
        public PersonContext(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider, new CurrentIdentityProvider())
        { }

        public DbSet<Person> People { get; protected set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonTable());
        }
    }
}
