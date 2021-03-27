using System;

namespace TinyCsvParser.TypeConverter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class UserTypeAttribute : Attribute
    {
        public int ConstructorArgs { get; }

        public UserTypeAttribute(int constructorArgs)
        {
            this.ConstructorArgs = constructorArgs;
        }
    }
}