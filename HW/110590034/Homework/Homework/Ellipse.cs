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

        // set info by random
        public override void SetPosition()
        {
            Point point = new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE));
            _position.Add(point);
        }

        // get shape name
        public override string GetShapeName()
        {
            return base.GetShapeName();
        }

        // get info (position)
        public override string GetInfo()
        {
            string coordinate = LEFT_PARENTHESIS + _position[0].X + COMMA + _position[0].Y + RIGHT_PARENTHESIS;
            return coordinate;
        }
    }
}
