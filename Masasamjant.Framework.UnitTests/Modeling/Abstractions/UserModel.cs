using System.ComponentModel.DataAnnotations;

namespace Masasamjant.Modeling.Abstractions
{
    public class UserModel : Record, ISupportPrepareModel
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of the user is mandatory and cannot be empty string.")]
        public string Name { get; protected set; } = string.Empty;

        public Guid Identifier { get; protected set; }

        public bool IsPrepared { get; protected set; }

        protected override object[] GetKeyProperties()
        {
            if (Identifier.Equals(Guid.Empty))
                return [];

            return [Identifier];
        }

        void ISupportPrepareModel.PrepareModel()
        {
            IsPrepared = true;
        }
    }
}
