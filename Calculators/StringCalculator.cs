using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculators
{
    /// <summary>
    /// Provides a method to add integers contained in a delimited string
    /// </summary>
    public class StringCalculator
    {
        private static readonly Regex SingleDelimiterRegex = new Regex("^\\/\\/\\D(\\r\\n?|\\n).*");

        /// <summary>
        /// Adds a integers contained in a delimited string
        /// </summary>
        /// <param name="numbers">The delimited string containing the integers</param>
        /// <returns>The sum of the integers</returns>
        /// <remarks>
        /// <para>
        /// The string can contain zero to any number of integers. With no whitespace between the integers
        /// and/or the delimiters. E.g. "0,1,2"
        /// </para>
        /// <para>,
        /// The delimiter is specified at the beginning of the string using the format "//[delimiter]\n[numbers…]".
        /// For example "//;\n1;2", which will return the result 3.
        /// If no delimiters are specified then commas and newline characters, or a mix, will be expected.
        /// E.g. "0,1,2\n3\n4"
        /// </para>
        /// </remarks>
        public int Add(string numbers)
        {
            char[] delimiters;
            string cleanedNumbers;
            if (SingleDelimiterRegex.IsMatch(numbers))
            {
                delimiters = new[] { numbers[2] };
                cleanedNumbers = numbers.Substring(4);
            }
            else
            {
                delimiters = new[] { ',', '\n' };
                cleanedNumbers = numbers;
            }
            return cleanedNumbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Sum();
        }
    }
}