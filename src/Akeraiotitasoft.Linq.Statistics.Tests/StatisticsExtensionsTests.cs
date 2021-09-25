using Akeraiotitasoft.Linq.Statistics;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Akeraiotitasoft.Linq.Statistics.Tests
{
    public class Tests
    {
        private static List<List<double>> _samples;

        static Tests()
        {
            _samples = new List<List<double>>();
            List<double> sample1 = new List<double>();
            sample1.Add(1);
            sample1.Add(10);
            sample1.Add(2);
            sample1.Add(99);
            sample1.Add(-10);
            sample1.Add(1000);
            sample1.Add(55);
            sample1.Add(5);
            sample1.Add(7);
            _samples.Add(sample1);
            List<double> sample2 = new List<double>();
            sample2.Add(0.34);
            sample2.Add(34.545);
            sample2.Add(234.253);
            sample2.Add(11.24);
            sample2.Add(-1234.12);
            sample2.Add(234234.3);
            sample2.Add(234.2);
            sample2.Add(4534.3);
            sample2.Add(454.3);
            sample2.Add(11.55);
            sample2.Add(93.23);
            _samples.Add(sample2);
            List<double> sample3 = new List<double>();
            _samples.Add(sample3);
            List<double> sample4 = new List<double>();
            sample4.Add(1);
            _samples.Add(sample4);
        }

        // see: http://www.z-table.com/
        [Test]
        [TestCase(0, 0.0, -10.0, false)] // z-score negative infinity
        [TestCase(0, 0.0013, -10.0, false)] // z-score -3.0
        [TestCase(0, 0.0228, -10.0, false)] // z-score -2.0
        [TestCase(0, 0.1587, 1.0, false)] // z-score -1.0
        [TestCase(0, 0.5, 7.0, false)] // z-score 0.0
        [TestCase(0, 0.75, 55.0, false)]
        [TestCase(0, 0.8413, 99.0, false)] // z-score 1.0
        [TestCase(0, 0.90, 1000.0, false)]
        [TestCase(0, 0.95, 1000.0, false)]
        [TestCase(0, 0.9772, 1000.0, false)] // z-score 2.0
        [TestCase(0, 0.9987, 1000.0, false)] // z-score 3.0
        [TestCase(0, 1.0, 1000.0, false)] // z-score positive infinity
        [TestCase(1, 0.0, -1234.12, false)] // z-score negative infinity
        [TestCase(1, 0.0013, -1234.12, false)] // z-score -3.0
        [TestCase(1, 0.0228, -1234.12, false)] // z-score -2.0
        [TestCase(1, 0.1587, 0.34, false)] // z-score -1.0
        [TestCase(1, 0.5, 93.23, false)] // z-score 0.0
        [TestCase(1, 0.75, 454.3, false)]
        [TestCase(1, 0.8413, 4534.3, false)] // z-score 1.0
        [TestCase(1, 0.90, 4534.3, false)]
        [TestCase(1, 0.95, 234234.3, false)]
        [TestCase(1, 0.9772, 234234.3, false)] // z-score 2.0
        [TestCase(1, 0.9987, 234234.3, false)] // z-score 3.0
        [TestCase(1, 1.0, 234234.3, false)] // z-score positive infinity
        [TestCase(2, 0.0, 0.0, true)] // z-score negative infinity
        [TestCase(2, 0.5, 0.0, true)] // z-score 0.0
        [TestCase(2, 1.0, 0.0, true)] // z-score positive infinity
        [TestCase(3, 0.0, 1.0, false)] // z-score negative infinity
        [TestCase(3, 0.5, 1.0, false)] // z-score 0.0
        [TestCase(3, 1.0, 1.0, false)] // z-score positive infinity
        public void InversePercentileTests(int index, double percentile, double expectedResult, bool hasException)
        {
            List<double> dataSet = _samples[index];
            if (hasException)
            {
                var exception = Assert.Throws<InvalidOperationException>(() => dataSet.InversePercentile(x => x, percentile));
                Assert.IsNotNull(exception);
                Assert.AreEqual("Sequence contains no elements", exception.Message);
            }
            else
            {
                double result = dataSet.InversePercentile(x => x, percentile);
                Assert.AreEqual(expectedResult, result);
            }
        }

        // see: https://www.calculatorsoup.com/calculators/statistics/statistics.php
        [Test]
        [TestCase(0, 309.34786045071024d, false)] // copied from assert exception, calculator soup gave: 309.34786
        [TestCase(1, 67225.747588020109d, false)] // copied from assert exception, calculator soup gave: 67225.748
        [TestCase(2, 0.0, true)]
        [TestCase(3, 0.0, false)]
        public void StandardDeviationTests(int index, double expectedResult, bool hasException)
        {
            List<double> dataSet = _samples[index];
            if (hasException)
            {
                var exception = Assert.Throws<InvalidOperationException>(() => dataSet.StandardDeviation(x => x));
                Assert.IsNotNull(exception);
                Assert.AreEqual("Sequence contains no elements", exception.Message);
            }
            else
            {
                double result = dataSet.StandardDeviation(x => x);
                Assert.AreEqual(expectedResult, result);
            }
        }

        // see: https://www.calculatorsoup.com/calculators/statistics/statistics.php
        [Test]
        [TestCase(0, 328.11295480537052d, false)] // copied from assert exception, calculator soup gave: 328.11295
        [TestCase(1, 70506.958895168718d, false)] // copied from assert exception, calculator soup gave: 70506.959
        [TestCase(2, 0.0, true)]
        [TestCase(3, 0.0, false)]
        public void SampleStandardDeviationTests(int index, double expectedResult, bool hasException)
        {
            List<double> dataSet = _samples[index];
            if (hasException)
            {
                var exception = Assert.Throws<InvalidOperationException>(() => dataSet.SampleStandardDeviation(x => x));
                Assert.IsNotNull(exception);
                Assert.AreEqual("Sequence contains no elements", exception.Message);
            }
            else
            {
                double result = dataSet.SampleStandardDeviation(x => x);
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}