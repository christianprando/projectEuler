using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EulerProject
{
    internal class Problem67 : IEulerProblem
    {
        public int ProblemNumber => 67;

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p067_triangle.txt");
            var triangle = new List<List<int>>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var numbers = line.Split(" ").Select(int.Parse).ToList();
                triangle.Add(numbers);
            }

            triangle.Reverse();

            for(var i=0; i < triangle.Count - 1; i++)
            {
                var currentLine = triangle[i];
                var nextLine = triangle[i + 1];

                for (var k = 0; k < nextLine.Count; k++)
                {
                    var highestChild = currentLine[k] > currentLine[k + 1]
                        ? currentLine[k]
                        : currentLine[k + 1];

                    nextLine[k] += highestChild;
                }
            }
            
            return triangle.Last()[0].ToString();
        }
    }
}