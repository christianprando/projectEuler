using System;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler.Problems
{
    internal class Problem78 : IEulerProblem
    {
        private readonly SortedDictionary<int, BigInteger> _waysToSum = new SortedDictionary<int, BigInteger> {{0,1}};

        public int ProblemNumber => 78;

        public string Run()
        {
            var number = 2;
            BigInteger ways = 1;

            while (ways % 1000000 != 0)
            {
                ways = GetWaysToPartition(++number);
            }

            return number.ToString();
        }

        private BigInteger GetWaysToPartition(int n)
        {
            if (_waysToSum.TryGetValue(n, out var sum))
            {
                return sum;
            }

            sum = 0;
            int multiplier;
            int delta;
            var startingK = GetStartingK(n);
            var endingK = GetEndingK(n);

            for (var k = startingK; k <= endingK ; k++)
            {
                if (k == 0)
                {
                    continue;
                }

                multiplier = (int)Math.Pow(-1, k + 1);
                delta = k * (3 * k - 1) / 2;
                if (n - delta < 0)
                {
                    break;
                }

                var toAdd = GetWaysToPartition(n - delta);
                
                sum += multiplier * toAdd;
            }

            //Console.WriteLine($"p({n})={sum}");
            _waysToSum.Add(n, sum);
            return sum;
        }

        private double GetEndingK(int n)
        {
            return (Math.Sqrt(24 * n + 1) + 1) / 6;
        }

        private int GetStartingK(int n)
        {
            return -1 * (int)Math.Floor((Math.Sqrt(24 * n + 1) - 1) / 6);
        }
    }
}