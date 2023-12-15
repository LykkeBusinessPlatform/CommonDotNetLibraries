// Copyright (c) 2023 Lykke Corp.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Common.ListExtensions;

namespace Lykke.Common.EnumerableExtensions
{
    /// <summary>
    /// Extensions methods for <see cref="IEnumerable{T}"/> which duplicate
    /// functionality of Single methods with a description in case of failure
    /// </summary>
    public static class SingleExtensions
    {
        /// <summary>
        /// Single element from the sequence matching the predicate
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <param name="predicate">The predicate with a description</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// When the sequence contains no matching element or more than one matching element,
        /// contains description of the predicate.
        /// </exception>
        public static T Single<T>(this IEnumerable<T> source, DescribedPredicate<T> predicate)
        {
            var matchingElements = source.Where(predicate.Invoke).Take(2).ToList();

            return matchingElements.Count switch
            {
                0 => throw new InvalidOperationException($"Sequence contains no matching element. Predicate: {predicate}"),
                1 => matchingElements[0],
                _ => throw new InvalidOperationException($"Sequence contains more than one matching element. Predicate: {predicate}")
            };
        }

        /// <summary>
        /// Single element from the sequence
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <param name="description">The description in case of failure</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// When the sequence contains no elements or more than one element,
        /// contains description.
        /// </exception>
        public static T Single<T>(this IEnumerable<T> source, string description) =>
            source.Single(new DescribedPredicate<T>(_ => true, description));
    }
}
