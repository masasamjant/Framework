namespace Masasamjant.Reflection
{
    [TestClass]
    public class PropertyInfoHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsIndexProperty()
        {
            var list = new List<int>();
            var type = list.GetType();
            var property = type.GetProperty("Item");
            Assert.IsTrue(property!.IsIndexProperty());
            property = type.GetProperty("Count");
            Assert.IsFalse(property!.IsIndexProperty());
        }

        [TestMethod]
        public void Test_IsEnumerable()
        {
            var type = typeof(Person);
            var property = type.GetProperty("FirstName");
            Assert.IsFalse(property!.IsEnumerable(true));
            Assert.IsTrue(property!.IsEnumerable(false));
            property = type.GetProperty("Age");
            Assert.IsFalse(property!.IsEnumerable());
        }

        [TestMethod]
        public void Test_HasCustomAttribute()
        {
            var type = typeof(Person);
            var property = type.GetProperty("FirstName");
            Assert.IsFalse(property!.HasCustomAttribute<IgnorePropertyAttribute>());
            Assert.IsFalse(property!.HasCustomAttribute<IgnorePropertyAttribute>(out var attribute));
            Assert.IsNull(attribute);
            property = type.GetProperty("Age");
            Assert.IsTrue(property!.HasCustomAttribute<IgnorePropertyAttribute>());
            Assert.IsTrue(property!.HasCustomAttribute<IgnorePropertyAttribute>(out attribute));
            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void Test_GetGetterExpression_When_Not_Same_Type()
        {
            var property = typeof(Person).GetProperty("FirstName");
            Assert.ThrowsException<ArgumentException>(() =>
            {
                PropertyInfoHelper.GetGetterExpression<DateTime, string>(property!);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                PropertyInfoHelper.GetGetterExpression<DateTime>(property!);
            });
        }

        [TestMethod]
        public void Test_GetGetterExpression()
        {
            var property = typeof(Person).GetProperty("FirstName");
            var expression = PropertyInfoHelper.GetGetterExpression<Person, string>(property!);
            var expected = "Tim";
            var actual = expression.Compile().Invoke(new Person("Tim", "Test", 20));
            Assert.AreEqual(expected, actual);
            var expression2 = PropertyInfoHelper.GetGetterExpression<Person>(property!);
            var actual2 = expression2.Compile().Invoke(new Person("Tim", "Test", 20));
            Assert.AreEqual(expected, actual2);
        }
    }
}