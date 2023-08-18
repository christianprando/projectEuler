using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem88 : IEulerProblem
    {
        private readonly Dictionary<int, Dictionary<int, HashSet<int>>> _factorsDictionary = new();
        public int ProblemNumber => 88;
        
        public string Run()
        {
            return GetSumOfMinimalProductSumNumbers(12000).ToString();
        }

        private int GetSumOfMinimalProductSumNumbers(int maxK)
        {
            for (var i = 4; i <= maxK * 2 + 1; i++)
            {
                ComputeFactors(i);
            }

            var sums = new HashSet<int>();
            for (var i = 2; i <= maxK; i++)
            {
                sums.Add(FindMinimalProductSum(i));
            }

            return sums.Sum();
        }

        private int FindMinimalProductSum(int i)
        {
            foreach (var item in _factorsDictionary)
            {
                if(item.Key > i*2)
                {
                    break;
                }

                foreach (var combination in item.Value)
                {
                    foreach (var sum in combination.Value)
                    {
                        if (i - combination.Key + sum == item.Key)
                        {
                            return item.Key;
                        }
                    }
                }
            }

            return -1;
        }

        private void ComputeFactors(int num)
        {
            var dictionary = new Dictionary<int, HashSet<int>>();
            _factorsDictionary.Add(num, dictionary);

            for (var firstFactor = 2; firstFactor <= Math.Sqrt(num); firstFactor++)
            {
                if (num % firstFactor != 0) 
                    continue;

                var otherFactor = num / firstFactor;

                UpdateDictionary(dictionary, 2, firstFactor + otherFactor);

                var possibilitiesFirstFactor = new Dictionary<int, HashSet<int>>();
                var possibilitiesOtherFactor = new Dictionary<int, HashSet<int>>();

                if (_factorsDictionary.ContainsKey(firstFactor))
                {
                    possibilitiesFirstFactor = _factorsDictionary[firstFactor];
                }

                if (firstFactor != otherFactor && _factorsDictionary.ContainsKey(otherFactor))
                {
                    possibilitiesOtherFactor = _factorsDictionary[otherFactor];
                }

                foreach (var firstItem in possibilitiesFirstFactor)
                {
                    foreach (var otherItem in possibilitiesOtherFactor)
                    {
                        var numberOfFactors = firstItem.Key + otherItem.Key;

                        foreach (var firstSum in firstItem.Value)
                        {
                            foreach (var otherSum in otherItem.Value)
                            {
                                var sumOfFactors = firstSum + otherSum;

                                UpdateDictionary(dictionary, numberOfFactors, sumOfFactors);
                            }
                        }
                    }
                }

                foreach (var firstItem in possibilitiesFirstFactor)
                {
                    var numberOfFactors = firstItem.Key + 1;

                    foreach (var sum in firstItem.Value)
                    {
                        var sumOfFactors = sum + otherFactor;

                        UpdateDictionary(dictionary, numberOfFactors, sumOfFactors);
                    }
                }

                foreach (var otherItem in possibilitiesOtherFactor)
                {
                    foreach (var sum in otherItem.Value)
                    {
                        var numberOfFactors = otherItem.Key + 1;
                        var sumOfFactors = sum + firstFactor;

                        UpdateDictionary(dictionary, numberOfFactors, sumOfFactors);
                    }
                }
            }
        }

        private void UpdateDictionary(Dictionary<int, HashSet<int>> dictionary, int numberOfFactors, int sumOfFactors)
        {
            if (dictionary.TryGetValue(numberOfFactors, out var set))
            {
                set.Add(sumOfFactors);
            }
            else
            {
                dictionary.Add(numberOfFactors, new HashSet<int>{sumOfFactors});
            }
        }
    }
}