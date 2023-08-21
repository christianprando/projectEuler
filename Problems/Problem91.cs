using System;

namespace ProjectEuler.Problems
{
    internal class Problem91 : IEulerProblem
    {
        public int ProblemNumber => 91;

        public string Run()
        {
            var max = 50;
            var sum = 0;

            for (var x1 = 0; x1 <= max; x1++)
            {
                for (var y1 = 0; y1 <= max; y1++)
                {
                    for (var x2 = 0; x2 <= max; x2++)
                    {
                        for (var y2 = 0; y2 <= max; y2++)
                        {
                            if (x1 == y1 && y1 == 0)
                            {
                                continue;
                            }

                            if (x2 == y2 && y2 == 0)
                            {
                                continue;
                            }

                            if (x1 != x2 || y1 != y2)
                            {
                                if (IsARightTriangle(x1, y1, x2, y2))
                                {
                                    Console.WriteLine($"{{{x1},{y1}}},{{{x2},{y2}}}");
                                    sum += 1;
                                }
                            }
                        }
                    }
                }
            }

            sum /= 2;

            return sum.ToString();
        }

        private bool IsARightTriangle(int x1, int y1, int x2, int y2)
        {
            var h = y1 * y1 + x1 * x1;
            var a = y2 * y2 + x2 * x2;
            var b1 = Math.Abs(x2 - x1);
            var b2 = Math.Abs(y2 - y1);
            var b = b1 * b1 + b2 * b2;

            return h == a + b || a == h + b || b == h + a;
        }
    }
}