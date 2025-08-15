using Masasamjant.Configuration;

namespace Masasamjant.Stubs
{
    internal class ConstantDateTimeConfiguration : IDateTimeConfiguration
    {
        private readonly DateTime dateTime;
        private readonly DateTimeOffset dateTimeOffset;
        
        public ConstantDateTimeConfiguration(DateTimeKind kind, DateTime dateTime, DateTimeOffset dateTimeOffset)
        {
            Kind = kind;
            this.dateTime = dateTime;
            this.dateTimeOffset = dateTimeOffset;
        }

        public DateTimeKind Kind { get; }

        public DateTime GetDateTimeNow()
        {
            return dateTime;
        }

        public DateTimeOffset GetDateTimeOffsetNow()
        {
            return dateTimeOffset;
        }
    }
}
