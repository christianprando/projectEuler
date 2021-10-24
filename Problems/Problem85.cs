using System;

namespace ProjectEuler.Problems
{
    internal class Problem85 : IEulerProblem
    { 
        public int ProblemNumber => 85;
        public string Run()
        {
            var goal = 2000000;

            var maxSquareGrid = GetMaxSquareGrid(goal);

            return (maxSquareGrid.Height * maxSquareGrid.Width).ToString();
        }

        private Grid GetMaxSquareGrid(int goal)
        {
            var gridWidth = 1;
            var gridHeight = 1;
            var bestCandidate = new Grid(0,0,0);
            
            while (true)
            {
                var rectangles = 0;
                for (var h = 1; h <= gridHeight; h++)
                {
                    for (var w = 1; w <= gridWidth; w++)
                    {
                        rectangles += (gridWidth - (w - 1)) * (gridHeight - (h - 1));
                    }
                }
                
                Console.WriteLine($"Grid: {gridWidth}x{gridHeight} | #Rectangles: {rectangles}");

                if (Math.Abs(bestCandidate.Rectangles - goal) > Math.Abs(rectangles - goal))
                {
                    bestCandidate = new Grid(gridWidth, gridHeight, rectangles);
                }

                gridHeight++;

                if (rectangles > goal)
                {
                    if (gridWidth >= gridHeight)
                    {
                        return bestCandidate;
                    }
                    gridHeight = 0;
                    gridWidth++;
                }
            }
        }
    }

    internal class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public int Rectangles { get; }

        public Grid(int width, int height, int rectangles)
        {
            Width = width;
            Height = height;
            Rectangles = rectangles;
        }
    }
}