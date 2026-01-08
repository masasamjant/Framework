namespace Masasamjant
{
    [TestClass]
    public class CharacterMappingExceptionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructors()
        {
            var exception = new CharacterMappingException('A', 'B', "Message");
            Assert.AreEqual('A', exception.SourceCharacter);
            Assert.AreEqual('B', exception.DestinationCharacter);
            Assert.AreEqual("Message", exception.Message);
            Assert.IsNull(exception.InnerException);

            var inner = new InvalidOperationException();
            exception = new CharacterMappingException('A', 'B', "Message", inner);
            Assert.AreEqual('A', exception.SourceCharacter);
            Assert.AreEqual('B', exception.DestinationCharacter);
            Assert.AreEqual("Message", exception.Message);
            Assert.AreSame(inner, exception.InnerException);
        }
    }
}
