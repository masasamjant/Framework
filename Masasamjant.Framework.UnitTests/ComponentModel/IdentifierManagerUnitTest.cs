namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class IdentifierManagerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetTemporaryIdentifier()
        {
            var manager = new IdentifierManager();
            var identifier = Guid.NewGuid();
            var temporaryIdentifier = manager.GetTemporaryIdentifier("Scope", identifier);
            Assert.AreNotEqual(temporaryIdentifier, identifier.ToString());
            var temporaryIdentifier2 = manager.GetTemporaryIdentifier("Scope", identifier);
            Assert.AreEqual(temporaryIdentifier, temporaryIdentifier2);
            var identifiers = new object[] 
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            temporaryIdentifier2 = manager.GetTemporaryIdentifier("Scope", identifiers);
            Assert.AreNotEqual(temporaryIdentifier, temporaryIdentifier2);
        }

        [TestMethod]
        public void Test_TryGetIdentifier()
        {
            var manager = new IdentifierManager();
            var identifier = Guid.NewGuid();
            var temporaryIdentifier = manager.GetTemporaryIdentifier("Scope", identifier);
            Assert.IsTrue(manager.TryGetIdentifier("Scope", temporaryIdentifier, out Guid actual));
            Assert.AreEqual(identifier, actual);
            var identifiers = new object[]
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            temporaryIdentifier = manager.GetTemporaryIdentifier("Scope", identifiers);
            Assert.IsTrue(manager.TryGetIdentifier("Scope", temporaryIdentifier, out object[] actualIdentifiers));
            CollectionAssert.AreEqual(identifiers, actualIdentifiers);
            Assert.IsFalse(manager.TryGetIdentifier("Foo", temporaryIdentifier, out actual));
            Assert.AreEqual(Guid.Empty, actual);
        }

        [TestMethod]
        public void Test_RemoveIdentifiers()
        {
            var manager = new IdentifierManager();
            var identifier = Guid.NewGuid();
            var temporaryIdentifier = manager.GetTemporaryIdentifier("Scope", identifier);
            Assert.IsTrue(manager.TryGetIdentifier("Scope", temporaryIdentifier, out Guid actual));
            manager.RemoveIdentifiers("Scope");
            Assert.IsFalse(manager.TryGetIdentifier("Scope", temporaryIdentifier, out actual));
            Assert.AreEqual(Guid.Empty, actual);
        }
    }
}
