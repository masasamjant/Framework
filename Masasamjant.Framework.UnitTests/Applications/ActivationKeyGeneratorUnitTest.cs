namespace Masasamjant.Applications
{
    [TestClass]
    public class ActivationKeyGeneratorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Default_Constructor()
        {
            var properties = ActivationKeyProperties.Default;
            var generator = new ActivationKeyGenerator();
            Assert.AreEqual(properties.ComponentCount, generator.KeyProperties.ComponentCount);
            Assert.AreEqual(properties.ComponentLength, generator.KeyProperties.ComponentLength);
            Assert.AreEqual(properties.ComponentSeparator, generator.KeyProperties.ComponentSeparator);
            Assert.AreEqual(properties.PrefixLength, generator.KeyProperties.PrefixLength);
            CollectionAssert.AreEqual(properties.NumberToLetterMap.ToDictionary(), generator.KeyProperties.NumberToLetterMap.ToDictionary());
            CollectionAssert.AreEqual(properties.NumberToNumberMap.ToDictionary(), generator.KeyProperties.NumberToNumberMap.ToDictionary());
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var properties = new ActivationKeyProperties('+', 3, 3, 0);
            var generator = new ActivationKeyGenerator(properties);
            Assert.AreSame(properties, generator.KeyProperties);
        }

        [TestMethod]
        public void Test_CreateActivationKey_Without_Prefix()
        {
            var generator = new ActivationKeyGenerator();
            ActivationKeySeed? previousSeed = null;

            int length = generator.KeyProperties.ComponentCount * generator.KeyProperties.ComponentLength + (generator.KeyProperties.ComponentCount - 1) * 1; // Separator length is 1

            var key1 = generator.CreateActivationKey(previousSeed);
            Assert.IsNotNull(key1);
            Assert.AreEqual(length, key1.Value.Length);

            var key2 = generator.CreateActivationKey(key1.Seed);
            Assert.IsNotNull(key2);
            Assert.AreNotEqual(key1, key2);
            Assert.AreEqual(length, key2.Value.Length);
        }

        [TestMethod]
        public void Test_CreateActivationKey_With_Prefix()
        {
            var generator = new ActivationKeyGenerator(new ActivationKeyProperties('-', 4, 4, 3));
            ActivationKeySeed? previousSeed = null;
            string prefix = "ABC";

            int length = prefix.Length + 1 + generator.KeyProperties.ComponentCount * generator.KeyProperties.ComponentLength + (generator.KeyProperties.ComponentCount - 1); // Separator length is 1
            
            var key1 = generator.CreateActivationKey(prefix, previousSeed);
            Assert.IsNotNull(key1);
            Assert.AreEqual(length, key1.Value.Length);
            Assert.IsTrue(key1.Value.StartsWith(prefix + "-"));

            var key2 = generator.CreateActivationKey(prefix, key1.Seed);
            Assert.IsNotNull(key2);
            Assert.AreNotEqual(key1, key2);
            Assert.AreEqual(length, key2.Value.Length);
            Assert.IsTrue(key2.Value.StartsWith(prefix + "-"));
        }
    }
}
