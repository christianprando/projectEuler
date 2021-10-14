using System.Collections.Generic;

namespace ProjectEuler.Problems
{
    internal class Problem74 : IEulerProblem
    {
        private readonly Dictionary<int, int> _factorials = new Dictionary<int, int>();
        private readonly  Dictionary<int, int> _factorialSumCache = new Dictionary<int, int>();

        public int ProblemNumber => 74;

        public string Run()
        {
            var sizeSixtyCounter = 0;
            for (var i = 1; i < 1000000; i++)
            {
                if (CountTerms(i) == 60)
                {
                    sizeSixtyCounter++;
                }
            }
            return sizeSixtyCounter.ToString();
        }

        private int CountTerms(int i)
        {
            var terms = new HashSet<int>();
            int term = i;
            terms.Add(term);
            do
            {
                term = GetFactorialSum(term);
            } while (terms.Add(term));

            return terms.Count;
        }

        private int GetFactorialSum(int term)
        {
            if (_factorialSumCache.TryGetValue(term, out var factorialSum))
            {
                return factorialSum;
            }

            var number = term;
            var sum = 0;

            while (number >= 10)
            {
                var digit = number % 10;
                number = number / 10;
                sum += Factorial(digit);
            }

            sum += Factorial(number);

            _factorialSumCache.Add(term, sum);

            return sum;
        }

        private int Factorial(int digit)
        {
            if (_factorials.ContainsKey(digit))
            {
                return _factorials[digit];
            }

            var product = 1;
            for (var i = digit; i > 1; i--)
            {
                product *= i;
            }

            _factorials.Add(digit, product);
            return product;
        }
    }
}