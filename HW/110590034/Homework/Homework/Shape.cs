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
        protected const int MIN_Y_RANGE = 100;
        protected const int MAX_Y_RANGE = 550;
        protected const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        protected string _shapeName;
        protected Random _randomNumber;
        protected List<Point> _position;

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
            throw new NotImplementedException(NOT_IMPLEMENTED);
        }
    }
}
