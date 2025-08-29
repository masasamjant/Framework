using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Masasamjant.Web.Binding
{
    /// <summary>
    /// Represents provider of <see cref="FlagsEnumBinder"/> model binder.
    /// </summary>
    public sealed class FlagsEnumBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates <see cref="FlagsEnumBinder"/> model binder, if <see cref="ModelMetadata.UnderlyingOrModelType"/> is enumeration with <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="context">The <see cref="ModelBinderProviderContext"/>.</param>
        /// <returns>A <see cref="FlagsEnumBinder"/> or <c>null</c>.</returns>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.IsEnum && EnumHelper.IsFlagsEnum(context.Metadata.UnderlyingOrModelType))
                return new FlagsEnumBinder(context.Metadata.UnderlyingOrModelType);

            return null;
        }
    }
}
