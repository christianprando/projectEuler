using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace EulerProject
{
    internal class Problem66 : IEulerProblem
    {
        private readonly List<BigInteger> _squares = new List<BigInteger>();
        private BigInteger _maxSquare;
        private BigInteger _maxSquared;
        public int ProblemNumber => 66;

        public string Run()
        {
            BigInteger largestX = 0;
            var dWithLargestX = 0;
            var listOfNonSquares = GenerateListOfNonSquaresUpTo(1000);

            foreach (var d in listOfNonSquares.Keys)
            {
                var aValues = GetRepresentationValuesOfIrrationalSquareRoot(d);
                var sequenceCounter = 0;

                Fraction fraction;

                do
                {
                    var sequence = GenerateSequence(aValues, sequenceCounter);

                    fraction = new Fraction(1, sequence[sequence.Count - 1]);

                    for (var i = sequence.Count - 1; i >= 1; i--)
                    {
                        //Console.WriteLine($"Adding {listOfAValues[i - 1]} and {fraction.Numerator} / {fraction.Denominator}");
                        fraction = AddIntegerAndFraction(sequence[i - 1], fraction);
                        fraction.Invert();
                        //Console.WriteLine($"Result: {fraction.Numerator} / {fraction.Denominator}");
                    }

                    fraction.Invert();

                    sequenceCounter++;
                } while (fraction.Numerator * fraction.Numerator - d * fraction.Denominator * fraction.Denominator != 1);

                if (fraction.Numerator > largestX)
                {
                    largestX = fraction.Numerator;
                    dWithLargestX = d;
                }

            }

            return dWithLargestX.ToString();
        }

        private List<int> GenerateSequence(ContinuedFractionRepresentation aValues, int sequenceCounter)
        {
            var sequence = new List<int> { aValues.A0 };

            for (var i = 0; i < sequenceCounter; i++)
            {
                var index = i % aValues.RepeatingBlock.Count;
                sequence.Add(aValues.RepeatingBlock[index]);
            }

            return sequence;
        }

        private Fraction AddIntegerAndFraction(int number, Fraction fraction)
        {
            return new Fraction(number * fraction.Denominator + fraction.Numerator, fraction.Denominator);
        }

        private ContinuedFractionRepresentation GetRepresentationValuesOfIrrationalSquareRoot(int number)
        {
            var representation = new ContinuedFractionRepresentation();

            double m = 0;
            double d = 1;
            var a0 = Math.Floor(Math.Sqrt(number));
            var a = a0;

            var period = 0;

            var stringRepresentation = $"sqrt({number}) = [{a0};(";
            representation.A0 = (int)a0;

            while (true)
            {
                m = d * a - m;
                d = (number - m * m) / d;
                a = Math.Floor((a0 + m) / d);

                period++;

                representation.RepeatingBlock.Add((int)a);

                if (a - 2 * a0 == 0)
                {
                    stringRepresentation += $"{a})] period={period}";
                    Debug.WriteLine(stringRepresentation);
                    break;
                }

                stringRepresentation += $"{a},";
            }

            return representation;
        }

        private Dictionary<int, (BigInteger x, BigInteger y)> GenerateListOfNonSquaresUpTo(int limit)
        {
            var dictionary = new Dictionary<int, (BigInteger, BigInteger)>();
            for (var i = 1; i <= limit; i++)
            {
                if (!IsSquare(i))
                {
                    dictionary.Add(i, (0, 0));
                }
            }

            return dictionary;
        }

        private bool IsSquare(BigInteger i)
        {
            if (i < _maxSquare)
            {
                return _squares.Contains(i);
            }

            while (i > _maxSquare)
            {
                _maxSquared++;

                _maxSquare = _maxSquared * _maxSquared;

                _squares.Add(_maxSquare);
            }

            return _squares.Contains(i);
        }

    }

    internal class ContinuedFractionRepresentation
    {
        public ContinuedFractionRepresentation()
        {
            RepeatingBlock = new List<int>();
        }

        public int A0;
        public List<int> RepeatingBlock;
    }
}