using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProjectEuler.Problems.Problem766
{
    internal class Grid
    {
        private readonly int _height;
        private readonly int _width;
        private readonly Block _emptyBlock = new Block(' ', Shape.ShapeType.Empty);
        
        public Grid(int height, int width)
        {
            GridState = new List<List<Block>>();
            Shapes = new List<Shape>();

            _height = height;
            _width = width;

            for (var h = 0; h < height; h++)
            {
                GridState.Add(new List<Block>());

                for (var w = 0; w < width; w++)
                {
                    GridState[h].Add(_emptyBlock);
                }
            }
        }

        public List<List<Block>> GridState;

        public List<Shape> Shapes;

        public bool CanPlace(Shape shape, GridCoordinates coordinates)
        {
            foreach (var coordinatePair in shape.ShapeCoordinates)
            {
                var gridX = coordinatePair.X + coordinates.X;
                var gridY = coordinatePair.Y + coordinates.Y;

                if (gridX >= _width || gridX < 0)
                {
                    return false;
                }

                if (gridY >= _height || gridY < 0)
                {
                    return false;
                }

                if (GridState[gridY][gridX].Id != ' ' && GridState[gridY][gridX].Id != shape.Id)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanMove(Shape shape, Problem766.Direction direction)
        {
            GetXYChange(direction, out var xChange, out var yChange);
            return CanPlace(shape, new GridCoordinates(xChange, yChange));
        }

        private void GetXYChange(Problem766.Direction direction, out int xChange, out int yChange)
        {
            yChange = 0;
            xChange = 0;

            switch (direction)
            {
                case Problem766.Direction.Up:
                    yChange = -1;
                    break;
                case Problem766.Direction.Down:
                    yChange = 1;
                    break;
                case Problem766.Direction.Left:
                    xChange = -1;
                    break;
                case Problem766.Direction.Right:
                    xChange = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public (bool Moved, Grid NewGrid) Move(Shape shape, Problem766.Direction direction)
        {
            if (!CanMove(shape, direction))
            {
                return (false, null);
            }

            GetXYChange(direction, out var xChange, out var yChange);

            var newGrid = this.Copy();

            var coordinatesMovementFrom = new List<GridCoordinates>();
            var coordinatesMovementTo = new List<GridCoordinates>();

            for (var i = 0; i < shape.ShapeCoordinates.Count; i++)
            {
                var coordinate = shape.ShapeCoordinates[i];

                var newX = coordinate.X + xChange;
                var newY = coordinate.Y + yChange;

                coordinatesMovementFrom.Add(new GridCoordinates(coordinate.X, coordinate.Y));
                coordinatesMovementTo.Add(new GridCoordinates(newX, newY));
                
                newGrid.GridState[newY][newX] = shape.Block;
                newGrid.Shapes.First(x => x.Id == shape.Id).ShapeCoordinates[i] =
                    new GridCoordinates(newX, newY);
            }

            foreach(var emptyCoordinate in coordinatesMovementFrom.Where(from =>
                !coordinatesMovementTo.Any(to => to.X == from.X && to.Y == from.Y)))
            {
                newGrid.GridState[emptyCoordinate.Y][emptyCoordinate.X] = _emptyBlock;
            }

            return (true, newGrid);
        }

        private Grid Copy()
        {
            var newGrid = new Grid(_height, _width);

            for (var row = 0; row < GridState.Count; row++)
            {
                for (var block = 0; block < GridState[row].Count; block++)
                {
                    newGrid.GridState[row][block] = new Block(GridState[row][block].Id, GridState[row][block].Type);
                }
            }

            foreach (var shape in Shapes)
            {
                newGrid.Shapes.Add(shape.Copy());
            }

            return newGrid;
        }

        public bool Place(Shape shape, GridCoordinates coordinates)
        {
            if (!CanPlace(shape, coordinates))
            {
                return false;
            }

            foreach (var coordinatePair in shape.ShapeCoordinates)
            {
                var gridX = coordinatePair.X + coordinates.X;
                var gridY = coordinatePair.Y + coordinates.Y;

                GridState[gridY][gridX] = shape.Block;
                coordinatePair.X = gridX;
                coordinatePair.Y = gridY;
            }

            Shapes.Add(shape);

            return true;
        }

        public void PrintIdGrid()
        {
            Print(true);
        }

        public void PrintTypeGrid()
        {
            Print(false);
        }

        private void Print(bool byId)
        {
            Console.WriteLine("-----------------");
            foreach (var row in GridState)
            {
                PrintRow(row, byId);
            }
            Console.WriteLine("-----------------");
        }

        private void PrintRow(List<Block> row, bool byId)
        {
            var str = "|";
            foreach (var c in row)
            {
                if (byId)
                {
                    str += $"{c.Id}|";
                }
                else
                {
                    str += $"{(int)c.Type}|";
                }
            }
            Console.WriteLine(str);
        }
    }

    internal class Block
    {
        public Block(char id, Shape.ShapeType type)
        {
            Id = id;
            Type = type;
        }
        public char Id;
        public Shape.ShapeType Type;
    }
}