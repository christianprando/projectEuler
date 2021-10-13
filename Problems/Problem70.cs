using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Problems
{
    internal class Problem70 : IEulerProblem
    {
        private readonly Dictionary<int, List<int>> _factors = new Dictionary<int, List<int>>();
        public int ProblemNumber => 70;

        public string Run()
        {
            var minRatio = new TotientProperties(100, 1);
            var limit = 10000000;
            var ending = limit/10;
            var startingPrime = GetPreviousPrime(ending);
            for (var prime = startingPrime; prime > 2; prime = GetPreviousPrime(prime))
            {
                for (var prime2 = GetPreviousPrime(limit / prime); prime2 <= prime && prime2 > 2; prime2 = GetPreviousPrime(prime2))
                {
                    var n = prime * (BigInteger)prime2;

                    BigInteger qN;
                    if (prime != prime2)
                    {
                        qN = n - prime - prime2 + 1;
                    }
                    else
                    {
                        qN = n - prime;
                    }

                    if (ArePermutations(qN, n))
                    {
                        var props = new TotientProperties(n, qN);
                        if (minRatio.Ratio > props.Ratio)
                        {
                            minRatio = props;
                        }
                    }
                }
            }

            return minRatio.N.ToString();
        }

        private bool ArePermutations(BigInteger n, BigInteger totient)
        {
            var primeChars = n.ToString().ToCharArray();
            var totientChars = totient.ToString().ToCharArray();

            Array.Sort(primeChars);
            Array.Sort(totientChars);

            return primeChars.SequenceEqual(totientChars);
        }

        //this is a helper method used to understand the relationship between n and q(n)
        private List<TotientProperties> GenerateListOfTotient(int limit)
        {
            var list = new List<TotientProperties>();

            for (var n = 2; n < limit; n++)
            {
                list.Add(new TotientProperties(n, GetTotient(n)));
            }

            foreach (var item in list.OrderBy(x => x.Ratio))
            {
                Console.WriteLine($"{item.N} | {item.Totient} | {item.Ratio}");
            }

            return list;
        }

        private int GetTotient(int n)
        {
            var totient = 1;

            for (var i = 2; i < n; i++)
            {
                if (!ShareCommonFactors(i, n))
                {
                    totient++;
                }
            }

            return totient;
        }

        private bool ShareCommonFactors(int i, int n)
        {
            var iFactors = GetFactors(i);
            var nFactors = GetFactors(n);

            return iFactors.Any(x => nFactors.Contains(x));
        }

        private int GetPreviousPrime(int i)
        {
            for (var number = i - 1; number > 3; number--)
            {
                if (GetFactors(number).Count() == 1)
                {
                    return number;
                }
            }

            return 2;
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

        internal class TotientProperties
        {
            public TotientProperties(BigInteger n, BigInteger totient)
            {
                N = n;
                Totient = totient;
            }

            public BigInteger N { get; }
            public BigInteger Totient { get; }
            public double Ratio => Math.Exp(BigInteger.Log(N) - BigInteger.Log(Totient));
        }
    }
}