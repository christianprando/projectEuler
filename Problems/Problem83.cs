using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem83 : IEulerProblem
    {
        public int ProblemNumber => 83;

        public string Run()
        {
            var file = new StreamReader(@".\Resources\p083_matrix.txt");
            var matrix = new List<List<int>>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var numbers = line.Split(",").Select(int.Parse).ToList();
                matrix.Add(numbers);
            }

            var nodes = MatrixToGraph(matrix);

            var currentNode = nodes.Aggregate((curMin, x) => (curMin == null || x.Distance < curMin.Distance ? x : curMin));

            while (true)
            {
                foreach (var neighbor in currentNode.Neighbors.Where(n => !n.Visited))
                {
                    if (neighbor.Distance > neighbor.Value + currentNode.Distance)
                    {
                        neighbor.SetDistance(neighbor.Value + currentNode.Distance);
                    }
                }
                currentNode.MarkAsVisited();

                currentNode = nodes.Where(n => !n.Visited).Aggregate((curMin, x) => (curMin == null || x.Distance < curMin.Distance ? x : curMin));

                if (currentNode == nodes.Last())
                {
                    break;
                }
            }

            
            return nodes.Last().Distance.ToString();
        }

        private List<Node> MatrixToGraph(List<List<int>> matrix)
        {
            var nodes = new List<Node>();

            var x = 0;
            foreach (var row in matrix)
            {
                var y = 0;
                nodes.AddRange(row.Select(number => new Node(x, y++, number)));
                x++;
            }

            foreach (var currentNode in nodes)
            {
                var neighbors = new List<Node>()
                {
                    nodes.FirstOrDefault(n => n.X == currentNode.X + 1 && n.Y == currentNode.Y),
                    nodes.FirstOrDefault(n => n.X == currentNode.X && n.Y == currentNode.Y + 1),
                    nodes.FirstOrDefault(n => n.X == currentNode.X - 1 && n.Y == currentNode.Y),
                    nodes.FirstOrDefault(n => n.X == currentNode.X && n.Y == currentNode.Y - 1)
                };

                foreach(var neighbor in neighbors.Where(n => n != null))
                {
                    currentNode.AddNeighbor(neighbor);
                }
            }

            nodes.First().SetDistance(nodes.First().Value);

            return nodes;
        }

        private class Node
        {
            public Node(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
                Distance = int.MaxValue;
                Neighbors = new List<Node>();
            }

            public int X { get; }
            public int Y { get; }
            public int Value { get; }
            public int Distance { get; private set; }
            public bool Visited { get; private set; }
            public List<Node> Neighbors { get; }

            public void MarkAsVisited()
            {
                Visited = true;
            }

            public void AddNeighbor(Node neighbor)
            {
                Neighbors.Add(neighbor);
            }

            public void SetDistance(int i)
            {
                Distance = i;
            }
        }
    }
}