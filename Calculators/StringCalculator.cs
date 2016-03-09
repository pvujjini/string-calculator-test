using System;
using System.Linq;

namespace Calculators
{
    /// <summary>
    /// Provides a method to add integers contained in a delimited string
    /// </summary>
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            return numbers.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum();
        }
    }
}
