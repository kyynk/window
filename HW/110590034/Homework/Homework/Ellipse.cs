using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Circle : Shape
    {
        private const string RECTANGLE_CHINESE = "圓形";
        private const string COMMA = ", ";
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";

        public Circle() : base()
        {
            _shapeName = RECTANGLE_CHINESE;
            _position = new List<List<int>>();
            SetPosition();
        }

        // set info by random
        public override void SetPosition()
        {
            base.SetPosition();
        }

        // get shape name
        public override string GetShapeName()
        {
            return base.GetShapeName();
        }

        // get info (position)
        public override string GetInfo()
        {
            string coordinate = LEFT_PARENTHESIS + _position[0][0] + COMMA + _position[0][1] + RIGHT_PARENTHESIS;
            return coordinate;
        }
    }
}
