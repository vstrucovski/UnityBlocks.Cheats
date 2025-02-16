using System;

namespace UnityBlocks.Cheats
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CheatCommandAttribute : Attribute
    {
        public string Category { get; }
        public object Parameter { get; }

        public CheatCommandAttribute(object parameter, string category = default)
        {
            Category = category;
            Parameter = parameter;
        }
    }
}