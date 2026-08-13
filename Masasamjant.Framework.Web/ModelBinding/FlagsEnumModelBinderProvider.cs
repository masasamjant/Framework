using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Masasamjant.Web.ModelBinding
{
    /// <summary>
    /// Represents provider of <see cref="FlagsEnumModelBinder"/> model binder.
    /// </summary>
    public sealed class FlagsEnumModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates <see cref="FlagsEnumModelBinder"/> model binder, if <see cref="ModelMetadata.UnderlyingOrModelType"/> is enumeration with <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="context">The context for model binder provider.</param>
        /// <returns>The created <see cref="FlagsEnumModelBinder"/> model binder, or <c>null</c> if the type is not a flags enumeration.</returns>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.IsEnum && EnumHelper.IsFlagsEnum(context.Metadata.UnderlyingOrModelType))
                return new FlagsEnumModelBinder(context.Metadata.UnderlyingOrModelType);
            
            return null;
        }
    }
}
