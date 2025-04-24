using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines supported equality comparison operators.
    /// </summary>
    public enum EqualityOperator : int
    {
        /// <summary>
        /// Compare that two values are equal.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.Equal), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        Equal = 0,

        /// <summary>
        /// Compare that two values are not equal.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.NotEqual), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        NotEqual = 1,

        /// <summary>
        /// Compare that first value is greater than second value.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.GreaterThan), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        GreaterThan = 2,

        /// <summary>
        /// Compare that first value is greater than or equal to second value.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.GreaterThanOrEqual), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        GreaterThanOrEqual = 3,

        /// <summary>
        /// Compare that first value is less than second value.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.LessThan), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        LessThan = 4,

        /// <summary>
        /// Compare that first value is less than or equal to second value.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.LessThanOrEqual), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        LessThanOrEqual = 5,

        /// <summary>
        /// Compare that first value start with second value. Only supported with strings.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.StartsWith), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        StartsWith = 6,

        /// <summary>
        /// Compare that first value ends with second value. Only supported with strings.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.EndsWith), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        EndsWith = 7,

        /// <summary>
        /// Compare that first value contains second value. Only supported with strings.
        /// </summary>
        [ResourceString(nameof(EqualityOperatorResource.Contains), typeof(EqualityOperatorResource), UseNonPublicResource = true)]
        Contains = 8
    }
}
