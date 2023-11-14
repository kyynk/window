using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Line : Shape
    {
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Line(Point point1, Point point2) : base(point1, point2)
        {
            _shapeName = Constant.LINE_CHINESE;
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
            string firstCoordinate = LEFT_PARENTHESIS + _point1.X + COMMA + _point1.Y + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _point2.X + COMMA + _point2.Y + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }
    }
}
