using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents base <see cref="Type"/> for reflection types.
    /// </summary>
    public abstract class ReflectionType : Type
    {
        private readonly Type type;

        /// <summary>
        /// Initializes new instance of the <see cref="ReflectionType"/> class.
        /// </summary>
        /// <param name="type">The actual type.</param>
        /// <exception cref="ArgumentException">If <paramref name="type"/> is or inherits <see cref="ReflectionType"/>.</exception>
        protected ReflectionType(Type type)
        {
            if (type is ReflectionType || (type.BaseType != null && type.BaseType.Equals(typeof(ReflectionType))))
                throw new ArgumentException("The type cannot be 'ReflectionType'.", nameof(type));

            this.type = type;
        }

        /// <summary>
        /// Gets the assembly where type is declared.
        /// </summary>
        public override Assembly Assembly => type.Assembly;

        /// <summary>
        /// Gets the assembly qualified name.
        /// </summary>
        public override string? AssemblyQualifiedName => type.AssemblyQualifiedName;

        /// <summary>
        /// Gets the base type from which type inherits or <c>null</c> if reprsents <see cref="object"/> or interface.
        /// </summary>
        public override Type? BaseType => type.BaseType;

        /// <summary>
        /// Gets the full name including namespace, but not assembly.
        /// </summary>
        public override string? FullName => type.FullName;

        /// <summary>
        /// Gets the GUID associated with type.
        /// </summary>
        public override Guid GUID => type.GUID;

        /// <summary>
        /// Gets the module where type is defined.
        /// </summary>
        public override Module Module => type.Module;

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        public override string? Namespace => type.Namespace;

        /// <summary>
        /// Gets the underlying system type.
        /// </summary>
        public override Type UnderlyingSystemType => type.UnderlyingSystemType;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => type.Name;

        /// <summary>
        /// Gets the actual type.
        /// </summary>
        /// <returns>A actual type.</returns>
        public Type GetActualType() => type;

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return type.GetConstructors(bindingAttr);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return type.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return type.GetCustomAttributes(attributeType, inherit);
        }

        public override Type? GetElementType()
        {
            return type.GetElementType();
        }

        public override EventInfo? GetEvent(string name, BindingFlags bindingAttr)
        {
            return type.GetEvent(name, bindingAttr);
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return type.GetEvents(bindingAttr);
        }

        public override FieldInfo? GetField(string name, BindingFlags bindingAttr)
        {
            return type.GetField(name, bindingAttr);
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return type.GetFields(bindingAttr);
        }

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
        public override Type? GetInterface(string name, bool ignoreCase)
        {
            return type.GetInterface(name, ignoreCase);
        }

        public override Type[] GetInterfaces()
        {
            return type.GetInterfaces();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return type.GetMembers(bindingAttr);
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return type.GetMethods(bindingAttr);
        }

        public override Type? GetNestedType(string name, BindingFlags bindingAttr)
        {
            return type.GetNestedType(name, bindingAttr);
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return type.GetNestedTypes(bindingAttr);
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return type.GetProperties(bindingAttr);
        }

        public override object? InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? namedParameters)
        {
            return type.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return type.IsDefined(attributeType, inherit);
        }

        #region Not implemented

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo? GetConstructorImpl(BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[] types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override MethodInfo? GetMethodImpl(string name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[]? types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override PropertyInfo? GetPropertyImpl(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[]? types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
