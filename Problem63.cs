using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerProject
{
    internal class Problem63 : IEulerProblem
    {
        public int ProblemNumber => 63;

        public string Run()
        {
            return CountIntegersThatMatchNumberOfDigitsToPower();
        }

        private string CountIntegersThatMatchNumberOfDigitsToPower()
        {
            var counter = 0;

            BigInteger lowerLimit = 0;

            var listOfAnswers = new List<Tuple<BigInteger,BigInteger>>();

            for (var exponent = 1;; exponent++)
            {
                var tempCounter = counter;
                for (var baseNumber = lowerLimit + 1; CountDigits(baseNumber) <= exponent ; baseNumber++)
                {
                    var digits = CountDigits(BigInteger.Pow(baseNumber, exponent));
                    if (digits < exponent)
                    {
                        lowerLimit = baseNumber;
                    }

                    if (digits == exponent)
                    {
                        listOfAnswers.Add(new Tuple<BigInteger, BigInteger>(baseNumber, exponent));
                        counter++;
                    }

                    if (digits > exponent)
                    {
                        break;
                    }
                }

                if (counter == tempCounter)
                {
                    break;
                }
            }

            return counter.ToString();
        }

        private int CountDigits(BigInteger originalNumber)
        {
            var modifiedNumber = originalNumber;
            var digits = 0;
            BigInteger divisor = 10;
            while (true)
            {
                if (modifiedNumber == 0)
                    return digits;
                modifiedNumber -= modifiedNumber % divisor;
                digits++;
                divisor *= 10;
            }
        }
    }
}