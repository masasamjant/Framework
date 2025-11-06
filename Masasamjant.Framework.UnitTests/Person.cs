using Masasamjant.Reflection;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Masasamjant
{
    [TypeConverter(typeof(PersonTypeConverter))]
    public class Person : IEquatable<Person>
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [IgnoreProperty]
        public int Age { get; set; }

        public Person? Parent { get; set; }

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

    public class PersonTypeConverter : TypeConverter
    {
        private const char ComponentSeparator = '|';
        
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return destinationType != null && destinationType.Equals(typeof(string));
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is Person p)
            {
                return string.Join(ComponentSeparator, [p.FirstName, p.LastName, p.Age]);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType.Equals(typeof(string));
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string str)
            {
                var parts = str.Split(ComponentSeparator, StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length == 3)
                {
                    return new Person(parts[0], parts[1], int.Parse(parts[2]));
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
