using Masasamjant.Applications;

namespace Masasamjant.GuidGeneratorApp
{
    internal class Program
    {
        static readonly GuidGenerator generator = new GuidGenerator();

        static readonly Dictionary<string, string> formats = new Dictionary<string, string>()
        {
            { GuidFormat.N, GuidFormat.GetFormatExample(GuidFormat.N) },
            { GuidFormat.D, GuidFormat.GetFormatExample(GuidFormat.D) },
            { GuidFormat.B, GuidFormat.GetFormatExample(GuidFormat.B) },
            { GuidFormat.P, GuidFormat.GetFormatExample(GuidFormat.P) },
            { GuidFormat.X, GuidFormat.GetFormatExample(GuidFormat.X) }
        };

        static void Main(string[] args)
        {
            bool exit;
            int count;
            string format;
            bool? upperCase;
            List<Guid> values;

            var arguments = ArgumentsReader.Read(args);

            do
            {
                format = arguments.TryGetValue("format", out var arg) ? arg : string.Empty; 
                count = arguments.TryGetValue("count", out arg) && int.TryParse(arg, out var result) && result >= 0 ? result : 0;
                upperCase = arguments.Contains("upper") ? true : null;

                while (!formats.ContainsKey(format))
                    format = AskFormat();

                Console.WriteLine();

                while (count < 1)
                    count = AskCount();

                values = new List<Guid>(count);
                generator.GenerateValues(values, count);

                Console.WriteLine();
                
                if (!upperCase.HasValue)
                {
                    Console.WriteLine(MessageResource.UpperCaseMessage);
                    upperCase = string.Equals(Console.ReadLine(), MessageResource.YesAnswer, StringComparison.OrdinalIgnoreCase);
                    Console.WriteLine();
                }

                foreach (var value in values)
                    Console.WriteLine(upperCase == true ? value.ToString(format).ToUpperInvariant() : value.ToString(format));

                Console.WriteLine();
                Console.WriteLine(MessageResource.MoreMessage);
                exit = string.Equals(Console.ReadLine(), MessageResource.NoAnswer, StringComparison.OrdinalIgnoreCase);
            }
            while (!exit);
        }

        static string AskFormat()
        {
            Console.WriteLine(MessageResource.FormatMessage);
            foreach (var keyValue in formats)
                Console.WriteLine($"{keyValue.Key}={keyValue.Value}");
            return Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
        }

        static int AskCount()
        {
            Console.WriteLine(MessageResource.CountMessage);
            var line = Console.ReadLine();
            if (!int.TryParse(line, out int count)) count = 0;
            return count;
        }
    }
}
