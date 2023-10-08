using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shape
    {
        protected const int MIN_X_RANGE = 150;
        protected const int MAX_X_RANGE = 800;
        protected const int MIN_Y_RANGE = 50;
        protected const int MAX_Y_RANGE = 600;
        protected const string COMMA = ", ";
        protected const string LEFT_PARENTHESIS = "(";
        protected const string RIGHT_PARENTHESIS = ")";
        protected ShapeType _shapeType;
        protected string _shapeName;
        protected Random _randomNumber;
        protected List<List<int>> _info;

        public Shape()
        {
            _shapeType = new ShapeType();
            _shapeName = "";
            _randomNumber = new Random();
            _info = new List<List<int>>();
        }

        // get name
        public string GetShapeName()
        {
            return _shapeName;
        }

        // get location
        public List<List<int>> GetInfo()
        {
            return _info;
        }

        // get location by string
        public string GetInfoByString()
        {
            string firstCoordinate = LEFT_PARENTHESIS + _info[0][0] + COMMA + _info[0][1] + RIGHT_PARENTHESIS;
            string secondCoordinate = LEFT_PARENTHESIS + _info[1][0] + COMMA + _info[1][1] + RIGHT_PARENTHESIS;
            return firstCoordinate + COMMA + secondCoordinate;
        }

        // set location
        public void SetInfo()
        {
            List<int> location1 = new List<int>
            { 
                _randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE) };
            List<int> location2 = new List<int>
            { 
                _randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE) };
            _info.Add(location1);
            _info.Add(location2);
        }
    }
}
