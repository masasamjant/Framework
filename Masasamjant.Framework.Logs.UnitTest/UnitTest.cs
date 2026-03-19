namespace Masasamjant.Diagnostics
{
    public abstract class UnitTest
    {
        protected static void AssertLine(string line, string[] expectedContents)
        {
            foreach (var expectedContent in expectedContents)
                Assert.Contains(expectedContent, line);
        }
    }
}
