using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ProjectEuler.Extensions;

namespace ProjectEuler.Problems
{
    internal class Problem76 : IEulerProblem
    {
        private readonly SortedDictionary<int, List<List<int>>> _waysToSumLists = new SortedDictionary<int, List<List<int>>>();
        private readonly SortedDictionary<int, SortedDictionary<int, int>> _waysToSum = new SortedDictionary<int, SortedDictionary<int, int>>();

        public int ProblemNumber => 76;

        public string Run()
        {
            //var result = GetWaysToSumLists(10, 1);
            //foreach (var list in result)
            //{
            //    Console.WriteLine(list.GetString());
            //}
            var result = GetWaysToSum(100, 1);
            return result.ToString();
        }

        private List<List<int>> GetWaysToSumLists(int number, int left)
        {
            if (number == 0 || number == 1)
            {
                return new List<List<int>>();
            }

            if (_waysToSumLists.TryGetValue(number, out var cachedResult))
            {
                var filteredList = cachedResult.Where(x => x.FirstOrDefault() >= left).ToList();
                return filteredList;
            }

            var lists = new List<List<int>>();

            for (var i = left; i <= number / 2; i++)
            {
                var result = GetWaysToSumLists(number - i, i);
                foreach (var list in result)
                {
                    var newList = new List<int> { i };
                    newList.AddRange(list);
                    lists.Add(newList);
                }
                lists.Add(new List<int>{i, number - i});
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
            for (var i = left; i <= number / 2; i++)
            {
                var result = GetWaysToSum(number - i, i);
                sum += result + 1;
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
    }
}