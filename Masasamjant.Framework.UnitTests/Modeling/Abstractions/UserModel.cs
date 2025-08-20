using System.ComponentModel.DataAnnotations;

namespace Masasamjant.Modeling.Abstractions
{
    public class UserModel : Record<Guid>, ISupportPrepareModel, ISupportVersion
    {
        public UserModel(string name)
            : this(Guid.Empty, name)
        { }

        public UserModel(Guid identifier, string name)
        {
            Identifier = identifier;
            Name = name;
            if (!Identifier.Equals(Guid.Empty))
                Version = identifier.ToByteArray();
        }

        public UserModel(Guid identifier, string name, byte[] version)
        {
            Identifier = identifier;
            Name = name;
            Version = version;
        }

        public byte[] Version { get; internal set; } = [];

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of the user is mandatory and cannot be empty string.")]
        public string Name { get; protected set; } = string.Empty;

        public bool IsPrepared { get; protected set; }

        public string GetVersionString()
        {
            if (Version.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(Version).ToUpperInvariant();
        }

        void ISupportPrepareModel.PrepareModel()
        {
            IsPrepared = true;
        }
    }
}
