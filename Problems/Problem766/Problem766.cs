using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace ProjectEuler.Problems.Problem766
{
    internal class Problem766 : IEulerProblem
    {
        internal enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public int ProblemNumber => 766;

        private List<Grid> _gridStates = new List<Grid>();

        private Dictionary<char, int> _placements = new Dictionary<char, int>();

        public string Run()
        {
            var grid = CreateAndPlaceShapes5by6();
            _gridStates.Add(grid);

            foreach (var shape in grid.Shapes)
            {
                _placements.Add(shape.Id, 0);
            }

            for (var i = 0; i < _gridStates.Count; i++)
            {
                CountNewStates(_gridStates[i]);
            }

            foreach (var placements in _placements)
            {
                Console.WriteLine($"ID:{placements.Key} | Placements: {placements.Value}");
            }

            return _gridStates.Count.ToString();
        }

        private List<Direction> _allDirections = new List<Direction>
            { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        private void CountNewStates(Grid grid)
        {
            foreach (var shape in grid.Shapes)
            {
                foreach (var direction in _allDirections)
                {
                    if (!grid.CanMove(shape, direction))
                    {
                        continue;
                    }
                    
                    var result = grid.Move(shape, direction);
                    if (result.Moved && IsNewState(result.NewGrid))
                    {
                        result.NewGrid.PrintIdGrid();
                        _gridStates.Add(result.NewGrid);
                        _placements[shape.Id]++;
                    }
                }
            }
        }

        private bool IsNewState(Grid newGrid)
        {
            return !_gridStates.Any(x => AreEquivalent(x, newGrid));
        }

        private Grid CreateAndPlaceShapes3by4()
        {
            var grid = new Grid(3, 4);
            var greenL = new Shape('L', Shape.ShapeType.GreenL, new List<GridCoordinates>
            {
                new GridCoordinates(0, 0),
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
            });

            grid.Place(greenL, new GridCoordinates(0, 0));
            
            //var square1 = new Shape('1', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square2 = new Shape('2', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square3 = new Shape('3', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square4 = new Shape('4', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square5 = new Shape('5', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square6 = new Shape('6', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square7 = new Shape('7', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });

            //grid.Place(square1, new GridCoordinates(2, 0));
            //grid.Place(square2, new GridCoordinates(1, 1));
            //grid.Place(square3, new GridCoordinates(2, 1));
            //grid.Place(square4, new GridCoordinates(0, 2));
            //grid.Place(square5, new GridCoordinates(1, 2));
            //grid.Place(square6, new GridCoordinates(2, 2));
            //grid.Place(square7, new GridCoordinates(3, 2));

            grid.PrintIdGrid();

            return grid;
        }

        private Grid CreateAndPlaceShapes5by6()
        {
            var grid = new Grid(5, 6);
            var id = 'A';

            var redL1 = new Shape(id++, Shape.ShapeType.RedL, new List<GridCoordinates>
            {
                new GridCoordinates(0, 0),
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
            });
            var redL2 = new Shape(id++, Shape.ShapeType.RedL, new List<GridCoordinates>
            {
                new GridCoordinates(0, 0),
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
            });

            grid.Place(redL1, new GridCoordinates(1, 0));
            grid.Place(redL2, new GridCoordinates(4, 0));

            var greenL1 = new Shape(id++, Shape.ShapeType.GreenL, new List<GridCoordinates>
            {
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
                new GridCoordinates(1, 1),
            });
            var greenL2 = new Shape(id++, Shape.ShapeType.GreenL, new List<GridCoordinates>
            {
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
                new GridCoordinates(1, 1),
            });

            grid.Place(greenL1, new GridCoordinates(2, 0));
            grid.Place(greenL2, new GridCoordinates(4, 3));

            //var square1 = new Shape('1', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square2 = new Shape('2', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square3 = new Shape('3', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square4 = new Shape('4', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square5 = new Shape('5', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });
            //var square6 = new Shape('6', Shape.ShapeType.SmallSquare, new List<GridCoordinates> { new GridCoordinates(0, 0) });

            //grid.Place(square1, new GridCoordinates(0, 2));
            //grid.Place(square2, new GridCoordinates(0, 3));
            //grid.Place(square3, new GridCoordinates(0, 4));
            //grid.Place(square4, new GridCoordinates(1, 2));
            //grid.Place(square5, new GridCoordinates(1, 3));
            //grid.Place(square6, new GridCoordinates(1, 4));

            var largeSquare = new Shape(id++, Shape.ShapeType.BigSquare, new List<GridCoordinates>
            {
                new GridCoordinates(0, 0),
                new GridCoordinates(0, 1),
                new GridCoordinates(1, 0),
                new GridCoordinates(1, 1),
            });

            grid.Place(largeSquare, new GridCoordinates(2, 2));

            //var horizontalLine = new Shape(id++, Shape.ShapeType.Horizontal, new List<GridCoordinates>
            //{
            //    new GridCoordinates(0, 0),
            //    new GridCoordinates(1, 0)
            //});

            //grid.Place(horizontalLine, new GridCoordinates(2, 4));

            //var verticalLine1 = new Shape(id++, Shape.ShapeType.Vertical, new List<GridCoordinates>
            //{
            //    new GridCoordinates(0, 0),
            //    new GridCoordinates(0, 1)
            //});
            //var verticalLine2 = new Shape(id++, Shape.ShapeType.Vertical, new List<GridCoordinates>
            //{
            //    new GridCoordinates(0, 0),
            //    new GridCoordinates(0, 1)
            //});

            //grid.Place(verticalLine1, new GridCoordinates(4, 2));
            //grid.Place(verticalLine2, new GridCoordinates(5, 1));

            grid.PrintIdGrid();

            return grid;
        }

        private bool AreEquivalent(Grid grid1, Grid grid2)
        {
            for (var row = 0; row < grid1.GridState.Count; row++)
            {
                for (var block = 0; block < grid1.GridState[row].Count; block++)
                {
                    if (grid1.GridState[row][block].Type != grid2.GridState[row][block].Type)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}