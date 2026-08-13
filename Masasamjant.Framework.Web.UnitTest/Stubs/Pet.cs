using System.Text.Json.Serialization;

namespace Masasamjant.Web.Stubs
{
    public class Pet
    {
        [JsonInclude]
        public string Name { get; set; } = string.Empty;

        [JsonInclude]
        public int Age { get; set; } = 0;
    }
}
