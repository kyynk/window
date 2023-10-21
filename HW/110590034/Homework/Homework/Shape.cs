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
        protected const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        protected string _shapeName;
        protected Random _randomNumber;
        protected List<List<int>> _position;

        public Shape()
        {
            _randomNumber = new Random();
        }

        // get name
        public virtual string GetShapeName()
        {
            return _shapeName;
        }

        // get position
        public virtual string GetInfo()
        {
            throw new NotImplementedException(NOT_IMPLEMENTED);
        }

        // set position by random
        public virtual void SetPosition()
        {
            List<int> coordinate1;
            coordinate1 = new List<int> 
            {
                _randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE),
                _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)
            };
            List<int> coordinate2;
            coordinate2 = new List<int> 
            {
                _randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE),
                _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE)
            };
            _position.Add(coordinate1);
            _position.Add(coordinate2);
        }
    }
}
