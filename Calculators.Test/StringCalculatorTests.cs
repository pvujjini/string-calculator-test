using System;
using System.Linq;
using NUnit.Framework;

namespace Calculators.Test
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [TestCase]
        public void AddUpToTwoNumbers()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add(string.Empty));
            Assert.AreEqual(0, calc.Add("0"));
            Assert.AreEqual(1, calc.Add("1"));
            Assert.AreEqual(537, calc.Add("537"));
            Assert.AreEqual(1000, calc.Add("1000"));
            Assert.AreEqual(0, calc.Add("0,0"));
            Assert.AreEqual(1, calc.Add("0,1"));
            Assert.AreEqual(1, calc.Add("1,0"));
            Assert.AreEqual(2, calc.Add("1,1"));
            Assert.AreEqual(3, calc.Add("1,2"));
            Assert.AreEqual(2000, calc.Add("1000,1000"));
            Assert.AreEqual(755, calc.Add("537,218"));
        }

        /// <summary>
        /// Tests adding an arbitrary number of integers
        /// </summary>
        /// <remarks>
        /// I tried testing a much longer string than in the tests as detailed in the method comments.
        /// As I was conscious of time I had to stick with what I had and leave the longer tests. Given
        /// enough time a decent pratical limit for testing could be established 
        /// </remarks>
        [TestCase]
        public void AddMoreThanTwoNumbers()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("0,0,0"));
            Assert.AreEqual(0, calc.Add("0,0,0,0,0,0,0"));
            Assert.AreEqual(0, calc.Add(string.Join(",", Enumerable.Range(1, 100000).Select(i => "0"))));
            Assert.AreEqual(100000, calc.Add(string.Join(",", Enumerable.Range(1, 100000).Select(i => "1"))));

            // The nth triangular number is given by: ((n * n) + n) / 2
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add(string.Join(",", Enumerable.Range(1, 1000).Select(i => i.ToString()))));

            // A string this long causes an out of memory exception before the tested method is even called so a 100000 number string
            // will have to do without spending a long time deciding what the maximum length can be
            //
            // var builder = new StringBuilder("1");
            // while (builder.Length < int.MaxValue) // 2,147,483,647
            // {
            //     builder.Append(",1");
            // } 
            // Assert.AreEqual((int.MaxValue / 2) + 1, calc.Add(builder.ToString()));
        }

        [TestCase]
        public void AllowNewlineAsSeparator()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("0\n0,0"));
            Assert.AreEqual(0, calc.Add("0,0\n0,0,0\n0,0"));
            Assert.AreEqual(0, calc.Add(string.Join("\n", Enumerable.Range(1, 100000).Select(i => "0"))));
            Assert.AreEqual(0, calc.Add(string.Join("\n", Enumerable.Range(1, 100000).Select(i => "0,0"))));
            Assert.AreEqual(200000, calc.Add(string.Join("\n", Enumerable.Range(1, 100000).Select(i => "1,1"))));
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add(string.Join("\n", Enumerable.Range(1, 500).Select(i => $"{(i * 2) - 1},{i * 2}"))));
        }

        [TestCase]
        public void AllowSingleCharacterDelimiters()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("// \n0 0"));
            Assert.AreEqual(0, calc.Add("//;\n0;0;0"));
            Assert.AreEqual(0, calc.Add("//a\n0a0a0a0a0a0a0"));
            Assert.AreEqual(0, calc.Add($"//\t\n{string.Join("\t", Enumerable.Range(1, 100000).Select(i => "0"))}"));
            Assert.AreEqual(100000, calc.Add($"//[\n{string.Join("[", Enumerable.Range(1, 100000).Select(i => "1"))}"));
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add($"//]\n{string.Join("]", Enumerable.Range(1, 1000).Select(i => i.ToString()))}"));
        }

        [TestCase]
        public void ThrowsOnNegatives()
        {
            var calc = new StringCalculator();
            Assert.Throws<Exception>(() => calc.Add("-1"), "negatives not allowed: (-1)");
            Assert.Throws<Exception>(() => calc.Add("//;\n-1;1000;-408"), "negatives not allowed: (-1, -408)");
            Assert.Throws<Exception>(() => calc.Add("//[\n" + string.Join("[", Enumerable.Range(1, 100000).Select(i => $"-{i}"))),
                $"negatives not allowed: {string.Join(", ", Enumerable.Range(1, 100000).Select(i => $"-{i}"))}");
        }

        [TestCase]
        public void IgnoreNumbersGreaterThanOneThousand()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("// \n1001"));
            Assert.AreEqual(1001, calc.Add($"//;\n1;1000;{int.MaxValue}"));
            Assert.AreEqual(10, calc.Add("//a\n1a1001a2a234565a3a6543a4"));
            Assert.AreEqual(0, calc.Add($"//\t\n{string.Join("\t", Enumerable.Range(1, 100000).Select(i => $"{i + 2064}"))}"));
        }

        [TestCase]
        public void AllowMultiCharacterDelimiters()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("//[ ]\n0 0"));
            Assert.AreEqual(0, calc.Add("//[   ]\n0   0   0"));
            Assert.AreEqual(0, calc.Add("//[abcdefg]\n0abcdefg0abcdefg0abcdefg0abcdefg0abcdefg0abcdefg0"));
            Assert.AreEqual(0, calc.Add($"//[\t$%&]\n{string.Join("\t$%&", Enumerable.Range(1, 100000).Select(i => "0"))}"));
            Assert.AreEqual(100000, calc.Add($"//[<>'@rr\"]\n{string.Join("<>'@rr\"", Enumerable.Range(1, 100000).Select(i => "1"))}"));
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add($"//[spqr]\n{string.Join("spqr", Enumerable.Range(1, 1000).Select(i => i.ToString()))}"));
        }

        [TestCase]
        public void AllowMultipleSingleCharacterDelimiters()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("//[ ][%]\n0%0 0"));
            Assert.AreEqual(0, calc.Add("//[a][b][c][d][e][f][g]\n0a0b0c0d0e0f0"));
            Assert.AreEqual(0, calc.Add($"//[\t][$][%][&]\n{string.Join("\t", Enumerable.Range(1, 100000).Select(i => "0"))}"));
            Assert.AreEqual(100000, calc.Add($"//[<][>]['][@][r][\"]\n{string.Join("\"", Enumerable.Range(1, 100000).Select(i => "1"))}"));
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add($"//[s][p][q][r]\n{string.Join("", Enumerable.Range(1, 1000).Select(i => $"{"spqr"[i % 4]}{i}")).Substring(1)}"));
        }

        [TestCase]
        public void AllowMultipleMultipleCharacterDelimiters()
        {
            var calc = new StringCalculator();
            Assert.AreEqual(0, calc.Add("//[ % ][**%]\n0 % 0**%0"));
            Assert.AreEqual(0, calc.Add("//[abc][defg]\n0abc0abc0abc0defg0defg0abc0"));
            Assert.AreEqual(0, calc.Add($"//[\t$][%&]\n{string.Join("\t$", Enumerable.Range(1, 100000).Select(i => "0"))}"));
            Assert.AreEqual(100000, calc.Add($"//[<>'][@r][\"]\n{string.Join("\"", Enumerable.Range(1, 100000).Select(i => "1"))}"));
            Assert.AreEqual(((1000 * 1000) + 1000) / 2,
                calc.Add($"//[spqr][rip][viz]\n{string.Join("", Enumerable.Range(1, 1000).Select(i => $"{(new [] {"spqr", "rip", "viz"})[i % 3]}{i}")).Substring(3)}"));
        }
    }
}
