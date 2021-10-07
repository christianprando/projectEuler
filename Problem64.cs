using System;
using System.Diagnostics;

namespace EulerProject
{
    internal class Problem64 : IEulerProblem
    {
        public int ProblemNumber => 64;

        public string Run()
        {
            return CountOffPeriodsUpTo(10000);
        }

        private string CountOffPeriodsUpTo(int limit)
        {
            var oddPeriodCounter = 0;

            for (var i = 2; i <= limit; i++)
            {
                var sqrt = (int)Math.Sqrt(i);
                if (sqrt - Math.Sqrt(i) == 0)
                {
                    continue;
                }

                if (IsOdd(GetPeriod(i)))
                {
                    oddPeriodCounter++;
                }
            }

            return oddPeriodCounter.ToString();
        }

        private bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        public int GetPeriod(int s)
        {
            double m = 0;
            double d = 1;
            var a0 = Math.Floor(Math.Sqrt(s));
            var a = a0;

            var period = 0;

            var stringRepresentation = $"sqrt({s}) = [{a0};(";

            for (var i = 0; ; i++)
            {
                m = d * a - m;
                d = (s - m * m) / d;
                a = Math.Floor((a0 + m) / d);

                period++;

                if (a - 2 * a0 == 0)
                {
                    stringRepresentation += $"{a})] period={period}";
                    //Debug.WriteLine(stringRepresentation);
                    break;
                }

                stringRepresentation += $"{a},";
            }

            return period;
        }
    }
}