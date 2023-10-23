using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Shape
    {
        protected const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        protected string _shapeName;
        protected Point _point1;
        protected Point _point2;

        public Shape()
        {
            _point1 = new Point(-1, -1);
            _point2 = new Point(-1, -1);
        }
        
        public Shape(Point point1, Point point2)
        {
            _point1 = new Point(point1.X, point1.Y);
            _point2 = new Point(point2.X, point2.Y);
        }

        // for databinding
        public string ShapeName
        {
            get
            {
                return GetShapeName();
            }
        }

        // for databinding
        public string Info
        {
            get
            {
                return GetInfo();
            }
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

        // draw
        public virtual void Draw(IGraphics graphics)
        {
            throw new NotImplementedException(NOT_IMPLEMENTED);
        }

        // set point1
        public void SetPoint1(Point point)
        {
            _point1 = point;
            //_point1.X = point.X;
            //_point1.Y = point.Y;
        }

        // set point2
        public void SetPoint2(Point point)
        {
            _point2 = point;
            //_point2.X = point.X;
            //_point2.Y = point.Y;
        }
    }
}
