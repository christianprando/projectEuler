using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem69 : IEulerProblem
    {
        public int ProblemNumber => 69;
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();

        public string Run()
        {
            var limit = 1000000;
            var product = 1;
            int i;
            var prevProduct = 0;
            for (i = 1; product <= limit; i = GetNextPrime(i))
            {
                prevProduct = product;
                product *= i;
            }

            return prevProduct.ToString();
        }

        private int GetNextPrime(int i)
        {
            for (var number = i + 1;; number++)
            {
                if (GetFactors(number).Count() == 1)
                {
                    return number;
                }
            }
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