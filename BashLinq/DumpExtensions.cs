using System;
using System.Collections.Generic;
using System.Linq;

namespace BashLinq
{
    public static class DumpExtensions
    {
        public static void Dump<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> map)
        {
            foreach (var result in source.Select(map))
                result.Dump();
        }

        public static void Dump<T>(this IEnumerable<T> source) => source.Dump(i => i);

        public static void Dump<T>(this T obj) => Console.WriteLine(obj);
    }
}