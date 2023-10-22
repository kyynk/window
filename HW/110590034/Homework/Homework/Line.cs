using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Line : Shape
    {
        private const string LINE_CHINESE = "線";
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Line() : base()
        {
            _shapeName = LINE_CHINESE;
            _position = new List<Point>();
            SetPosition();
        }

        // draw
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_position[0].X, _position[0].Y, _position[1].X, _position[1].Y);
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
