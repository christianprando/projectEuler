using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectEuler.Problems
{
    internal class Problem92 : IEulerProblem
    {
        public int ProblemNumber => 92;

        private readonly Dictionary<int,int> _arrivals = new();

        public string Run()
        {
            for (var i = 1; i < 10000000; i++)
            {
                var div = 10000000;
                var num = i;

                var sumOfSquares = 0;

                while (num > 0)
                {
                    var digit = num / div;
                    sumOfSquares += digit*digit;

                    num %= div;
                    div /= 10;
                }

                _arrivals.Add(i, sumOfSquares);
            }

            foreach (var arrival in _arrivals)
            {
                var listOfKeys = new List<int> { arrival.Key };
                var number = arrival.Value;

                while (number != 89 && number != 1)
                {
                    number = _arrivals[number];
                    listOfKeys.Add(number);
                }

                foreach (var key in listOfKeys)
                {
                    _arrivals[key] = number;
                }
            }

            return _arrivals.Count(x => x.Value == 89).ToString();
        }
    }
}