namespace Masasamjant.Diagnostics
{
    internal class DefaultFileWriter : IFileWriter
    {
        public Task AppendAllLinesAsync(string filePath, IEnumerable<string> lines)
        {
            return File.AppendAllLinesAsync(filePath, lines);
        }
    }
}
