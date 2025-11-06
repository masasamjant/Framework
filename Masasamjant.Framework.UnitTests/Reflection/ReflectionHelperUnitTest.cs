using Masasamjant.Configuration;
using System.Reflection;

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

        [TestMethod]
        public void Test_GetProperty()
        {
            object instance = new TestUser("Jack", 10);
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetProperty(instance, "", PropertySupport.Public | PropertySupport.Getter));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetProperty(instance, "  ", PropertySupport.Public | PropertySupport.Getter));

            var property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.None);
            Assert.IsNull(property);

            // Public property.
            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.NonPublic | PropertySupport.Setter);
            Assert.IsNull(property);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.NonPublic | PropertySupport.Getter);
            Assert.IsNull(property);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Setter | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            property = ReflectionHelper.GetProperty(instance, "NickName", PropertySupport.Public | PropertySupport.NonPublic | PropertySupport.Getter | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("NickName", property.Name);

            // Non-public property.
            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Public | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Public | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Public | PropertySupport.Setter | PropertySupport.Getter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Public | PropertySupport.Getter | PropertySupport.Setter);
            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.NonPublic | PropertySupport.Public | PropertySupport.Getter | PropertySupport.Setter);
            Assert.IsNotNull(property);
            Assert.AreEqual("Identifier", property.Name);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.Public | PropertySupport.Getter);
            Assert.IsNull(property);

            property = ReflectionHelper.GetProperty(instance, "Identifier", PropertySupport.Public | PropertySupport.Setter);
            Assert.IsNull(property);

            // Age has IgnorePropertyAttribute so should ignore.
            instance = new Person("Mick", "Mock", 2);
            property = ReflectionHelper.GetProperty(instance, "Age", PropertySupport.Public | PropertySupport.Getter);
            Assert.IsNull(property);
        }

        [TestMethod]
        public void Test_GetProperty_Path()
        {
            Person person = new Person("Mick", "Black", 10)
            {
                Parent = new Person("Jane", "Black", 38)
                {
                    Parent = new Person("Jack", "Black", 74)
                }
            };

            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetProperty(person, "", PropertySupport.None, out var owner));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetProperty(person, "  ", PropertySupport.None, out var owner));

            var property = ReflectionHelper.GetProperty(person, "Parent.Parent.FirstName", PropertySupport.None, out var owner);
            Assert.IsNull(property);
            Assert.IsNull(owner);

            property = ReflectionHelper.GetProperty(person, "Parent.Parent.FirstName", PropertySupport.NonPublic | PropertySupport.Getter, out owner);
            Assert.IsNull(property);
            Assert.IsNull(owner);

            property = ReflectionHelper.GetProperty(person, "Parent.Parent.FirstName", PropertySupport.Public | PropertySupport.Getter, out owner);
            Assert.IsNotNull(property);
            Assert.IsNotNull(owner);
            Assert.IsTrue(ReferenceEquals(person.Parent.Parent, owner));
            Assert.AreEqual("FirstName", property.Name);

            property = ReflectionHelper.GetProperty(person, "Parent.FirstName", PropertySupport.Public | PropertySupport.Getter, out owner);
            Assert.IsNotNull(property);
            Assert.IsNotNull(owner);
            Assert.IsTrue(ReferenceEquals(person.Parent, owner));
            Assert.AreEqual("FirstName", property.Name);

            property = ReflectionHelper.GetProperty(person, "FirstName", PropertySupport.Public | PropertySupport.Getter, out owner);
            Assert.IsNotNull(property);
            Assert.IsNotNull(owner);
            Assert.IsTrue(ReferenceEquals(person, owner));
            Assert.AreEqual("FirstName", property.Name);

            // Age has IgnorePropertyAttribute so should ignore.
            property = ReflectionHelper.GetProperty(person, "Age", PropertySupport.Public | PropertySupport.Getter, out owner);
            Assert.IsNull(property);
            Assert.IsNull(owner);
        }

        [TestMethod]
        public void Test_GetPropertyValue()
        {
            Person person = new Person("Mick", "Black", 10)
            {
                Parent = new Person("Jane", "Black", 38)
                {
                    Parent = new Person("Jack", "Black", 74)
                }
            };

            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetPropertyValue(person, "", false));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetPropertyValue(person, "  ", false));

            var value = ReflectionHelper.GetPropertyValue(person, "Parent.Parent.None", false);
            Assert.IsNull(value);

            value = ReflectionHelper.GetPropertyValue(person, "Parent.Parent.FirstName", false);
            Assert.AreEqual(person.Parent.Parent.FirstName, value);

            value = ReflectionHelper.GetPropertyValue(person, "Parent.FirstName", false);
            Assert.AreEqual(person.Parent.FirstName, value);

            value = ReflectionHelper.GetPropertyValue(person, "FirstName", false);
            Assert.AreEqual(person.FirstName, value);

            var user = new TestUser("Mick Mock", 9);
            value = ReflectionHelper.GetPropertyValue(user, "Identifier", true);
            Assert.AreEqual(user.GetIdentifier(), value);
        }

        [TestMethod]
        public void Test_SetPropertyValue()
        {
            object value = "Jake";

            Person person = new Person("Mick", "Black", 10)
            {
                Parent = new Person("Jane", "Black", 38)
                {
                    Parent = new Person("Jack", "Black", 74)
                }
            };

            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetPropertyValue(person, "", value, false));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetPropertyValue(person, "  ", value, false));

            ReflectionHelper.SetPropertyValue(person, "Parent.Parent.FirstName", value, false);
            Assert.AreEqual(person.Parent.Parent.FirstName, value);

            ReflectionHelper.SetPropertyValue(person, "Parent.FirstName", value, false);
            Assert.AreEqual(person.Parent.FirstName, value);

            ReflectionHelper.SetPropertyValue(person, "FirstName", value, false);
            Assert.AreEqual(person.FirstName, value);

            value = Guid.NewGuid();
            var user = new TestUser("Mick Mock", 9);
            ReflectionHelper.SetPropertyValue(user, "Identifier", value, true);
            Assert.AreEqual(user.GetIdentifier(), value);
        }

        [TestMethod]
        public void Test_InvokeMethod()
        {
            IFastestRouteProvider instance = new FastestRouteProvider();
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.InvokeMethod(instance, "", null));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.InvokeMethod(instance, " ", null));
            Assert.ThrowsException<InvalidOperationException>(() => ReflectionHelper.InvokeMethod(instance, "None", null));
            Assert.ThrowsException<ArgumentException>(() => ReflectionHelper.InvokeMethod(instance, "GetDefaultRoute", new object?[] { null }));
            Assert.ThrowsException<TargetInvocationException>(() => ReflectionHelper.InvokeMethod(instance, "GetFastestRoute", null));

            object? result = ReflectionHelper.InvokeMethod(instance, "GetDefaultRoute", null);
            Assert.AreEqual("DEFAULT", result);
            result = ReflectionHelper.InvokeMethod(instance, "GetShortestRoute", null);
            Assert.AreEqual("SHORTEST", result);
        }

        [TestMethod]
        public void Test_GetMethods()
        {
            BindingFlags binding = BindingFlags.Public | BindingFlags.Instance;
            Type type = typeof(IFastestRouteProvider);

            var methods = ReflectionHelper.GetMethods(type, binding, false).ToArray();
            Assert.AreEqual(1, methods.Length);
            Assert.AreEqual("GetFastestRoute", methods[0].Name);

            methods = ReflectionHelper.GetMethods(type, binding, true).ToArray();
            Assert.AreEqual(3, methods.Length);
            Assert.AreEqual("GetFastestRoute", methods[0].Name);
            Assert.AreEqual("GetShortestRoute", methods[1].Name);
            Assert.AreEqual("GetDefaultRoute", methods[2].Name);

            type = typeof(FastestRouteProvider);
            methods = ReflectionHelper.GetMethods(type, binding, false).ToArray();
            Assert.AreEqual(7, methods.Length);
            var expected = new[] 
            {
                "GetFastestRoute",
                "GetShortestRoute",
                "GetDefaultRoute", 
                "Equals", 
                "GetHashCode",
                "ToString",
                "GetType"
            };
            var actual = methods.Select(x => x.Name).ToArray();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        private interface IRouteProvider
        {
            string GetDefaultRoute();
        }

        private interface IShortestRouteProvider : IRouteProvider
        {
            string GetShortestRoute();
        }

        private interface IFastestRouteProvider : IShortestRouteProvider
        {
            string GetFastestRoute();
        }

        private class RouteProvider : IRouteProvider
        {
            public string GetDefaultRoute() => "DEFAULT";
        }

        private class ShortestRouteProvider : RouteProvider, IShortestRouteProvider
        {
            public string GetShortestRoute() => "SHORTEST";
        }

        private class FastestRouteProvider : ShortestRouteProvider, IFastestRouteProvider
        {
            public string GetFastestRoute() => throw new NotImplementedException();
        }
    }
}
