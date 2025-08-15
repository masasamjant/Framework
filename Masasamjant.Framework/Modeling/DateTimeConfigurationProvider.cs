using Masasamjant.Configuration;
using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents provider of specified <see cref="IDateTimeConfiguration"/> configuration to models. 
    /// This class is used at classes based on <see cref="Model"/> class.
    /// </summary>
    public sealed class DateTimeConfigurationProvider
    {
        private readonly IDateTimeConfiguration configuration;

        /// <summary>
        /// Initializes new instance of the <see cref="DateTimeConfigurationProvider"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/> to provide.</param>
        public DateTimeConfigurationProvider(IDateTimeConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the provided <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        public IDateTimeConfiguration Configuration
        {
            get { return configuration; }
        }

        /// <summary>
        /// Gets the current <see cref="DateTimeConfigurationProvider"/> instance. By default 
        /// gets provider that provides <see cref="DateTimeConfiguration.Local"/> configuration.
        /// </summary>
        public static DateTimeConfigurationProvider Current
        {
            get { return current; }
        }

        /// <summary>
        /// Change the provider returned from <see cref="Current"/>. This method should be used to change the <see cref="Current"/> when 
        /// application starts to use correct <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public static void ChangeCurrent(IDateTimeConfiguration configuration)
        {
            current = new DateTimeConfigurationProvider(configuration);
        }

        private static DateTimeConfigurationProvider current = new DateTimeConfigurationProvider(DateTimeConfiguration.Local);
    }
}
