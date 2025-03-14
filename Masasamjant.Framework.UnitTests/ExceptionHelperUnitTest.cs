namespace Masasamjant
{
    [TestClass]
    public class ExceptionHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetAll_No_Inner_Exceptions()
        {
            var exception = new Exception("Exception", null);
            var chain = ExceptionHelper.GetAll(exception);
            Assert.IsTrue(chain.Count() == 1);
            Assert.IsTrue(ReferenceEquals(exception, chain.First()));
        }

        [TestMethod]
        public void Test_GetAll_With_Inner_Exceptions()
        {
            var first = new Exception("First", null);
            var second = new Exception("Second", first);
            var third = new Exception("Third", second);
            var chain = ExceptionHelper.GetAll(third);
            Assert.IsTrue(chain.Count() == 3);
            var items = chain.ToArray();
            Assert.IsTrue(items[0].Message == "Third");
            Assert.IsTrue(items[1].Message == "Second");
            Assert.IsTrue(items[2].Message == "First");
        }

        [TestMethod]
        public void Test_GetFirst()
        {
            var first = new Exception("First", null);
            var second = new Exception("Second", first);
            var third = new Exception("Third", second);
            var result = ExceptionHelper.GetFirst(third);
            Assert.IsTrue(ReferenceEquals(first, result));
        }

        [TestMethod]
        public void Test_ToInvalidOperationException_Without_Message()
        {
            var aggregateException = new AggregateException("Aggregate exception", new[]
            {
                new Exception("First", new Exception("Second")),
            });

            var exception = ExceptionHelper.ToInvalidOperationException(aggregateException);
            Assert.AreEqual("Aggregate exception (First)", exception.Message);
            Assert.AreEqual("First", exception.InnerException?.Message);
            Assert.AreEqual("Second", exception?.InnerException?.InnerException?.Message);

            aggregateException = new AggregateException("Aggregate exception", new[]
            {
                new Exception("First"),
                new Exception("Second")
            });

            exception = ExceptionHelper.ToInvalidOperationException(aggregateException);
            Assert.AreEqual("Aggregate exception (First) (Second)", exception.Message);
            Assert.AreEqual("First", exception.InnerException?.Message);
            Assert.AreEqual("Second", exception?.InnerException?.InnerException?.Message);
        }

        [TestMethod]
        public void Test_ToInvalidOperationException_With_Message()
        {
            var aggregateException = new AggregateException("Aggregate exception", new[]
            {
                new Exception("First", new Exception("Second")),
            });

            var exception = ExceptionHelper.ToInvalidOperationException(aggregateException, "One inner exception with inner exception.");
            Assert.AreEqual("One inner exception with inner exception.", exception.Message);
            Assert.AreEqual("First", exception.InnerException?.Message);
            Assert.AreEqual("Second", exception?.InnerException?.InnerException?.Message);

            aggregateException = new AggregateException("Aggregate exception", new[]
            {
                new Exception("First"),
                new Exception("Second")
            });

            exception = ExceptionHelper.ToInvalidOperationException(aggregateException, "Two inner exceptions.");
            Assert.AreEqual("Two inner exceptions.", exception.Message);
            Assert.AreEqual("First", exception.InnerException?.Message);
            Assert.AreEqual("Second", exception?.InnerException?.InnerException?.Message);
        }
    }
}
