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
            bool upperCase;
            List<Guid> values;

            do
            {
                format = "";
                count = 0;
                upperCase = false;

                while (!formats.ContainsKey(format))
                    format = AskFormat();

                Console.WriteLine();

                while (count < 1)
                    count = AskCount();

                values = new List<Guid>(count);
                generator.GenerateValues(values, count);

                Console.WriteLine();
                Console.WriteLine("Create upper case letters? (Y/N)");
                upperCase = Console.ReadLine()?.ToLowerInvariant() == "y";
                Console.WriteLine();

                foreach (var value in values)
                    Console.WriteLine(upperCase ? value.ToString(format).ToUpperInvariant() : value.ToString(format));

                Console.WriteLine();
                Console.WriteLine("Create more? (Y/N)");
                exit = Console.ReadLine()?.ToLowerInvariant() == "n";
            }
            while (!exit);
        }

        static string AskFormat()
        {
            Console.WriteLine("Select GUID format?");
            foreach (var keyValue in formats)
                Console.WriteLine($"{keyValue.Key}={keyValue.Value}");
            return Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
        }

        static int AskCount()
        {
            Console.WriteLine("How many GUID to create?");
            var line = Console.ReadLine();
            if (!int.TryParse(line, out int count)) count = 0;
            return count;
        }
    }
}
