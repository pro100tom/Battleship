using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleshipGame.CoordinateStuff
{
    public enum Rotation { None, Rotation90, Rotation180, Rotation270 }
    public enum Reflection { None, Vertical, Horizontal, Both }

    public class CoordinateTransformationService
    {
        public List<Coordinate> TranslateCoordinates(List<Coordinate> coordinates, int horizontalOffset, int verticalOffset)
        {
            var translateCoordinates = new List<Coordinate>();

            foreach (var coordinate in coordinates)
            {
                var translatedCoordinate = TranslateCoordinate(coordinate, horizontalOffset, verticalOffset);
                translateCoordinates.Add(translatedCoordinate);
            }

            return translateCoordinates;
        }

        public Coordinate TranslateCoordinate(Coordinate coordinate, int horizontalOffset, int verticalOffset)
        {
            var newIndexColumn = coordinate.Column + horizontalOffset;
            var newIndexRow = coordinate.Row + verticalOffset;

            return new Coordinate(newIndexColumn, newIndexRow);
        }

        public List<Coordinate> TranslateCoordinatesToOrigin(List<Coordinate> coordinates)
        {
            var topmost = coordinates.First().Row;
            var leftmost = coordinates.First().Column;

            foreach (var coordinate in coordinates)
            {
                if (coordinate.Row < topmost)
                    topmost = coordinate.Row;

                if (coordinate.Column < leftmost)
                    leftmost = coordinate.Column;
            }

            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                var column = coordinate.Column - leftmost;
                var row = coordinate.Row - topmost;

                var newCoordinate = new Coordinate(column, row);
                newCoordinates.Add(newCoordinate);
            }

            return newCoordinates;
        }

        public List<Coordinate> RotateCoordinates(List<Coordinate> coordinates, int angle, bool translateToOrigin = true)
        {
            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                var newCoordinate = RotateCoordinate(coordinate, angle);
                newCoordinates.Add(newCoordinate);
            }

            if (translateToOrigin)
                newCoordinates = TranslateCoordinatesToOrigin(newCoordinates);

            return newCoordinates;
        }

        public Coordinate RotateCoordinate(Coordinate coordinate, int angle)
        {
            double angleInRadians = angle * Math.PI / 180.0d;
            var cosOfAngle = Math.Round(Math.Cos(angleInRadians), 2);
            var sinOfAngle = Math.Round(Math.Sin(angleInRadians), 2);

            var matrix = new Matrix(cosOfAngle, sinOfAngle, -sinOfAngle, cosOfAngle, 0.0d, 0.0d);
            var newCoordinate = MultiplyCoordinateByMatrix(coordinate, matrix);

            return newCoordinate;
        }

        public List<Coordinate> ReflectCoordinatesHorizontally(List<Coordinate> coordinates, bool translateToOrigin = true)
        {
            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                var newCoordinate = ReflectCoordinateHorizontally(coordinate);
                newCoordinates.Add(newCoordinate);
            }

            if (translateToOrigin)
                newCoordinates = TranslateCoordinatesToOrigin(newCoordinates);

            return newCoordinates;
        }

        public Coordinate ReflectCoordinateHorizontally(Coordinate coordinate)
        {
            return MultiplyCoordinateByMatrix(coordinate, new Matrix(1, 0, 0, -1, 0, 0));
        }

        public List<Coordinate> ReflectCoordinatesVertically(List<Coordinate> coordinates, bool translateToOrigin = true)
        {
            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                var newCoordinate = ReflectCoordinateVertically(coordinate);
                newCoordinates.Add(newCoordinate);
            }

            if (translateToOrigin)
                newCoordinates = TranslateCoordinatesToOrigin(newCoordinates);

            return newCoordinates;
        }

        public Coordinate ReflectCoordinateVertically(Coordinate coordinate)
        {
            return MultiplyCoordinateByMatrix(coordinate, new Matrix(-1, 0, 0, 1, 0, 0));
        }

        public List<Coordinate> MultiplyCoordinatesByMatrix(List<Coordinate> coordinates, Matrix matrix)
        {
            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                var newCoordinate = MultiplyCoordinateByMatrix(coordinate, matrix);
                newCoordinates.Add(newCoordinate);
            }

            return newCoordinates;
        }

        public Coordinate MultiplyCoordinateByMatrix(Coordinate coordinate, Matrix matrix)
        {
            var x = (int)(coordinate.Column * matrix.M11 + coordinate.Row * matrix.M12);
            var y = (int)(coordinate.Column * matrix.M21 + coordinate.Row * matrix.M22);

            return new Coordinate(x, y);
        }

        public List<Coordinate> TransformCoordinates(List<Coordinate> coordinates, TransformationSettings transformation)
        {
            List<Coordinate> transformedCoordinates;

            if (transformation.RotateFirst)
            {
                var tempCoordinates = RotateCoordinates(coordinates, transformation.Rotation);
                transformedCoordinates = ReflectCoordinates(tempCoordinates, transformation.Reflection);
            }
            else
            {
                var tempCoordinates = ReflectCoordinates(coordinates, transformation.Reflection);
                transformedCoordinates = RotateCoordinates(tempCoordinates, transformation.Rotation);
            }

            return TranslateCoordinates(transformedCoordinates, transformation.HorizontalOffset, transformation.VerticalOfsset);
        }

        public List<Coordinate> RotateCoordinates(List<Coordinate> coordinates, Rotation rotation)
        {
            var rotatedCoordinates = coordinates;
            switch (rotation)
            {
                case Rotation.None:
                    break;
                case Rotation.Rotation90:
                    rotatedCoordinates = RotateCoordinates(coordinates, 90);
                    break;
                case Rotation.Rotation180:
                    rotatedCoordinates = RotateCoordinates(coordinates, 180);
                    break;
                case Rotation.Rotation270:
                    rotatedCoordinates = RotateCoordinates(coordinates, 270);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return rotatedCoordinates;
        }

        public List<Coordinate> ReflectCoordinates(List<Coordinate> coordinates, Reflection reflection)
        {
            var reflectedCoordinates = coordinates;
            switch (reflection)
            {
                case Reflection.None:
                    break;
                case Reflection.Horizontal:
                    reflectedCoordinates = ReflectCoordinatesHorizontally(coordinates);
                    break;
                case Reflection.Vertical:
                    reflectedCoordinates = ReflectCoordinatesVertically(coordinates);
                    break;
                case Reflection.Both:
                    var tempCoordinates = ReflectCoordinatesHorizontally(coordinates);
                    reflectedCoordinates = ReflectCoordinatesVertically(tempCoordinates);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return reflectedCoordinates;
        }
    }
}
