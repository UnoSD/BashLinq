using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace BashLinq
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            new StreamReader(Console.OpenStandardInput())
                .TakeWhile(s => s.ReadLine(), (s, l) => !s.EndOfStream)
                .AsQueryable()
                .ApplyHighOrderFunction(args.First().ToLowerInvariant(), args.Skip(1).Single())
                .ToDynamicList()
                .ForEach(Console.WriteLine);

        private static IQueryable ApplyHighOrderFunction(
            this IQueryable<string> source,
            string hofName,
            string predicate)
        {
            var methodInfos = typeof(DynamicQueryableExtensions)
                                .GetMethods()
                                .Where(m => m.GetParameters().MatchDesiredInput() && !m.IsGenericMethod)
                                .ToArray();

            var methodInfo = methodInfos.SingleOrDefault(m => m.Name.ToLowerInvariant() == hofName) ??
                                         throw new Exception($"Argument {hofName} unrecognised");

            return (IQueryable)methodInfo.Invoke(null, new object[] { source, predicate, new object[0] });
        }

        private static bool MatchDesiredInput(this IReadOnlyCollection<ParameterInfo> parameters)
        {
            if (parameters.Count != 3)
                return false;

            if (parameters.First().ParameterType != typeof(IQueryable))
                return false;

            if (parameters.Skip(1).First().ParameterType != typeof(string))
                return false;

            return parameters.Last().ParameterType == typeof(object[]);
        }
    }
}