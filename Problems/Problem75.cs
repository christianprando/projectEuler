using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem75 : IEulerProblem
    {
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();
        private readonly SortedDictionary<int, Solution> _solutions = new SortedDictionary<int, Solution>();
        public int ProblemNumber => 75;

        public string Run()
        {
            var limit = 1500000;

            for (var n = 1; 2*(n+1)*(n+1) <= limit/2; n ++)
            {
                for (var m = n + 1;; m = GetNextCoPrime(n,m))
                {
                    var sides = new Sides(m * m + n * n, 2 * m * n, m * m - n * n);
                    if (sides.Sum() <= limit)
                    {
                        AddSolution(sides);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            var primeSolutions = _solutions.ToList();

            foreach (var solution in primeSolutions)
            {
                foreach (var sides in solution.Value.SideCollection)
                {
                    var multiplier = 2;
                    var newSides = sides.Multiply(multiplier);

                    while (newSides.Sum() <= limit)
                    {
                        AddSolution(newSides);
                        multiplier++;
                        newSides = sides.Multiply(multiplier);
                    }
                }
            }

            //foreach (var solution in _solutions)
            //{
            //    Console.WriteLine($"{solution.Key} cm: {solution.Value.PrintSides()}");
            //}
            return _solutions.Count(x => x.Value.Solutions == 1).ToString();
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

        private void AddSolution(Sides sides)
        {
            var solution = new Solution(sides);
            if (_solutions.ContainsKey(solution.Sum))
            {
                if(!_solutions[solution.Sum].HasEquivalentSides(sides)){
                    _solutions[solution.Sum].SideCollection.Add(solution.SideCollection.First());
                }
            }
            else
            {
                _solutions.Add(solution.Sum, solution);
            }
        }

        private class Sides
        {
            public Sides(int hypotenuse, int opposite, int adjacent)
            {
                SideValues = new List<int> { hypotenuse, adjacent, opposite };
            }

            public List<int> SideValues; 

            public string Print()
            {
                return $"({SideValues.First()},{SideValues.ElementAt(1)},{SideValues.Last()})";
            }

            public int Sum()
            {
                return SideValues.Sum();
            }

            public Sides Multiply(int multiplier)
            {
                return new Sides(SideValues.First() * multiplier, SideValues.ElementAt(1) * multiplier,
                    SideValues.Last() * multiplier);
            }

            public bool Equivalent(Sides sides)
            {
                return sides.SideValues.All(SideValues.Contains);
            }
        }

        private class Solution
        {
            public Solution(Sides sides)
            {
                SideCollection.Add(sides);
            }

            public int Sum => SideCollection.First().Sum();
            public int Solutions => SideCollection.Count;
            public List<Sides> SideCollection = new List<Sides>();

            public string PrintSides()
            {
                var str = string.Empty;
                foreach (var sides in SideCollection)
                {
                    str += $"{sides.Print()} ";
                }

                return str;
            }

            public bool HasEquivalentSides(Sides sides)
            {
                return SideCollection.Any(x => x.Equivalent(sides));
            }
        }
    }
}