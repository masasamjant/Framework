using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Runtime.ExceptionServices;

namespace Masasamjant.Web.Binding
{
    /// <summary>
    /// Represents model binder to find enumerations with <see cref="FlagsAttribute"/> attribute.
    /// </summary>
    public sealed class FlagsEnumBinder : IModelBinder
    {
        private readonly Type enumType;

        /// <summary>
        /// Initializes new instance of the <see cref="FlagsEnumBinder"/> class.
        /// </summary>
        /// <param name="enumType">The type of enumeration with <see cref="FlagsAttribute"/>.</param>
        /// <exception cref="ArgumentException">If <paramref name="enumType"/> is not enumeration type with <see cref="FlagsAttribute"/>.</exception>
        public FlagsEnumBinder(Type enumType)
        {
            if (!EnumHelper.IsFlagsEnum(enumType))
                throw new ArgumentException("The type is not flags enumeration.", nameof(enumType));

            this.enumType = enumType;
        }

        /// <summary>
        /// Attemps to bind model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="ModelBindingContext"/>.</param>
        /// <returns>A task of binding process.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            if (!EnumHelper.IsFlagsEnum(bindingContext.ModelType))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, bindingContext.ModelMetadata.ModelBindingMessageProvider.ValueIsInvalidAccessor(valueProviderResult.ToString()));
            }
            else
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
                        exception = ExceptionDispatchInfo.Capture(exception.InnerException).SourceException;

                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, exception, bindingContext.ModelMetadata);
                }
            }

            return Task.CompletedTask;
        }
    }
}
