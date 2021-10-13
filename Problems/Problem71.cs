using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem71 : IEulerProblem
    {
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();

        public int ProblemNumber => 71;

        public string Run()
        {
            var limit = 3 / (double)7;
            var currentCloser = 0d;
            var closestN = 0;
            var closestD = 0;

            for (var d = 2; d <= 1000000; d++)
            {
                var n = d * 3 / 7;

                if (ShareCommonFactors(n, d))
                {
                    n = d - n != 1 ? GetNextN(n, d) : GetPreviousN(n, d);
                }

                var leftN = 0;
                if (n / (double)d > limit)
                {
                    while (n / (double)d > limit)
                    {
                        n = GetPreviousN(n, d);
                        if (n == -1)
                        {
                            break;
                        }
                        leftN = n;
                    }
                }
                else if (n / (double)d < limit)
                {
                    while (n / (double)d < limit)
                    {
                        leftN = n;
                        n = GetNextN(n, d);
                        if (n == -1)
                        {
                            break;
                        }
                    }
                }

                if (!(currentCloser < leftN / (double)d)) 
                    continue;

                currentCloser = leftN / (double)d;
                closestD = d;
                closestN = leftN;
            }

            //Console.WriteLine($"N: {closestN} | D: {closestD} | n/d: {closestN / (double) closestD}");

            return closestN.ToString();
        }

        private int GetNextN(int n, int d)
        {
            if (d - n == 1)
            {
                return -1;
            }

            var newN = n + 1;
            while (ShareCommonFactors(newN, d))
            {
                newN++;
            }

            return newN;
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