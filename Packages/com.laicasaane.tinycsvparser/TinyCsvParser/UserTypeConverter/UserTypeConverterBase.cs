using System;
using System.Reflection;

namespace TinyCsvParser.TypeConverter
{
    public abstract class UserTypeConverterBase<TTargetType> : IUserTypeConverter<TTargetType>
    {
        public Type TargetType => typeof(TTargetType);

        public int ConstructorArgs { get; }

        private readonly ITypeConverterProvider converterProvider;

        protected UserTypeConverterBase(ITypeConverterProvider converterProvider)
        {
            this.converterProvider = converterProvider;

            var attribute = typeof(TTargetType).GetCustomAttribute<UserTypeAttribute>();
            this.ConstructorArgs = attribute?.ConstructorArgs ?? 0;
        }

        protected UserTypeConverterBase(int constructorArgs, ITypeConverterProvider converterProvider)
        {
            this.ConstructorArgs = constructorArgs;
            this.converterProvider = converterProvider;
        }

        public abstract bool TryConvert(in ReadOnlySpan<string> values, out TTargetType result);

        protected ITypeConverter<T> GetConverter<T>()
            => this.converterProvider.Resolve<T>();

        protected IArrayTypeConverter<T> GetCollectionConverter<T>()
            => this.converterProvider.ResolveCollection<T>();
    }
}