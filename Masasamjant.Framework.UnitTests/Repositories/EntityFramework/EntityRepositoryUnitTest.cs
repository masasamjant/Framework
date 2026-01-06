using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    [TestClass]
    public class EntityRepositoryUnitTest : UnitTest
    {
        private readonly string connectionString = @"Server=localhost\SQLExpress;Database=FrameworkDemo;Trusted_Connection=True;TrustServerCertificate=true;";

        [TestMethod]
        public async Task Test_Database_Operations()
        {
            using (var context = CreateContext())
            {
                var person = new Person("Mickey", "Mouse");
                var repository = new PeopleRepository(context);
                person = await repository.AddAsync(person);
                await repository.SaveAsync();
            }

            using (var context = CreateContext())
            {
                var repository = new PeopleRepository(context);
                var persons = await repository.GetPeopleByNameAsync("Mickey", "Mouse");
                Assert.AreEqual(1, persons.Count);
                var person = persons.First();
                person = await repository.RemoveAsync(person);
                await repository.SaveAsync();
            }
        }

        private PersonContext CreateContext()
        {
            var provider = new ConnectionStringProvider(connectionString);
            return new PersonContext(provider);
        }
    }
}
