using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem79 : IEulerProblem
    {
        public int ProblemNumber => 79;

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p079_keylog.txt");
            string line;

            var passwords = new List<Password>();
            while ((line = file.ReadLine()) != null)
            {
                passwords.Add(ProcessLine(line));
            }

            var secret = new List<int>{passwords.First().First, passwords.First().Middle, passwords.First().Last };
            
            foreach (var password in passwords)
            {
                if (secret.Last() == password.Middle)
                {
                    if (!IsValid(secret, password))
                    {
                        secret.Add(password.Last);
                    }
                }
                else if (secret.First() == password.Middle)
                {
                    if (!IsValid(secret, password))
                    {
                        secret.Insert(0, password.First);
                    }
                }
                else
                {
                    if (!IsValid(secret, password))
                    {
                        TryInsert(secret, password);
                    }
                }
            }

            var answer = string.Empty;
            foreach (var i in secret)
            {
                answer += i;
            }

            return answer;
        }

        private void TryInsert(List<int> secret, Password password)
        {
            var prev = -1;
            var index = 0;
            foreach (var digit in secret)
            {
                if (prev == password.First && digit == password.Last)
                {
                    secret.Insert(index, password.Middle);
                    break;
                }

                prev = digit;
                index++;
            }
        }

        private bool IsValid(List<int> secret, Password password)
        {
            if (!secret.Contains(password.First) || !secret.Contains(password.Middle) ||
                !secret.Contains(password.Last))
            {
                return false;
            }

            var firstIndex = secret.FindIndex(x => x == password.First);
            var lastIndex = secret.FindIndex(x => x == password.Last);
            var middleIndexes = FindAllIndexesOf(secret, password.Middle);

            return middleIndexes.Any(x => x < lastIndex) && middleIndexes.Any(x => x > firstIndex);
        }

        private List<int> FindAllIndexesOf(IEnumerable<int> secret, int digit)
        {
            return secret.Select((x, i) => x == digit ? i : -1).Where(i => i != -1).ToList();
        }

        private class Password
        {
            public Password(int first, int middle, int last)
            {
                First = first;
                Middle = middle;
                Last = last;
            }
            public int First { get; }
            public int Middle { get; }
            public int Last { get; }
        }

        private Password ProcessLine(string line)
        {
            var list = line.Select(x => int.Parse(x.ToString())).ToList();
            return new Password(list.First(), list.ElementAt(1), list.Last());
        }
    }
}