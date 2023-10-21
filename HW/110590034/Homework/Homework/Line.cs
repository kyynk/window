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
            _position = new List<List<int>>();
            SetPosition();
        }

        // get shape name
        public override string GetShapeName()
        {
            return base.GetShapeName();
        }

        // get info (position)
        public override string GetInfo()
        {
            string firstCoordinate = LEFT_PARENTHESIS + _position[0][0] + COMMA + _position[0][1] + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _position[1][0] + COMMA + _position[1][1] + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }
    }
}
