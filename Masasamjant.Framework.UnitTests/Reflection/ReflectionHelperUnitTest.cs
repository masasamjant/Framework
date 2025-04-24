using Masasamjant.Configuration;

namespace Masasamjant.Reflection
{
    [TestClass]
    public class ReflectionHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetExtensionMethods()
        {
            var methods = ReflectionHelper.GetExtensionMethods(typeof(TestType));
            Assert.IsNotNull(methods);
            Assert.IsFalse(methods.Any());
            methods = ReflectionHelper.GetExtensionMethods(typeof(DateTime), typeof(ErrorBehavior).Assembly);
            Assert.IsNotNull(methods);
            Assert.IsTrue(methods.Any());
        }

        [TestMethod]
        public void Test_GetExtensionMethod()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetExtensionMethod(typeof(DateTime), string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetExtensionMethod(typeof(DateTime), "  "));
            var method = ReflectionHelper.GetExtensionMethod(typeof(TestType), "TestMethod");
            Assert.IsNull(method);
            method = ReflectionHelper.GetExtensionMethod(typeof(DateTime), "Noon", typeof(ErrorBehavior).Assembly);
            Assert.IsNotNull(method);
            var method1 = ReflectionHelper.GetExtensionMethod(typeof(DateTime), "IsFuture", typeof(ErrorBehavior).Assembly, new Type[] { typeof(IDateTimeConfiguration), typeof(TimeSpan) });
            Assert.IsNotNull(method1);
            var method2 = ReflectionHelper.GetExtensionMethod(typeof(DateTime), "IsFuture", typeof(ErrorBehavior).Assembly, new Type[] { new ThisType(typeof(DateTime)), typeof(IDateTimeConfiguration), typeof(TimeSpan) });
            Assert.IsNotNull(method2);
            Assert.AreEqual(method1, method2);
            Assert.ThrowsException<ArgumentException>(() => ReflectionHelper.GetExtensionMethod(typeof(DateTime), "IsFuture", typeof(ErrorBehavior).Assembly, new Type[] { typeof(IDateTimeConfiguration), new ThisType(typeof(DateTime)), typeof(TimeSpan) }));
        }

        [TestMethod]
        public void Test_IsThisType()
        {
            Type t = new ThisType(typeof(DateTime));
            Assert.IsTrue(ReflectionHelper.IsThisType(t));
            t = DateTime.Now.GetType();
            Assert.IsFalse(ReflectionHelper.IsThisType(t));
        }

        [TestMethod]
        public void Test_ToThisType()
        {
            Type type = typeof(DateTime);
            ThisType thisType = ReflectionHelper.ToThisType(type);
            Assert.AreEqual(type, thisType.GetActualType());
            ThisType thisType2 = ReflectionHelper.ToThisType(thisType);
            Assert.IsTrue(ReferenceEquals(thisType, thisType2));
        }

        [TestMethod]
        public void Test_IsNullType()
        {
            Type t = new NullType(typeof(DateTime));
            Assert.IsTrue(ReflectionHelper.IsNullType(t));
            t = DateTime.Now.GetType();
            Assert.IsFalse(ReflectionHelper.IsNullType(t));
        }

        [TestMethod]
        public void Test_ToNullType()
        {
            Type type = typeof(DateTime);
            NullType nullType = ReflectionHelper.ToNullType(type);
            Assert.AreEqual(type, nullType.GetActualType());
            NullType nullType2 = ReflectionHelper.ToNullType(nullType);
            Assert.IsTrue(ReferenceEquals(nullType, nullType2));
        }
    }
}
