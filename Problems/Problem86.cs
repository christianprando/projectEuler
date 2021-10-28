using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem86 : IEulerProblem
    {
        public int ProblemNumber => 86;
        
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();
        private HashSet<string> _triangles = new HashSet<string>();
        public string Run()
        {
            var m = 100;
            int counter;
            do
            {
                m++;
                counter = RunToTheLimit(m);
                _triangles = new HashSet<string>();
            } while (counter <= 1000000);

            return m.ToString();
        }

        private int RunToTheLimit(int limit)
        {
            var counter = 0;
            for (var n = 1; ; n++)
            {
                var m = n + 1;
                
                var a = Math.Pow(m, 2) - Math.Pow(n , 2);
                var b = 2 * m * n;
                var max = a > b ? a : b;
                var min = a > b ? b : a;
                if (max > 2 * limit && min > limit)
                {
                    break;
                }
                
                for (m = n + 1; ; m = GetNextCoPrime(n,m))
                {
                    a = Math.Pow(m, 2) - Math.Pow(n , 2);
                    b = 2 * m * n;

                    max = Math.Max(a, b);
                    min = Math.Min(a, b);

                    if (max > 2 * limit || min > limit)
                    {
                        break;
                    }

                    var result = AddSolutions((int)max, (int)min, limit);
                    counter += result;

                    if (result != 0)
                    {
                        var multipliedMax = max * 2;
                        var multipliedMin = min * 2;

                        while (multipliedMax <= 2 * limit && multipliedMin <= limit ||
                               multipliedMin <= 2 * limit && multipliedMax <= limit)
                        {
                            counter += AddSolutions((int) multipliedMax, (int) multipliedMin, limit);
                            multipliedMax += max;
                            multipliedMin += min;
                        }
                    }
                }
            }

            return counter;
        }
        
        private int AddSolutions(int max, int min, int limit)
        {
            var key = $"{max}:{min}";
            if (_triangles.Contains(key))
            {
                return 0;
            }

            _triangles.Add(key);

            var ceiling = Math.Min(min / 2, limit);
            var alternativeCounter = 0;
            if (max <= limit)
            {
                alternativeCounter = ceiling;
            }
           
            var floor = Math.Min(max / 2, limit);
            if (min <= limit)
            {
                alternativeCounter += Math.Max(0, floor - (max - min - 1));
            }

            return alternativeCounter;
        }

        private int GetNextCoPrime(int a, int b)
        {
            var n = b;
            var aFactors = GetFactors(a).ToList();
            do
            {
                n++;
            } while (GetFactors(n).Intersect(aFactors).Any());

            return n;
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