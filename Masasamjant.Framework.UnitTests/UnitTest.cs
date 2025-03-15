using Microsoft.Extensions.Configuration;

namespace Masasamjant
{ 
    public abstract class UnitTest
    {
        protected static IConfiguration GetConfiguration(Dictionary<string, string?> values)
        {
            return new ConfigurationBuilder().AddInMemoryCollection(values).Build();
        }

        protected static List<int> CreateInt32List(int count)
        {
            var list = new List<int>(count);

            for (int i = 0; i < count; i++)
                list.Add(i);

            return list;
        }
    }
}
