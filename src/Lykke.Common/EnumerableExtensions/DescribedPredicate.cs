// Copyright (c) 2023 Lykke Corp.
// See the LICENSE file in the project root for more information.

using System;

namespace Lykke.Common.ListExtensions
{
    /// <summary>
    /// Wrapper for predicate with description
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DescribedPredicate<T>
    {
        private readonly Func<T, bool> _predicate;
        private readonly string _description;

        public DescribedPredicate(Func<T, bool> predicate, string description)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description must not be null or empty.", nameof(description));
            }
            _description = description;
        }

        public bool Invoke(T arg) => _predicate(arg);

        public override string ToString()
        {
            return _description;
        }
    }
}
