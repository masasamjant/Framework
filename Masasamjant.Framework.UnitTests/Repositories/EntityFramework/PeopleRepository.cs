using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    public class PeopleRepository : EntityRepository<Person, int>
    {
        public PeopleRepository(PersonContext context)
            : base(context)
        { }

        public override Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default)
        {
            return Context.AddEntityAsync(entity);
        }

        public override Task<Person?> FindAsync(int identifier, CancellationToken cancellationToken = default)
        {
            var query = Context.Query<Person>().Where(x => x.Identifier == identifier);
            return Context.AsyncQueryExecution.FirstOrDefaultAsync(query, cancellationToken);
        }

        public override Task<Person> RemoveAsync(Person entity, CancellationToken cancellationToken = default)
        {
            return Context.RemoveEntityAsync(entity);
        }

        public override Task<Person> UpdateAsync(Person entity, CancellationToken cancellationToken = default)
        {
            return Context.UpdateEntityAsync(entity);
        }

        public async Task<IReadOnlyCollection<Person>> GetAllPeopleAsync(CancellationToken cancellationToken = default)
        {
            var query = Context.Query<Person>();
            var result = await Context.AsyncQueryExecution.ToListAsync(query, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyCollection<Person>> GetPeopleByNameAsync(string? firstName, string? lastName, CancellationToken cancellationToken = default)
        {
            var query = Context.Query<Person>();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(x => x.FirstName == firstName);

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(x => x.LastName == lastName);

            return await Context.AsyncQueryExecution.ToListAsync(query, cancellationToken);
        }
    }
}
