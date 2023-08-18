using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem89 : IEulerProblem
    {
        public int ProblemNumber => 89;

        private readonly List<char> _characters = new List<char> { 'M', 'D', 'C', 'L', 'X', 'V', 'I' };
        private readonly Dictionary<char, int> _values = new Dictionary<char, int>() { { 'M', 1000 }, { 'D', 500 }, { 'C', 100 }, { 'L', 50 }, { 'X', 10 }, { 'V', 5 }, { 'I', 1 }, };

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p089_roman.txt");
            string line;
            var charactersSaved = 0;
            while ((line = file.ReadLine()) != null)
            {
                charactersSaved += MinimizeAndReturnCharactersSaved(line);
            }

            return charactersSaved.ToString();
        }

        private int MinimizeAndReturnCharactersSaved(string line)
        {
            var number = RomanToInt(line);
            var roman = IntToRoman(number);

            var savedChars = line.Length - roman.Length;

            if (savedChars < 0)
            {
                Console.WriteLine("WRONG: " + line + " | " + number + " | " + roman);
            }
            else if (savedChars != 0)
            {
                Console.WriteLine(line + " | " + number + " | " + roman);
            }

            return savedChars;
        }

        private string IntToRoman(int number)
        {
            var roman = "";

            for (var i = 1000; i > 0; i /= 10)
            {
                var mult = number / i;

                if (mult == 0)
                {
                    continue;
                }

                if (i < 1000 && mult % 9 == 0)
                {
                    roman += GetNineMultiple(i);
                }
                else if (i < 1000 && mult % 4 == 0 && mult / 4 == 1)
                {
                    roman += GetFourMultiple(i);
                }
                else
                {
                    if (i < 1000 && mult >= 5)
                    {
                        roman += GetFiveMultiple(i);
                        mult -= 5;
                    }
                    for (var j = 0; j < mult; j++)
                    {
                        roman += _values.First(x => x.Value == i).Key;
                    }
                }

                number %= i;
            }

            return roman;
        }

        private string GetNineMultiple(int i)
        {
            switch (i)
            {
                case 100:
                    return "CM";
                case 10:
                    return "XC";
                case 1:
                    return "IX";
            }

            throw new Exception();
        }

        private string GetFourMultiple(int i)
        {
            switch (i)
            {
                case 100:
                    return "CD";
                case 10:
                    return "XL";
                case 1:
                    return "IV";
            }

            throw new Exception();
        }

        private string GetFiveMultiple(int i)
        {
            switch (i)
            {
                case 100:
                    return "D";
                case 10:
                    return "L";
                case 1:
                    return "V";
            }

            throw new Exception();
        }

        private int RomanToInt(string line)
        {
            var charCounter = 0;
            var currentChar = '-';

            var sum = 0;

            for (var i = 0; i < line.Length; i++)
            {
                if (currentChar == line[i] || currentChar == '-')
                {
                    currentChar = line[i];
                    charCounter++;
                }
                else if (NextIsLarger(currentChar, line[i]))
                {
                    sum += _values[line[i]] - _values[currentChar];

                    if (line.Length > i + 1)
                    {
                        i++;
                        currentChar = line[i];
                        charCounter = 1;
                    }
                    else
                    {
                        currentChar = '-';
                    }
                }
                else
                {
                    sum += charCounter * _values[currentChar];
                    currentChar = line[i];
                    charCounter = 1;
                }
            }

            if (currentChar != '-')
            {
                sum += charCounter * _values[currentChar];
            }

            return sum;
        }

        private bool NextIsLarger(char first, char second)
        {
            return _characters.IndexOf(first) > _characters.IndexOf(second);
        }
    }
}