using System;
using System.Collections.Generic;
using System.Linq;

namespace Akeraiotitasoft.Linq.Statistics
{
    /// <summary>
    /// Statistic <see cref="IEnumerable{T}"/> extensions.
    /// </summary>
    public static class StatisticsExtensions
    {
        private const double ZEROTH_PERCENTILE = 0.0;
        private const double HUNDREDTH_PERCENTILE = 1.0; // 1.0 == 100%

        /// <summary>
        /// Calculates the actual value at or just below a percentile.
        /// </summary>
        /// <typeparam name="T">The enumerable item type</typeparam>
        /// <param name="enumerable">The enumerable object</param>
        /// <param name="selector">selects the value per item</param>
        /// <param name="percentile">The percentile</param>
        /// <returns></returns>
        public static double InversePercentile<T>(this IEnumerable<T> enumerable, Func<T, double> selector, double percentile)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "enumerable cannt be null");
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector), "selector cannt be null");
            }
            if (percentile < ZEROTH_PERCENTILE || percentile > HUNDREDTH_PERCENTILE)
            {
                throw new ArgumentException("percentile is out of range", nameof(percentile));
            }

            List<double> values = new List<double>();
            foreach (var item in enumerable)
            {
                values.Add(selector(item));
            }
            values.Sort();
            if (values.Count == 0)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
            int index = (int)Math.Floor(values.Count * percentile);
            return values[Math.Min(index, values.Count - 1)];
        }

        /// <summary>
        /// Calculate the population standard deviation
        /// </summary>
        /// <typeparam name="T">The enumerable item type</typeparam>
        /// <param name="enumerable">The enumerable object</param>
        /// <param name="selector">The selector for the values to use</param>
        /// <returns>The calculated standard deviation of the data set</returns>
        public static double StandardDeviation<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "enumerable cannt be null");
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector), "selector cannt be null");
            }
            List<double> values = enumerable.Select(x => selector(x)).ToList();
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            return Math.Sqrt(sumOfSquaresOfDifferences / values.Count);
        }

        /// <summary>
        /// Calculate the sample standard deviation
        /// </summary>
        /// <typeparam name="T">The enumerable item type</typeparam>
        /// <param name="enumerable">The enumerable object</param>
        /// <param name="selector">The selector for the values to use</param>
        /// <returns>The calculated standard deviation of the data set</returns>
        public static double SampleStandardDeviation<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "enumerable cannt be null");
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector), "selector cannt be null");
            }
            List<double> values = enumerable.Select(x => selector(x)).ToList();
            if (values.Count == 1)
            {
                return 0.0;
            }
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            return Math.Sqrt(sumOfSquaresOfDifferences / (values.Count - 1));
        }
    }
}
