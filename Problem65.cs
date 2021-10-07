using System.Collections.Generic;
using System.Numerics;

namespace EulerProject
{
    internal class Problem65 : IEulerProblem
    {
        public int ProblemNumber => 65;

        public string Run()
        {
            var convergents = 100;
            var listOfAValues = ListOfAValuesForE(convergents);
            var fraction = new Fraction(1, listOfAValues[convergents - 1]);

            for (var i = convergents - 1; i >= 1; i--)
            {
                //Console.WriteLine($"Adding {listOfAValues[i - 1]} and {fraction.Numerator} / {fraction.Denominator}");
                fraction = AddIntegerAndFraction(listOfAValues[i - 1], fraction);
                fraction.Invert();
                //Console.WriteLine($"Result: {fraction.Numerator} / {fraction.Denominator}");
            }

            fraction.Invert();

            var sum = SumOfDigits(fraction.Numerator);

            return $"{sum}";
        }

        private BigInteger SumOfDigits(BigInteger number)
        {
            var modifiedNumber = number;

            BigInteger sum = 0;

            while (modifiedNumber > 0)
            {
                var lastDigit = modifiedNumber % 10;
                sum += lastDigit;
                modifiedNumber = (modifiedNumber - lastDigit) / 10;
            }

            return sum;
        }

        private List<int> ListOfAValuesForE(int convergents)
        {
            var list = new List<int> { 2 };

            for (var i = 1; list.Count <= convergents; i++)
            {
                list.Add(1);
                list.Add(2*i);
                list.Add(1);
            }

            return list;
        }

        private Fraction AddIntegerAndFraction(int number, Fraction fraction)
        {
            return new Fraction(number * fraction.Denominator + fraction.Numerator, fraction.Denominator);
        }
    }

    internal class Fraction
    {
        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        
        public BigInteger Numerator;
        public BigInteger Denominator;

        public void Invert()
        {
            (Numerator, Denominator) = (Denominator, Numerator);
        }
    }
}