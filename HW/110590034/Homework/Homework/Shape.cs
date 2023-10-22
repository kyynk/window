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

        // draw
        public virtual void Draw(IGraphics graphics)
        {
            throw new NotImplementedException(NOT_IMPLEMENTED);
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
        public void SetPosition()
        {
            Point point1 = new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE));
            Point point2 = new Point(_randomNumber.Next(MIN_X_RANGE, MAX_X_RANGE), _randomNumber.Next(MIN_Y_RANGE, MAX_Y_RANGE));
            _position.Add(point1);
            _position.Add(point2);
        }
    }
}
