using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

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
            switch (hofName)
            {
                case "select":
                    return source.Select(predicate);
                case "where":
                    return source.Where(predicate);
                default:
                    throw new Exception($"Argument {hofName} unrecognised");
            }
        }

        private static IEnumerable<TOut> TakeWhile<TIn, TOut>(
            this TIn source,
            Func<TIn, TOut> map,
            Func<TIn, TOut, bool> stopCondition)
        {
            TOut result;

            do
            {
                result = map(source);

                yield return result;
            }
            while (!stopCondition(source, result));
        }
    }
}