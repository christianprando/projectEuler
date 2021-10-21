using System;
using System.Numerics;

namespace ProjectEuler.Problems
{
    internal class Problem80 : IEulerProblem
    {
        public int ProblemNumber => 80;

        public string Run()
        {
            var sum = 0;
            for (var n = 1; n <= 100; n++)
            {
                if (Math.Sqrt(n) % 1 == 0)
                {
                    continue;
                }
                var partialSum = SumSquareRootDecimals(n, 100);

                //Console.WriteLine($"n: {n} | sum: {partialSum}");

                sum += partialSum;
            }
            
            return sum.ToString();
        }

        private int SumSquareRootDecimals(int n, int maxDigits)
        {
            BigInteger p = 0;
            var sum = 0;
            BigInteger c = n;
            for (var digits = 0; digits < maxDigits; digits++)
            {
                BigInteger y = 0;
                var savedX = 0;
                int x;
                for (x = 1; x <= 9; x++)
                {
                    BigInteger candidateY = x * (20 * p + x);
                    if (candidateY > c)
                    {
                        break;
                    }

                    savedX = x;
                    y = candidateY;
                }

                sum += savedX;
                var newRemainder = c - y;
                c = newRemainder * 100;
                p = p * 10 + savedX;
            }

            return sum;
        }
    }
}