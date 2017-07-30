using System;
using System.Collections.Generic;

namespace BashLinq
{
    public static class FunctionalExtensions
    {
        public static IEnumerable<TOut> TakeWhile<TIn, TOut>(
            this TIn source,
            Func<TIn, TOut> map,
            Func<TIn, TOut, bool> continueCondition)
        {
            TOut result;

            do
            {
                result = map(source);

                yield return result;
            }
            while (continueCondition(source, result));
        }
    }
}