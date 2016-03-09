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

    }
}
