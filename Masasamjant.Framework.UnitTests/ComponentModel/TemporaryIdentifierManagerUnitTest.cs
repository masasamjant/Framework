namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class TemporaryIdentifierManagerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetTemporaryIdentifier()
        {
            var manager = new TemporaryIdentifierManager();
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
            var manager = new TemporaryIdentifierManager();
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
            Assert.IsFalse(manager.TryGetIdentifier("Scope", temporaryIdentifier, out actualIdentifiers));
        }

        [TestMethod]
        public void Test_RemoveIdentifiers()
        {
            var manager = new TemporaryIdentifierManager();
            var identifier = Guid.NewGuid();
            var temporaryIdentifier = manager.GetTemporaryIdentifier("Scope", identifier);
            manager.RemoveIdentifiers("Scope");
            Assert.IsFalse(manager.TryGetIdentifier("Scope", temporaryIdentifier, out Guid actual));
            Assert.AreEqual(Guid.Empty, actual);
        }
    }
}
