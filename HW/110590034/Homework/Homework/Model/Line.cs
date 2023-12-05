using System;

namespace Homework.Model
{
    public class Line : Shape
    {
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Line(Point point1, Point point2) : base(point1, point2)
        {
            _shapeName = Constant.Constant.LINE;
        }

        // draw
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_point1.X, _point1.Y, _point2.X, _point2.Y);
        }

        // get shape name
        public override string GetShapeName()
        {
            return _shapeName;
        }

        // get info (position)
        public override string GetInfo()
        {
            ResetPointWithoutResizing();
            string firstCoordinate = LEFT_PARENTHESIS + _point1.X + COMMA + _point1.Y + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _point2.X + COMMA + _point2.Y + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }

        // change point1 to left, point2 to right
        public override void ResetPoint()
        {
            double left = Math.Min(_point1.X, _point2.X);
            if (_point1.X != left)
            {
                Point temp = _point1;
                _point1 = _point2;
                _point2 = temp;
            }
        }

        // update point
        public override void UpdatePoint(Location location)
        {
            if (location == Location.RightBottom && IsFirstMethod())
            {
                _point1.Y = _resizePoint[Location.RightBottom].Y;
                _point2.X = _resizePoint[Location.RightBottom].X;
            }
            else if (location == Location.RightBottom && !IsFirstMethod())
            {
                _point2 = _resizePoint[Location.RightBottom];
            }
        }

        // is method 1 (point1.y > point2.y)
        public bool IsFirstMethod()
        {
            return IsFirstPointBottom;
        }

    }
}
