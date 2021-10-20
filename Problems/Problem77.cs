using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem77 : IEulerProblem
    {
        private readonly SortedDictionary<int, List<List<int>>> _waysToSumLists = new SortedDictionary<int, List<List<int>>>();
        private readonly SortedDictionary<int, SortedDictionary<int, int>> _waysToSum = new SortedDictionary<int, SortedDictionary<int, int>>();
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();

        public int ProblemNumber => 77;

        public string Run()
        {
            var ways = 0;
            var number = 4;
            while (ways < 5000)
            {
                number++;
                ways = GetWaysToSum(number, 2);
            }

            return number.ToString();
        }

        private List<List<int>> GetWaysToSumLists(int number, int left)
        {
            if (number == 0 || number == 1 || number == 2)
            {
                return new List<List<int>>();
            }

            if (_waysToSumLists.TryGetValue(number, out var cachedResult))
            {
                var filteredList = cachedResult.Where(x => x.FirstOrDefault() >= left).ToList();
                return filteredList;
            }

            var lists = new List<List<int>>();

            for (var i = left; i <= number / 2; i = GetNextPrime(i))
            {
                var result = GetWaysToSumLists(number - i, i);
                foreach (var list in result)
                {
                    var newList = new List<int> { i };
                    newList.AddRange(list);
                    lists.Add(newList);
                }

                if (IsPrime(number - 1))
                {
                    lists.Add(new List<int> { i, number - i });
                }
            }

            _waysToSumLists.Add(number, lists);
            return lists;
        }

        private int GetWaysToSum(int number, int left)
        {
            if (_waysToSum.TryGetValue(number, out var cachedResult))
            {
                if (cachedResult.TryGetValue(left, out var cachedSum))
                {
                    return cachedSum;
                }
            }

            var sum = 0;
            for (var i = left; i <= number / 2; i = GetNextPrime(i))
            {
                var result = GetWaysToSum(number - i, i);
                sum += result;

                if (IsPrime(number - i))
                {
                    sum++;
                }
            }

            if (_waysToSum.TryGetValue(number, out var existing))
            {
                existing.Add(left, sum);
            }
            else
            {
                _waysToSum.Add(number, new SortedDictionary<int, int>(){{left, sum}});
            }

            return sum;
        }

        private int GetNextPrime(int i)
        {
            for (var number = i + 1; ; number++)
            {
                if (IsPrime(number))
                {
                    return number;
                }
            }
        }

        private bool IsPrime(int number)
        {
            if (number == 2 || number == 3)
            {
                return true;
            }
            return GetFactors(number).Count() == 1;
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