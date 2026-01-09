using System.Reflection;

namespace Masasamjant
{
    [TestClass]
    public class TypeHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void TestImplements()
        {
            Assert.IsTrue(TypeHelper.Implements(typeof(IList<string>), typeof(IList<string>)));
            Assert.IsTrue(TypeHelper.Implements(typeof(List<string>), typeof(IList<string>)));
            Assert.IsFalse(TypeHelper.Implements(typeof(string), typeof(IList<string>)));
            Assert.ThrowsException<ArgumentException>(() => TypeHelper.Implements(typeof(string), typeof(string)));
        }

        [TestMethod]
        public void Test_IsOfType()
        {
            Assert.IsTrue(typeof(string).IsOfType(typeof(string)));
            Assert.IsTrue(typeof(string).IsOfType(typeof(object)));
            Assert.IsFalse(typeof(string).IsOfType(typeof(IList<string>)));
        }

        [TestMethod]
        public void Test_GetTypeName()
        {
            Assert.AreEqual(string.Empty, TypeHelper.GetTypeName(null));
            Assert.AreEqual("Assembly Qualified Name", TypeHelper.GetTypeName(new TestType("Name", assemblyQualifiedName: "Assembly Qualified Name")));
            Assert.AreEqual("Full Name", TypeHelper.GetTypeName(new TestType("Name", fullName: "Full Name")));
            Assert.AreEqual("Name", TypeHelper.GetTypeName(new TestType("Name")));
        
            var type = new TestType("Name", assemblyQualifiedName: "Assembly Qualified Name", fullName: "Full Name");
            Assert.AreEqual(string.Empty, TypeHelper.GetTypeName(null, PreferredTypeName.AssemblyQualifiedName));
            Assert.AreEqual(string.Empty, TypeHelper.GetTypeName(null, PreferredTypeName.FullName));
            Assert.AreEqual(string.Empty, TypeHelper.GetTypeName(null, PreferredTypeName.Name));
            Assert.AreEqual("Assembly Qualified Name", TypeHelper.GetTypeName(type, PreferredTypeName.AssemblyQualifiedName));
            Assert.AreEqual("Full Name", TypeHelper.GetTypeName(type, PreferredTypeName.FullName));
            Assert.AreEqual("Name", TypeHelper.GetTypeName(type, PreferredTypeName.Name));
            type = new TestType("");
            Assert.AreEqual("", TypeHelper.GetTypeName(type, PreferredTypeName.Name));
        }

        [TestMethod]
        public void Test_GetTypeHierarchy()
        {
            var type = typeof(TestType);
            var expected = new Type[] { typeof(Type), typeof(MemberInfo), typeof(object) };
            var actual = type.GetTypeHierarchy(false).ToArray();
            CollectionAssert.AreEqual(expected, actual);
            expected = new Type[] { typeof(TestType), typeof(Type), typeof(MemberInfo), typeof(object) };
            actual = type.GetTypeHierarchy(true).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsConcrete()
        {
            Assert.IsFalse(TypeHelper.IsConcrete(typeof(Type)));
            Assert.IsFalse(TypeHelper.IsConcrete(typeof(List<>)));
            Assert.IsFalse(TypeHelper.IsConcrete(typeof(ICloneable)));
            Assert.IsTrue(TypeHelper.IsConcrete(typeof(string)));
        }

        [TestMethod]
        public void Test_TypeScopedKey()
        {
            string key = "Key";
            string expected = "System.DateTime+Key";
            string actual = TypeHelper.GetTypeScopedKey(typeof(DateTime), key);
            Assert.AreEqual(expected, actual);
            actual = TypeHelper.GetTypeScopedKey(DateTime.Now, key);
            Assert.AreEqual(expected, actual);
            string original = TypeHelper.GetOriginalKey(typeof(DateTime), actual);
            Assert.AreEqual(key, original);
        }

        [TestMethod]
        public void Test_GetOriginalKey()
        {
            Assert.AreEqual("Foo", TypeHelper.GetOriginalKey(typeof(DateTime), "Foo"));
            Assert.AreEqual("System.DateTimeFoo", TypeHelper.GetOriginalKey(typeof(DateTime), "System.DateTimeFoo"));
            Assert.AreEqual("Foo", TypeHelper.GetOriginalKey(typeof(DateTime), "System.DateTime+Foo"));
            Assert.AreEqual("Foo", TypeHelper.GetOriginalKey(DateTime.Now, "System.DateTime+Foo"));
        }
    }
}
