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
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.IsDefaultFlag(ErrorBehavior.Cancel));
        }

        [TestMethod]
        public void Test_IsSingleFlagsValue()
        {
            FileAttributes attributes = FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden;
            Assert.IsFalse(EnumHelper.IsSingleFlagsValue(attributes, FileAttributes.Normal));
            attributes = FileAttributes.Normal;
            Assert.IsTrue(EnumHelper.IsSingleFlagsValue(attributes, FileAttributes.Normal));
            Assert.IsFalse(EnumHelper.IsSingleFlagsValue(attributes, FileAttributes.Normal | FileAttributes.Hidden));
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
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.Flags(ErrorBehavior.Cancel));
        }

        [TestMethod]
        public void Test_AppendFlag()
        {
            Type enumType = typeof(int);
            object value = FileAttributes.Normal;
            object flag = FileAttributes.ReadOnly;
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.AppendFlag(enumType, value, flag));
            
            enumType = typeof(DateTimeKind);
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.AppendFlag(enumType, value, flag));
            
            enumType = typeof(FileAttributes);
            value = DateTimeKind.Local;
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.AppendFlag(enumType, value, flag));

            value = FileAttributes.Normal;
            flag = DateTimeKind.Local;
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.AppendFlag(enumType, value, flag));

            value = FileAttributes.Normal;
            flag = FileAttributes.ReadOnly;
            FileAttributes expected = FileAttributes.Normal | FileAttributes.ReadOnly;
            FileAttributes actual = (FileAttributes)EnumHelper.AppendFlag(enumType, value, flag);
            Assert.AreEqual(expected, actual);

            value = (int)FileAttributes.Normal;
            flag = (int)FileAttributes.ReadOnly;
            expected = FileAttributes.Normal | FileAttributes.ReadOnly;
            actual = (FileAttributes)EnumHelper.AppendFlag(enumType, value, flag);
            Assert.AreEqual(expected, actual);

            AssertAppendFlag(typeof(Int64Enum), Int64Enum.Value2, Int64Enum.Value8, Int64Enum.Value2 | Int64Enum.Value8);
            AssertAppendFlag(typeof(UInt64Enum), UInt64Enum.Value2, UInt64Enum.Value8, UInt64Enum.Value2 | UInt64Enum.Value8);
            AssertAppendFlag(typeof(UInt32Enum), UInt32Enum.Value2, UInt32Enum.Value8, UInt32Enum.Value2 | UInt32Enum.Value8);
            AssertAppendFlag(typeof(Int16Enum), Int16Enum.Value2, Int16Enum.Value8, Int16Enum.Value2 | Int16Enum.Value8);
            AssertAppendFlag(typeof(UInt16Enum), UInt16Enum.Value2, UInt16Enum.Value8, UInt16Enum.Value2 | UInt16Enum.Value8);
            AssertAppendFlag(typeof(ByteEnum), ByteEnum.Value2, ByteEnum.Value8, ByteEnum.Value2 | ByteEnum.Value8);
            AssertAppendFlag(typeof(SByteEnum), SByteEnum.Value2, SByteEnum.Value8, SByteEnum.Value2 | SByteEnum.Value8);
        }

        private static void AssertAppendFlag<T>(Type enumType, object value, object flag, T expected)
            where T : struct, Enum
        {
            T actual = (T)EnumHelper.AppendFlag(enumType, value, flag);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ConvertToEnum()
        {
            Type enumType = typeof(DateTime);
            object value = DateTimeKind.Utc;
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.ConvertToEnum(enumType, value));

            enumType = typeof(DateTimeKind);
            value = DateTimeKind.Utc;
            Assert.AreEqual(DateTimeKind.Utc, EnumHelper.ConvertToEnum(enumType, value));

            value = "Foo";
            Assert.ThrowsException<InvalidCastException>(() => EnumHelper.ConvertToEnum(enumType, value));

            value = "UTC";
            Assert.AreEqual(DateTimeKind.Utc, EnumHelper.ConvertToEnum(enumType, value));

            value = "utc";
            Assert.AreEqual(DateTimeKind.Utc, EnumHelper.ConvertToEnum(enumType, value));

            value = DateTime.Now;
            Assert.ThrowsException<InvalidCastException>(() => EnumHelper.ConvertToEnum(enumType, value));

            value = (int)DateTimeKind.Utc;
            Assert.AreEqual(DateTimeKind.Utc, EnumHelper.ConvertToEnum(enumType, value));

            value = 999;
            Assert.AreEqual((DateTimeKind)999, EnumHelper.ConvertToEnum(enumType, value));
        }

        [Flags]
        private enum Int64Enum : long
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum UInt64Enum : ulong
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum UInt32Enum : uint
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum Int16Enum : short
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum UInt16Enum : ushort
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum ByteEnum : byte
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }

        [Flags]
        private enum SByteEnum : sbyte
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4,
            Value8 = 8,
            Value16 = 16,
            Value32 = 32
        }
    }
}
