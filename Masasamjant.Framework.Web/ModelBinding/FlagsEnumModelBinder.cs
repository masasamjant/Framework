using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace Masasamjant.Web.ModelBinding
{
    /// <summary>
    /// Represents <see cref="IModelBinder"/> to bind enumerations with <see cref="FlagsAttribute"/> attribute.
    /// </summary>
    public sealed class FlagsEnumModelBinder : IModelBinder
    {
        private readonly Type enumType;

        /// <summary>
        /// Initializes new instance of the <see cref="FlagsEnumModelBinder"/> class.
        /// </summary>
        /// <param name="enumType">The type of the enumeration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="enumType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="enumType"/> is not a flags enumeration.</exception>
        public FlagsEnumModelBinder(Type enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);

            if (!EnumHelper.IsFlagsEnum(enumType))
                throw new ArgumentException("The type is not flags enumeration.", nameof(enumType));
        
            this.enumType = enumType;
        }

        /// <summary>
        /// Attempts to bind model.
        /// </summary>
        /// <param name="bindingContext">The context for model binding.</param>
        /// <returns>A task that represents the asynchronous binding operation.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            if (EnumHelper.IsFlagsEnum(bindingContext.ModelType))
            {
                try
                {
                    var underlyingType = Enum.GetUnderlyingType(bindingContext.ModelType);
                    var value = Activator.CreateInstance(underlyingType) ?? throw new InvalidOperationException($"Could not create instance of '{underlyingType}'.");
                    var converter = TypeDescriptor.GetConverter(enumType);

                    foreach (var resultValue in valueProviderResult)
                    {
                        var modelValue = converter.ConvertFrom(null, valueProviderResult.Culture, value);
                        if (modelValue == null)
                            continue;
                        value = EnumHelper.AppendFlag(bindingContext.ModelType, value, modelValue);
                    }

                    var enumValue = EnumHelper.ConvertToEnum(bindingContext.ModelType, value);
                    bindingContext.Result = ModelBindingResult.Success(enumValue);
                }
                catch (Exception exception)
                {
                    if (exception is not FormatException && exception.InnerException != null)
                        exception = ExceptionHelper.GetFirst(exception);

                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, exception, bindingContext.ModelMetadata);
                }
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, bindingContext.ModelMetadata.ModelBindingMessageProvider.ValueIsInvalidAccessor(valueProviderResult.ToString()));
            }

            return Task.CompletedTask;
        }
    }
}
