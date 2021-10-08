using System;
using System.Collections.Generic;

namespace ProjectEuler.Problems.Problem766
{
    internal class Shape
    {
        internal enum ShapeType
        {
            Empty,
            GreenL,
            SmallSquare,
            RedL,
            BigSquare,
            Horizontal,
            Vertical,
        }

        public Shape(char id, ShapeType type, List<GridCoordinates> coordinates)
        {
            Type = type;
            Id = id;
            ShapeCoordinates = coordinates;
            Block = new Block(id, type);
        }

        public ShapeType Type;
        public char Id;
        public List<GridCoordinates> ShapeCoordinates;
        public Block Block { get; }

        public int Placements;

        public Shape Copy()
        {
            var newShape = new Shape(Id, Type, new List<GridCoordinates>());

            foreach (var coordinates in ShapeCoordinates)
            {
                newShape.ShapeCoordinates.Add(new GridCoordinates(coordinates.X, coordinates.Y));
            }

            return newShape;
        }
    }
}