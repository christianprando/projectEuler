using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem82 : IEulerProblem
    {
        public int ProblemNumber => 82;

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p082_matrix.txt");
            var matrix = new List<List<int>>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var numbers = line.Split(",").Select(int.Parse).ToList();
                matrix.Add(numbers);
            }

            for (var i = matrix.Count - 1; i > 0; i--)
            {
                PutWeightInColumn(matrix, i - 1, i);
            }

            var smallest = int.MaxValue;
            for (var row = 0; row < matrix.Count; row++)
            {
                if (matrix[row][0] < smallest)
                {
                    smallest = matrix[row][0];
                }
            }

            return smallest.ToString();
        }

        private void PutWeightInColumn(List<List<int>> matrix, int leftColumn, int rightColumn)
        {
            var tempLeftColumn = new List<int>();
            for (var row = 0; row < matrix.Count; row++)
            {
                tempLeftColumn.Add(ChooseShortestValue(matrix, leftColumn, rightColumn, row));
            }
            for (var row = 0; row < matrix.Count; row++)
            {
                matrix[row][leftColumn] = tempLeftColumn[row];
            }
        }

        private int ChooseShortestValue(List<List<int>> matrix, int leftColumn, int rightColumn, int row)
        {
            var availableSums = new List<int>();
            for (var targetRow = 0; targetRow < matrix.Count; targetRow++)
            {
                availableSums.Add(FindShortestPath(matrix, leftColumn, rightColumn, row, targetRow));
            }
            availableSums.Sort();
            return availableSums.First();
        }

        private int FindShortestPath(List<List<int>> matrix, int leftColumn, int rightColumn, int sourceRow, int targetRow)
        {
            int max;
            int mod;
            if (targetRow > sourceRow)
            {
                max = targetRow - sourceRow;
                mod = 1;
            }
            else
            {
                max = sourceRow - targetRow;
                mod = -1;
            }

            //Console.WriteLine($"FROM: ({leftColumn},{sourceRow}) TO: ({rightColumn},{targetRow})");

            var sums = new List<int>();
            for (var switchRowCounter = 0; switchRowCounter <= max; switchRowCounter++)
            {
                var switchRow = sourceRow + switchRowCounter * mod;

                var sum = matrix[sourceRow][leftColumn];
                //Console.WriteLine($"({leftColumn},{sourceRow})");
                var xd = 0;
                var yd = 0;
                for (var row = sourceRow; mod > 0 ? row < targetRow : row > targetRow; row += mod)
                {
                    if (row == switchRow)
                    {
                        xd = 1;
                    }
                    else if (mod > 0 ? row <= switchRow : row >= switchRow)
                    {
                        yd = mod;
                    }
                    else if (row == targetRow && xd == 1)
                    {
                        continue;
                    }

                    sum += matrix[row + yd][leftColumn + xd];
                    //Console.WriteLine($"({leftColumn + xd},{row + yd})");
                    yd = 0;
                }

                //Console.WriteLine($"({rightColumn},{targetRow})");
                sum += matrix[targetRow][rightColumn];
                sums.Add(sum);
                //Console.WriteLine($"---------");
            }

            sums.Sort();

            return sums.First();
        }
    }
}