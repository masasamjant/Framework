using Masasamjant.Modeling.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    public class Person : Record<int>
    {
        public const int MaxFirstNameLength = 50;
        public const int MaxLastNameLength = 50;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person()
        { }

        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; protected set; } = string.Empty;

        [MaxLength(MaxLastNameLength)]
        public string LastName { get; protected set; } = string.Empty;
    }
}
