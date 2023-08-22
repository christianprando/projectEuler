using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem94 : IEulerProblem
    {
        public int ProblemNumber => 94;

        //https://www.had2know.org/academics/nearly-equilateral-heronian-triangles.html

        private readonly Dictionary<int, int> _largeBaseResults = new();
        private readonly Dictionary<int, int> _smallBaseResults = new();

        public string Run()
        {
            var limit = 1000000000;

            _largeBaseResults.Add(0, 5);
            _largeBaseResults.Add(1, 65);
            _largeBaseResults.Add(2, 901);

            _smallBaseResults.Add(0, 16);
            _smallBaseResults.Add(1, 240);
            _smallBaseResults.Add(2, 3360);
            
            for (var i = 3; ; i++)
            {
                var result = 15 * _largeBaseResults[i-1] - 15 * _largeBaseResults[i-2] + _largeBaseResults[i-3];
                var p = result * 3 + 1;

                if (p >= limit)
                {
                    break;
                }

                Console.WriteLine($"{result}-{result}-{result+1}");
                _largeBaseResults.Add(i, result);
            }

            for (var i = 3; ; i++)
            {
                var result = 15 * _smallBaseResults[i - 1] - 15 * _smallBaseResults[i - 2] + _smallBaseResults[i - 3];
                var p = result * (Int128)3 + 2;

                if (p >= limit)
                {
                    break;
                }

                Console.WriteLine($"{result+1}-{result+1}-{result}");
                _smallBaseResults.Add(i, result);
            }

            var sum = 0;
            foreach (var value in _largeBaseResults.Values)
            {
                sum += value * 3 + 1;
            }

            foreach (var value in _smallBaseResults.Values)
            {
                sum += value * 3 + 2;
            }

            return sum.ToString();
        }
    }
}