using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem87 : IEulerProblem
    {
        public int ProblemNumber => 87;

        private readonly Dictionary<Int128, List<Int128>> _factors = new();
        private readonly List<Int128> _squares = new();
        private readonly List<Int128> _cubed = new();
        private readonly List<Int128> _fourths = new();
        private readonly HashSet<Int128> _sums = new();

        public string Run()
        {
            return MaxNumbersAsSumOfPrimes(50000000).ToString();
        }

        private int MaxNumbersAsSumOfPrimes(Int128 max)
        {
            for (Int128 num = 2; num * num < max; num++)
            {
                if (!IsPrime(num))
                {
                    continue;
                }

                Int128 squared = num * num;

                if (squared + 24 < max)
                {
                    _squares.Add(squared);
                }
                else
                {
                    break;
                } 

                Int128 cubed = num * num * num;

                if (cubed + 20 < max)
                {
                    _cubed.Add(cubed);
                }
                else
                {
                    continue;
                }

                Int128 fourth = num * num * num * num;

                if (fourth + 12 < max)
                {
                    _fourths.Add(fourth);
                }
            }

            foreach (var square in _squares)
            {
                foreach (var cubed in _cubed)
                {
                    foreach (var fourth in _fourths)
                    {
                        if (square + cubed + fourth < max)
                        {
                            _sums.Add(square + cubed + fourth);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return _sums.Count;
        }

        private bool IsPrime(Int128 i)
        {
            return GetFactors(i).Count() == 1;
        }

        private IEnumerable<Int128> GetFactors(Int128 i)
        {
            if (_factors.TryGetValue(i, out var readyFactors))
            {
                return readyFactors;
            }

            var factors = new List<Int128>();

            for (var factor = 2; factor <= Math.Sqrt((int)i); factor++)
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