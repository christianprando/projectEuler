using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem93 : IEulerProblem
    {
        public int ProblemNumber => 93;

        private List<Target> _possibilities = new();

        public string Run()
        {
            var max = 0;
            var maxDigits = string.Empty;

            for (var a = 1; a <= 10; a++)
            {
                for (var b = a + 1; b <= 10; b++)
                {
                    for (var c = b + 1; c <= 10; c++)
                    {
                        for (var d = c + 1; d <= 10; d++)
                        {
                            _possibilities = new List<Target>();
                            var maxInt = GetMaximumConsecutiveInt(new List<int> { a,b, c, d });

                            if (maxInt > max)
                            {
                                max = maxInt;
                                maxDigits = $"{a}{b}{c}{d}";
                            }
                        }
                    }
                }
            }

            return maxDigits;
        }

        private int GetMaximumConsecutiveInt(List<int> digits)
        {
            var results = new List<Target>();
            for (var i = 0; i < digits.Count; i++)
            {
                for (var j = i + 1; j < digits.Count; j++)
                {
                    results.AddRange(ComputePossibilities( new Target(digits[i]), new Target(digits[j])));
                }
            }

            _possibilities.AddRange(results);
            results = new List<Target>();

            for (var i = 0; i < digits.Count; i++)
            {
                foreach (var target in _possibilities.Where(x => x.Keys.Count == 2 && !x.Keys.Contains(digits[i])))
                {
                    results.AddRange(ComputePossibilities(new Target(digits[i]), target));
                }
            }

            _possibilities.AddRange(results);
            results = new List<Target>();

            for (var i = 0; i < digits.Count; i++)
            {
                foreach (var target in _possibilities.Where(x => x.Keys.Count == 3 && !x.Keys.Contains(digits[i])))
                {
                    results.AddRange(ComputePossibilities(new Target(digits[i]), target));
                }
            }

            _possibilities.AddRange(results);
            results = new List<Target>();

            foreach (var firstTarget in _possibilities.Where(x => x.Keys.Count == 2))
            {
                foreach (var secondTarget in _possibilities.Where(x => x.Keys.Except(firstTarget.Keys).Count() == 2))
                {
                    results.AddRange(ComputePossibilities(firstTarget, secondTarget));
                }
            }

            var finalResults = _possibilities.Where(x => x.Keys.Count == 4 && x.Value > 0 && x.Value%1 == 0).Select(x => x.Value).ToImmutableSortedSet();

            var previous = 0;
            for (var i = 0; i < finalResults.Count; i++)
            {
                if (finalResults[i] - previous > 1 || finalResults[i] - previous < 1)
                {
                    return previous;
                }
                else
                {
                    previous = (int)finalResults[i];
                }
            }

            return previous;
        }

        private HashSet<Target> ComputePossibilities(Target x, Target y)
        {
            var keys = new List<int>(x.Keys);
            keys.AddRange(y.Keys);

            var possibilities = new HashSet<Target>
            {
                new(keys, x.Value / y.Value),
                new(keys, y.Value / x.Value),
                new(keys, y.Value + x.Value),
                new(keys, y.Value - x.Value),
                new(keys, x.Value - y.Value),
                new(keys, x.Value * y.Value)
            };

            return possibilities;
        }

        internal class Target
        {
            public List<int> Keys;

            public double Value;

            public Target(List<int> keys, double val)
            {
                Keys = keys;
                Value = val;
            }

            public Target(int digit) : this(new List<int> { digit }, digit)
            {
            }
        }
    }
}