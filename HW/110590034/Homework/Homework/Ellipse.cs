using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Ellipse : Shape
    {
        private const string CIRCLE_CHINESE = "圓";
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Ellipse() : base()
        {
            _shapeName = CIRCLE_CHINESE;
            _position = new List<Point>();
            SetPosition();
        }

        // draw
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(_position[0].X, _position[0].Y, _position[1].X, _position[1].Y);
        }

        // get shape name
        public override string GetShapeName()
        {
            return _shapeName;
        }

        // get info (position)
        public override string GetInfo()
        {
            string firstCoordinate = LEFT_PARENTHESIS + _position[0].X + COMMA + _position[0].Y + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _position[1].X + COMMA + _position[1].Y + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }
    }
}
