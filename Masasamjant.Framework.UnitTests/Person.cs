using Masasamjant.Reflection;

namespace Masasamjant
{
    public class Person : IEquatable<Person>
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; }

        public string LastName { get; }

        [IgnoreProperty]
        public int Age { get; }

        public bool Equals(Person? other)
        {
            if (other == null)
                return false;

            return FirstName == other.FirstName &&
                LastName == other.LastName &&
                Age == other.Age;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Age);
        }
    }
}
