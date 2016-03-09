﻿using System;
using System.Linq;

namespace Calculators
{
    /// <summary>
    /// Provides a method to add integers contained in a delimited string
    /// </summary>
    public class StringCalculator
    {
        /// <summary>
        /// Adds a integers contained in a delimited string
        /// </summary>
        /// <param name="numbers">The delimited string containing the integers</param>
        /// <returns>The sum of the integers</returns>
        /// <remarks>
        /// <para>
        /// The string can contain zero to any number of integers, but they must be
        /// delimited by commas or newline characters, or a mix, with no spaces. E.g. "0,1,2\n3\n4"
        /// </para>
        /// </remarks>
        public int Add(string numbers)
        {
            return numbers.Split(new[] {',', '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum();
        }
    }
}
