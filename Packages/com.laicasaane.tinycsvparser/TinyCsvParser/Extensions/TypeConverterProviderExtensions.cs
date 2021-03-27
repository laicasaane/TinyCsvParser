using System;

namespace TinyCsvParser.TypeConverter
{
    public static class TypeConverterProviderExtensions
    {
        public static TypeConverterProvider AddEnumConverters<T>(this TypeConverterProvider self) where T : struct, Enum
        {
            self.Add(new EnumConverter<T>());
            self.Add(new ArrayConverter<T>(new EnumConverter<T>()));
            self.Add(new NullableEnumConverter<T>());
            self.Add(new ArrayConverter<T?>(new NullableEnumConverter<T>()));

            return self;
        }

        public static TypeConverterProvider AddUserTypeConverters<T>(this TypeConverterProvider self, IUserTypeConverter<T> converter)
        {
            self.Add(converter);
            self.Add(new UserTypeArrayConverter<T>(converter));

            return self;
        }

        public static TypeConverterProvider AddValueTypeConverters<T>(this TypeConverterProvider self, UserTypeConverter<T> converter)
            where T : struct
        {
            self.AddUserTypeConverters<T>(converter);

            var type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return self;

            self.Add(new NullableUserTypeConverter<T>(converter));
            self.Add(new UserTypeArrayConverter<T?>(new NullableUserTypeConverter<T>(converter)));

            return self;
        }
    }
}
