namespace Masasamjant.Applications
{
    [TestClass]
    public class ActivationKeyPropertiesUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Default()
        {
            var properties = ActivationKeyProperties.Default;
            Assert.AreEqual('-', properties.ComponentSeparator);
            Assert.AreEqual(4, properties.ComponentLength);
            Assert.AreEqual(4, properties.ComponentCount);
            Assert.AreEqual(4, properties.PrefixLength);
            var numberToNumberMap = ActivationKeyProperties.DefaultNumberToNumberMap();
            var numberToLetterMap = ActivationKeyProperties.DefaultNumberToLetterMap();
            CollectionAssert.AreEqual(numberToNumberMap.Mappings.ToArray(), properties.NumberToNumberMap.Mappings.ToArray());
            CollectionAssert.AreEqual(numberToLetterMap.Mappings.ToArray(), properties.NumberToLetterMap.Mappings.ToArray());
        }

        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new ActivationKeyProperties(' ', ActivationKeyProperties.MinComponentLength, ActivationKeyProperties.MinComponentCount, ActivationKeyProperties.MinPrefixLength));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MinComponentLength - 1, ActivationKeyProperties.MinComponentCount, ActivationKeyProperties.MinPrefixLength));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MinComponentLength, ActivationKeyProperties.MinComponentCount - 1, ActivationKeyProperties.MinPrefixLength));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MinComponentLength, ActivationKeyProperties.MinComponentCount, ActivationKeyProperties.MinPrefixLength -1));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MaxComponentLength + 1, ActivationKeyProperties.MaxComponentCount, ActivationKeyProperties.MaxPrefixLength));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MaxComponentLength, ActivationKeyProperties.MaxComponentCount + 1, ActivationKeyProperties.MaxPrefixLength));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ActivationKeyProperties('-', ActivationKeyProperties.MaxComponentLength, ActivationKeyProperties.MaxComponentCount, ActivationKeyProperties.MaxPrefixLength + 1));

            var properties = new ActivationKeyProperties('+', ActivationKeyProperties.MinComponentLength + 1, ActivationKeyProperties.MinComponentCount + 1, ActivationKeyProperties.MinPrefixLength + 1);
            Assert.AreEqual('+', properties.ComponentSeparator);
            Assert.AreEqual(ActivationKeyProperties.MinComponentLength + 1, properties.ComponentLength);
            Assert.AreEqual(ActivationKeyProperties.MinComponentCount + 1, properties.ComponentCount);
            Assert.AreEqual(ActivationKeyProperties.MinPrefixLength + 1, properties.PrefixLength);
            Assert.IsTrue(properties.UsePrefix);
            var numberToNumberMap = ActivationKeyProperties.DefaultNumberToNumberMap();
            var numberToLetterMap = ActivationKeyProperties.DefaultNumberToLetterMap();
            CollectionAssert.AreEqual(numberToNumberMap.Mappings.ToArray(), properties.NumberToNumberMap.Mappings.ToArray());
            CollectionAssert.AreEqual(numberToLetterMap.Mappings.ToArray(), properties.NumberToLetterMap.Mappings.ToArray());

            properties = new ActivationKeyProperties('+', ActivationKeyProperties.MinComponentLength + 1, ActivationKeyProperties.MinComponentCount + 1);
            Assert.AreEqual('+', properties.ComponentSeparator);
            Assert.AreEqual(ActivationKeyProperties.MinComponentLength + 1, properties.ComponentLength);
            Assert.AreEqual(ActivationKeyProperties.MinComponentCount + 1, properties.ComponentCount);
            Assert.AreEqual(0, properties.PrefixLength);
            Assert.IsFalse(properties.UsePrefix);
        }

        [TestMethod]
        public void Test_ChangeNumberToNumberMap()
        {
            var properties = new ActivationKeyProperties('-', 4, 4);

            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToNumberMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', '9'},
                {'1', '8'},
                {'2', '7'},
                {'3', '6'}
            })));
            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToNumberMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', '9'},
                {'1', '8'},
                {'2', '7'},
                {'3', '6'},
                {'4', '5'},
                {'A', '4'},
                {'6', '3'},
                {'7', '2'},
                {'8', '1'},
                {'9', '0' }
            })));
            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToNumberMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', '9'},
                {'1', '8'},
                {'2', '7'},
                {'3', '6'},
                {'4', '5'},
                {'5', 'A'},
                {'6', '3'},
                {'7', '2'},
                {'8', '1'},
                {'9', '0' }
            })));

            var expected = new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', '9'},
                {'1', '8'},
                {'2', '7'},
                {'3', '6'},
                {'4', '5'},
                {'5', '4'},
                {'6', '3'},
                {'7', '2'},
                {'8', '1'},
                {'9', '0' }
            });

            properties.ChangeNumberToNumberMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', '9'},
                {'1', '8'},
                {'2', '7'},
                {'3', '6'},
                {'4', '5'},
                {'5', '4'},
                {'6', '3'},
                {'7', '2'},
                {'8', '1'},
                {'9', '0' }
            }));

            CollectionAssert.AreEqual(expected.Mappings.ToArray(), properties.NumberToNumberMap.Mappings.ToArray());
        }

        [TestMethod]
        public void Test_ChangeNumberToLetterMap()
        {
            var properties = new ActivationKeyProperties('-', 4, 4);

            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToLetterMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', 'A'},
                {'1', 'B'},
                {'2', 'C'},
                {'3', 'D'}
            })));
            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToLetterMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', 'A'},
                {'1', 'B'},
                {'2', 'C'},
                {'3', 'D'},
                {'4', 'E'},
                {'A', 'F'},
                {'6', 'G'},
                {'7', 'H'},
                {'8', 'I'},
                {'9', 'J' }
            })));
            Assert.ThrowsException<ArgumentException>(() => properties.ChangeNumberToLetterMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', 'A'},
                {'1', 'B'},
                {'2', 'C'},
                {'3', 'D'},
                {'4', 'E'},
                {'5', '5'},
                {'6', 'F'},
                {'7', 'G'},
                {'8', 'H'},
                {'9', 'I' }
            })));

            var expected = new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', 'A'},
                {'1', 'B'},
                {'2', 'C'},
                {'3', 'D'},
                {'4', 'E'},
                {'5', 'F'},
                {'6', 'G'},
                {'7', 'H'},
                {'8', 'I'},
                {'9', 'J' }
            });

            properties.ChangeNumberToLetterMap(new AsciiCharacterMap(new Dictionary<char, char>
            {
                {'0', 'A'},
                {'1', 'B'},
                {'2', 'C'},
                {'3', 'D'},
                {'4', 'E'},
                {'5', 'F'},
                {'6', 'G'},
                {'7', 'H'},
                {'8', 'I'},
                {'9', 'J' }
            }));

            CollectionAssert.AreEqual(expected.Mappings.ToArray(), properties.NumberToLetterMap.Mappings.ToArray());
        }
    }
}
