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
        public bool isSelected
        { 
            get;
            set; 
        }

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
        public Point Point1
        {
            get
            {
                return _point1;
            }
            set
            {
                _point1 = value;
            }
        }

        // set point2
        public Point Point2
        {
            get
            {
                return _point2;
            }
            set
            {
                _point2 = value;
            }
        }

        // change point1 to left top, point2 to right buttom
        public virtual void ResetPoint()
        {
            double top = Math.Min(_point1.Y, _point2.Y);
            double bottom = Math.Max(_point1.Y, _point2.Y);
            double left = Math.Min(_point1.X, _point2.X);
            double right = Math.Max(_point1.X, _point2.X);
            _point1.X = left;
            _point1.Y = top;
            _point2.X = right;
            _point2.Y = bottom;
        }

        // move shape
        public void Move(double offsetX, double offsetY)
        {
            _point1.X -= offsetX;
            _point2.X -= offsetX;
            _point1.Y -= offsetY;
            _point2.Y -= offsetY;
        }

        // check select
        public bool CheckSelect(double pointX, double pointY)
        {
            double top = Math.Min(_point1.Y, _point2.Y);
            double bottom = Math.Max(_point1.Y, _point2.Y);
            double left = Math.Min(_point1.X, _point2.X);
            double right = Math.Max(_point1.X, _point2.X);
            bool checkX = (left <= pointX && right >= pointX);
            bool checkY = (top <= pointY && bottom >= pointY);
            return (checkX && checkY);
        }

        // draw hint
        public void DrawHint(IGraphics graphics)
        {
            graphics.DrawHint(_point1.X, _point1.Y, _point2.X, _point2.Y);
        }
    }
}
