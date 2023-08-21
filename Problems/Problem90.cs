using System;
using System.Collections.Generic;

namespace ProjectEuler.Problems
{
    internal class Problem90 : IEulerProblem
    {
        public int ProblemNumber => 90;

        private readonly List<Tuple<int, int>> _squares = new()
        {
            new(0,1),
            new(0,4),
            new(0,9),
            new(1,6),
            new(2,5),
            new(3,6),
            new(4,9),
            new(6,4),
            new(8,1)
        };

        public string Run()
        {
            var validCombinations = new HashSet<int>();

            for (var i = 12345; i <= 456789; i++)
            {
                if (IsValid(i))
                {
                    validCombinations.Add(i);
                }
            }

            var arrangements = 0;

            foreach (var firstDice in validCombinations)
            {
                foreach (var secondDice in validCombinations)
                {
                    if (ValidArrangement(firstDice, secondDice))
                    {
                        Console.WriteLine(firstDice + " " + secondDice);
                        arrangements++;
                    }
                }
            }

            arrangements /= 2;

            return arrangements.ToString();
        }

        private bool ValidArrangement(int firstDice, int secondDice)
        {
            var firstDiceSet = DiceToSet(firstDice);
            var secondDiceSet = DiceToSet(secondDice);

            foreach (var square in _squares)
            {
                if (!CanMakeCombination(square.Item1, square.Item2, firstDiceSet, secondDiceSet))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanMakeCombination(int firstDigit, int secondDigit, HashSet<int> firstDice, HashSet<int> secondDice)
        {
            return DiceContains(firstDice, firstDigit) && DiceContains(secondDice, secondDigit) || DiceContains(secondDice, firstDigit) && DiceContains(firstDice, secondDigit);
        }

        private bool DiceContains(HashSet<int> dice, int digit)
        {
            if (digit == 9 || digit == 6)
            {
                return dice.Contains(9) || dice.Contains(6);
            }

            return dice.Contains(digit);
        }

        private HashSet<int> DiceToSet(int dice)
        {
            var set = new HashSet<int>();

            for (var i = 100000; i > 0; i /= 10)
            {
                var digit = dice / i;
                set.Add(digit);
                dice %= i;
            }

            return set;
        }

        private bool IsValid(int number)
        {
            var previousDigit = -1;

            for (var i = 100000; i > 0; i /= 10)
            {
                var digit = number / i;
                if (digit <= previousDigit)
                {
                    return false;
                }

                previousDigit = digit;
                number %= i;
            }

            return true;
        }
    }
}