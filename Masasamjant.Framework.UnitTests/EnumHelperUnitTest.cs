using Masasamjant.Resources;

namespace Masasamjant
{
    [TestClass]
    public class EnumHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetUnderlyingEnumTypes()
        {
            var expected = new Type[] { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) };
            var actual = EnumHelper.GetUnderlyingEnumTypes().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsEnumTypeOrUnderlyingEnumType()
        {
            foreach (var type in EnumHelper.GetUnderlyingEnumTypes())
                Assert.IsTrue(EnumHelper.IsEnumTypeOrUnderlyingEnumType(type));

            Assert.IsFalse(EnumHelper.IsEnumTypeOrUnderlyingEnumType(typeof(string)));
            Assert.IsTrue(EnumHelper.IsEnumTypeOrUnderlyingEnumType(typeof(DateTimeKind)));
        }

        [TestMethod]
        public void Test_GetCustomAttribute()
        {
            ErrorBehavior behavior = ErrorBehavior.Cancel;
            var flagsAttribute = EnumHelper.GetCustomAttribute<ErrorBehavior, FlagsAttribute>(behavior);
            Assert.IsNull(flagsAttribute);
            var resourceAttribute = EnumHelper.GetCustomAttribute<ErrorBehavior, ResourceStringAttribute>(behavior);
            Assert.IsNotNull(resourceAttribute);
        }

        [TestMethod]
        public void Test_IsFlagsEnum()
        {
            Assert.IsFalse(EnumHelper.IsFlagsEnum<ErrorBehavior>());
            Assert.IsTrue(EnumHelper.IsFlagsEnum<FileAttributes>());
            Assert.IsFalse(EnumHelper.IsFlagsEnum(ErrorBehavior.Retry));
            Assert.IsTrue(EnumHelper.IsFlagsEnum(FileAttributes.ReadOnly));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FlagCount_Without_Flags_Enum()
        {
            EnumHelper.FlagCount(ErrorBehavior.Retry);
        }

        [TestMethod]
        public void Test_FlagCount()
        {
            FileAttributes attributes = FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden;
            Assert.AreEqual(3, EnumHelper.FlagCount(attributes, true));
            Assert.AreEqual(4, EnumHelper.FlagCount(attributes, false));
            attributes = FileAttributes.Normal | FileAttributes.Compressed;
            Assert.AreEqual(2, EnumHelper.FlagCount(attributes, true));
            Assert.AreEqual(3, EnumHelper.FlagCount(attributes, false));
            attributes = FileAttributes.Normal;
            Assert.AreEqual(1, EnumHelper.FlagCount(attributes, true));
            Assert.AreEqual(2, EnumHelper.FlagCount(attributes, false));
        }

        [TestMethod]
        public void Test_IsDefaultFlag()
        {
            FileAttributes attributes = FileAttributes.None | FileAttributes.Normal;
            Assert.IsFalse(EnumHelper.IsDefaultFlag(attributes, FileAttributes.None));
            attributes = FileAttributes.Normal;
            Assert.IsFalse(EnumHelper.IsDefaultFlag(attributes, FileAttributes.None));
            attributes = FileAttributes.None;
            Assert.IsTrue(EnumHelper.IsDefaultFlag(attributes, FileAttributes.None));
        }

        [TestMethod]
        public void Test_IsSingleFlagsValue()
        {
            FileAttributes attributes = FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden;
            Assert.IsFalse(EnumHelper.IsSingleFlagsValue(attributes, FileAttributes.Normal));
            attributes = FileAttributes.Normal;
            Assert.IsTrue(EnumHelper.IsSingleFlagsValue(attributes, FileAttributes.Normal));
        }

        [TestMethod]
        public void Test_Flags()
        {
            FileAttributes attributes = FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden;
            var actual = EnumHelper.Flags(attributes, false).ToArray();
            var expected = new[] { FileAttributes.Normal, FileAttributes.ReadOnly, FileAttributes.Hidden };
            CollectionAssert.AreEquivalent(expected, actual);
            actual = EnumHelper.Flags(attributes, true).ToArray();
            expected = new[] { FileAttributes.None, FileAttributes.Normal, FileAttributes.ReadOnly, FileAttributes.Hidden };
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
