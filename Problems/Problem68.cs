using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem68 : IEulerProblem
    {
        private readonly List<string> _possibleAnswers = new List<string>();
        public int ProblemNumber => 68;

        private int _gonRingSize = 5;
        private bool _limitSize = true;
        private int _stringSize = 16;

        public string Run()
        {
            var total = _gonRingSize * 2;

            var numbers = new List<int>();

            for (var i = 1; i <= total; i++)
            {
                numbers.Add(i);
            }

            GetPer(numbers.ToArray());

            return _possibleAnswers.Max();
        }

        private void Swap(ref int a, ref int b)
        {
            if (a == b) return;

            (a, b) = (b, a);
        }

        public void GetPer(int[] list)
        {
            var x = list.Length - 1;
            GetPer(list, 0, x, ConsiderPermutation);
        }

        private void GetPer(int[] list, int k, int m, Action<int[]> action)
        {
            if (k == m)
            {
                action(list);
            }
            else
            {
                for (var i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m, action);
                    Swap(ref list[k], ref list[i]);
                }
            }
        }

        private string PrintList(int[] list)
        {
            var s = "{";
            foreach (var num in list)
            {
                s += $" {num}";
            }

            s += "}";

            return s;
        }

        private void ConsiderPermutation(int[] list)
        {
            var groups = new List<List<int>>();

            var indexCounter = 0;
            var lastIndex = 0;
            
            for (var groupIndex = 0; groupIndex < _gonRingSize; groupIndex++)
            {
                var indexList =  new List<int>();

                if (groupIndex == 0)
                {
                    indexList.Add(list[indexCounter++]);
                    indexList.Add(list[indexCounter++]);
                    lastIndex = indexCounter++;
                    indexList.Add(list[lastIndex]);
                }
                else
                {
                    indexList.Add(list[indexCounter++]);
                    indexList.Add(list[lastIndex]);
                    lastIndex = indexCounter % (_gonRingSize * 2) == 0 ? 1 : indexCounter++;
                    indexList.Add(list[lastIndex]);
                }

                groups.Add(indexList);
            }

            if (SumOfAllGroupsIsEqual(groups))
            {
                var startingIndex = groups.Select(x => x.First()).Select((value, index) => new { Value = value, Index = index })
                    .Aggregate((left, right) => (left.Value < right.Value) ? left : right)
                    .Index;

                var orderedGroups = new List<List<int>>();

                for (var increment = 0; increment < _gonRingSize; increment++)
                {
                    var index = (startingIndex + increment) % _gonRingSize;
                    orderedGroups.Add(groups[index]);
                }

                var str = GroupsToString(orderedGroups);
                
                if (_limitSize && str.Length != _stringSize)
                {
                    return;
                }
                
                Debug.WriteLine(PrintList(list));
                Debug.WriteLine(str);
                _possibleAnswers.Add(str);
            }
        }

        private string GroupsToString(List<List<int>> groups)
        {
            var str = string.Empty;
            foreach (var group in groups)
            {
                foreach (var num in group)
                {
                    str += num;
                }
            }

            return str;
        }

        private bool SumOfAllGroupsIsEqual(IEnumerable<List<int>> groups)
        {
            return !groups.Select(x => x.Sum()).Distinct().Skip(1).Any();
        }
    }
}