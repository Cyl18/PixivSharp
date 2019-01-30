using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PixivSharp
{
    [AttributeUsage(AttributeTargets.All)]
    public class SymbolAttribute : Attribute
    {
        public string Symbol { get; }

        public SymbolAttribute(string symbol)
        {
            Symbol = symbol;
        }
    }

    public static class SymbolExtensions
    {
        private static readonly Dictionary<object, string> Cache = new Dictionary<object, string>(20);
        public static string GetSymbol<T>(this T obj)
        {
            if (Cache.ContainsKey(obj)) return Cache[obj];

            var symbol = typeof(T).GetMember(obj.ToString()).First().GetCustomAttribute<SymbolAttribute>().Symbol;
            Cache[obj] = symbol;
            return symbol;
        }
    }
}
