using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem81 : IEulerProblem
    {
        public int ProblemNumber => 81;

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p081_matrix.txt");
            var matrix = new List<List<int>>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var numbers = line.Split(",").Select(int.Parse).ToList();
                matrix.Add(numbers);
            }

            var triangles = MatrixIntoTriangles(matrix);

            var triangle1NodeSums = CalculateWeightedTriangle(triangles[0]);
            var triangle2NodeSums = CalculateWeightedTriangle(triangles[1]);

            var t1LastRow = triangle1NodeSums.First();
            var t2LastRow = triangle2NodeSums.First();

            var allPossibleSums = new List<int>();

            for (var i = 0; i < t1LastRow.Count; i++)
            {
                if (i == 0)
                {
                    allPossibleSums.Add(t1LastRow[i] + t2LastRow[i]);
                }
                else if (i == t1LastRow.Count - 1)
                {
                    allPossibleSums.Add(t1LastRow[i] + t2LastRow[i-1]);
                }
                else
                {
                    allPossibleSums.Add(t1LastRow[i] + t2LastRow[i - 1]);
                    allPossibleSums.Add(t1LastRow[i] + t2LastRow[i]);
                }
            }

            allPossibleSums.Sort();

            return allPossibleSums.First().ToString();
        }

        private List<List<List<int>>> MatrixIntoTriangles(List<List<int>> matrix)
        {
            var triangle1 = new List<List<int>>();
            for (var max = 1; max <= matrix.Count; max++)
            {
                var list = new List<int>();
                for (var x = 0; x < max; x++)
                {
                    for (var y = max -1; y >= 0; y--)
                    {
                        if (x + y == max -1)
                        {
                            list.Add(matrix[y][x]);
                        }
                    }
                }
                triangle1.Add(list);
            }

            var triangle2 = new List<List<int>>();
            var counter = 0;
            for (var max = 2*matrix.Count - 1; max > matrix.Count; max--)
            {
                var list = new List<int>();
                for (var x = matrix.Count - 1; x >= 0; x--)
                {
                    for (var y = matrix.Count - 1 - counter; y > 0 && y < matrix.Count; y++)
                    {
                        if (x + y == max - 1)
                        {
                            list.Add(matrix[x][y]);
                        }
                    }
                }
                triangle2.Add(list);
                counter++;
            }

            return new List<List<List<int>>> { triangle1, triangle2 };
        }

        private List<List<int>> CalculateWeightedTriangle(List<List<int>> givenTriangle)
        {
            var weightedTriangle = new List<List<int>>();

            foreach (var row in givenTriangle)
            {
                weightedTriangle.Add(new List<int>(row));
            }

            for (var i = 0; i < weightedTriangle.Count - 1; i++)
            {
                var currentLine = weightedTriangle[i];
                var nextLine = weightedTriangle[i + 1];

                for (var k = 0; k < nextLine.Count; k++)
                {
                    var index = k;
                    if (index == 0)
                    {
                        index = 0;
                    }
                    else if (index == nextLine.Count - 1)
                    {
                        index--;
                    }
                    else
                    {
                        index = currentLine[index] > currentLine[index - 1] ? index - 1 : index;
                    }

                    nextLine[k] += currentLine[index];
                }
            }

            weightedTriangle.Reverse();
            return weightedTriangle;
        }
    }
}