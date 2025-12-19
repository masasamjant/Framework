using Microsoft.Extensions.Configuration;
using System.Text;

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

        protected static List<TValue> CreateList<TValue>(TValue value, int count)
        {
            var list = new List<TValue>(count);
            for (int i = 0; i < count; i++)
                list.Add(value);
            return list;
        }

        protected static byte[] GetLoremIpsumData()
        {
            string str = GetLoremIpsumText();
            return Encoding.UTF8.GetBytes(str);
        }

        protected static string GetLoremIpsumText()
        {
            const string file = "lorem.txt";
            using (var reader = File.OpenText(file))
            {
                return reader.ReadToEnd();
            }
        }

        protected static void AssertLine(string line, string[] expectedContents)
        {
            foreach (var expectedContent in expectedContents)
                Assert.IsTrue(line.Contains(expectedContent));
        }
    }
}
