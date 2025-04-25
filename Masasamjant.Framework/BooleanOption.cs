using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines boolean options.
    /// </summary>
    public enum BooleanOption : int
    {
        /// <summary>
        /// Default
        /// </summary>
        [ResourceString(nameof(BooleanOptionResource.Default), typeof(BooleanOptionResource), UseNonPublicResource = true)]
        Default = 0,

        /// <summary>
        /// False
        /// </summary>
        [ResourceString(nameof(BooleanOptionResource.False), typeof(BooleanOptionResource), UseNonPublicResource = true)]
        False = 1,

        /// <summary>
        /// True
        /// </summary>
        [ResourceString(nameof(BooleanOptionResource.True), typeof(BooleanOptionResource), UseNonPublicResource = true)]
        True = 2
    }
}
