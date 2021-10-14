using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Problems
{
    internal class Problem72 : IEulerProblem
    {
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();
        private readonly Dictionary<int, int> _totients = new Dictionary<int, int>();

        public int ProblemNumber => 72;

        public string Run()
        {
            var limit = 1000000;
            BigInteger sum = 0;

            for (var n = 2; n <= limit; n++)
            {
                sum += GetTotient(n);
            }

            return sum.ToString();
        }

        private int GetTotient(int i)
        {
            if (_totients.ContainsKey(i))
            {
                return _totients[i];
            }
            
            var factors = GetPrimeFactors(i);

            if (factors.GroupBy(x => x).Count() == 1)
            {
                var exponent = factors.Count - 1;
                var t = i - (int)Math.Pow(factors.First(), exponent);
                _totients.Add(i, t);
                return t;
            }

            var product = 1;
            foreach (var group in factors.GroupBy(x => x))
            {
                var value = (int)Math.Pow(group.Key, group.Count());
                product *= GetTotient(value);
            }

            _totients.Add(i, product);
            return product;
        }

        private List<int> GetPrimeFactors(int i)
        {
            
            
            var number = i;
            var smallestPrimeFactor = 2;
            var primeFactors = new List<int>();
            while (!IsPrime(number))
            {
                if (number % smallestPrimeFactor == 0)
                {
                    primeFactors.Add(smallestPrimeFactor);
                    number /= smallestPrimeFactor;

                    smallestPrimeFactor = 2;
                }
                else
                {
                    smallestPrimeFactor = GetNextPrime(smallestPrimeFactor);
                    if (smallestPrimeFactor > Math.Sqrt(number))
                    {
                        break;
                    }
                }
            }

            primeFactors.Add(number);

            return primeFactors;
        }

        private int GetNextPrime(int i)
        {
            for (var number = i + 1; ; number++)
            {
                if (GetFactors(number).Count() == 1)
                {
                    return number;
                }
            }
        }

        private bool IsPrime(int i)
        {
            return GetFactors(i).Count() == 1;
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