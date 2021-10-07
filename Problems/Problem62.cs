using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Problems
{
    internal class Problem62 : IEulerProblem
    {
        private readonly Dictionary<BigInteger, List<BigInteger>> _digitsCache;

        public Problem62()
        {
            _digitsCache = new Dictionary<BigInteger, List<BigInteger>>();
        }

        public int ProblemNumber => 62;

        public string Run()
        {
            return SmallestCubeWithCubicPermutations(5);
        }

        public string SmallestCubeWithCubicPermutations(int numberOfCubicPermutations)
        {
            var permutationsDictionary = new Dictionary<BigInteger, List<BigInteger>>();

            for (BigInteger i = 1;; i++)
            {
                var cube = i * i * i;

                var permutations = FindPermutations(cube, permutationsDictionary);
                
                if (permutations == null)
                {
                    permutationsDictionary.Add(i, new List<BigInteger> { cube });
                    continue;
                }

                var enumerable = permutations.ToList();
                if (enumerable.Count == numberOfCubicPermutations)
                {
                    return enumerable.FirstOrDefault().ToString();
                }
                
            }
        }

        private IEnumerable<BigInteger> FindPermutations(BigInteger cube, Dictionary<BigInteger, List<BigInteger>> cubeDictionary)
        {
            return cubeDictionary.FirstOrDefault(x => IsPermutation(x.Value, cube)).Value;
        }

        private bool IsPermutation(List<BigInteger> permutations, BigInteger originalCube)
        {
            var originalDigits = GetDigits(originalCube);
            var testedDigits = GetDigits(permutations.FirstOrDefault());

            originalDigits.Sort();
            testedDigits.Sort();

            if (!originalDigits.SequenceEqual(testedDigits)) return false;

            //Debug.WriteLine($"Permuting cubes: {permutations} and {originalCube}");
            permutations.Add(originalCube);
            return true;
        }

        private List<BigInteger> GetDigits(BigInteger originalCube)
        {
            if (_digitsCache.TryGetValue(originalCube, out var digits))
            {
                return digits;
            }

            digits = new List<BigInteger>();
            var subtractedCube = originalCube;
            for (BigInteger divisor = 10;; divisor *= 10)
            {
                var modulo = subtractedCube % divisor;
                var digit = modulo/(divisor/10);
                digits.Add(digit);

                subtractedCube -= subtractedCube % divisor;

                if (subtractedCube == 0)
                {
                    _digitsCache.Add(originalCube, digits);
                    return digits;
                }
            }
        }
    }
}