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
        private static readonly Regex MultipleCharacterDelimiterRegex = new Regex("^\\/\\/(\\[([^0-9\\[\\]]+)\\])+(\\r\\n?|\\n).*");

        /// <summary>
        /// Adds a integers contained in a delimited string
        /// </summary>
        /// <param name="numbers">The delimited string containing the integers</param>
        /// <returns>The sum of the integers</returns>
        /// <exception cref="Exception">When any of the supplied integers are less than zero</exception>
        /// <remarks>
        /// <para>
        /// The string can contain zero to any number of integers. With no whitespace between the integers
        /// and/or the delimiters. E.g. "0,1,2"
        /// </para>
        /// <para>
        /// Delimiters are specified at the begining of the string using the format
        /// "//[delimiter1][delimiter2]\n[numbers…]".<br/>
        /// Example 1: "//[***]\n1***2***3", which will return the result 6.<br/>
        /// Example 2: "//[*][$]\n1*2$3$4*5", which will return the result 15.<br/>
        /// Digits and square brackets ('[' and ']') are not allowed as part of the delimiter.
        /// </para>
        /// <para>
        /// The square brackets are optional if one single character delimiter is specified.
        /// For example "//;\n1;2", which will return the result 3. In this instance '[' and ']' are allowed 
        /// as delimiters, but digits are still not allowed.
        /// </para>
        /// <para>
        /// If no delimiters are specified the leading slashes and newline character should be omitted.
        /// In this case commas and newline characters, or a mix, will be expected. E.g. "0,1,2\n3\n4"
        /// </para>
        /// <para>
        /// Integers over 1000 in the input are ignored and not included in the sum.
        /// </para>
        /// </remarks>
        public int Add(string numbers)
        {
            string[] delimiters;
            string cleanedNumbers;
            if (SingleDelimiterRegex.IsMatch(numbers))
            {
                delimiters = new[] {numbers[2].ToString()};
                cleanedNumbers = numbers.Substring(4);
            }
            else
            {
                var match = MultipleCharacterDelimiterRegex.Match(numbers);
                if (match.Success)
                {
                    var captures = match.Groups[2].Captures.Cast<Capture>().ToList();
                    delimiters = captures.Select(c => c.Value).ToArray();
                    cleanedNumbers = numbers.Substring(captures.Sum(c => c.Length + 2) + 3);
                }
                else
                {
                    delimiters = new[] {",", "\n"};
                    cleanedNumbers = numbers;
                }
            }
            var ints = cleanedNumbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var negatives = ints.Where(i => i < 0).ToList();
            if (negatives.Count > 0)
            {
                throw new Exception($"negatives not allowed: ({string.Join(", ", negatives)})");
            }
            return ints.Where(i => i <= 1000).Sum();

        }
    }
}