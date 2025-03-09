using Masasamjant.Resources;

namespace Masasamjant
{
    [ResourceString("User", typeof(PublicUnitTestResource))]
    public class TestUser
    {
        [ResourceString("Name", typeof(PublicUnitTestResource))]
        private string? name;
        private int? age;

        public TestUser(string name, int? age)
        {
            this.name = name;
            this.age = age;
        }

        [ResourceString("Name", typeof(PublicUnitTestResource))]
        public string Name
        {
            get { return name ?? string.Empty; }
        }

        public int? Age
        {
            get { return age; }
        }
    }
}
