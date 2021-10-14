using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem73 : IEulerProblem
    {
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();

        public int ProblemNumber => 73;

        public string Run()
        {
            var maxLimit = 1 / (double)2;
            var lowLimit = 1 / (double)3;
            var counter = 0;

            for (var d = 2; d <= 12000; d++)
            {
                var n = d / 2;

                if (ShareCommonFactors(n, d))
                {
                    n = GetPreviousN(n, d);
                }

                while (n / (double)d > lowLimit)
                {
                    if (n / (double)d < maxLimit)
                    {
                        counter++;
                    }
                    n = GetPreviousN(n, d);

                    if (n == -1)
                    {
                        break;
                    }
                }
            }

            return counter.ToString();
        }

        private int GetPreviousN(int n, int d)
        {
            if (n == 1)
            {
                return -1;
            }

            var newN = n - 1;
            while (ShareCommonFactors(newN, d))
            {
                newN--;
            }

            return newN;
        }

        private bool ShareCommonFactors(int i, int n)
        {
            var iFactors = GetFactors(i);
            var nFactors = GetFactors(n);

            return iFactors.Any(x => nFactors.Contains(x));
        }

        private IEnumerable<int> GetFactors(int i)
        {
            if (_factors.ContainsKey(i))
            {
                return _factors[i];
            }

            var factors = new List<int>();

            for (var factor = 2; factor <= Math.Sqrt(i); factor++)
            {
                if (i % factor == 0)
                {
                    factors.Add(factor);
                    var secondFactor = i / factor;

                    if (secondFactor != factor)
                    {
                        factors.Add(secondFactor);
                    }
                }
            }
            factors.Add(i);
            _factors.Add(i, factors);
            return factors;
        }
    }
}