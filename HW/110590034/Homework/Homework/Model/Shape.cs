using System;
using System.Collections.Generic;

namespace Homework.Model
{
    public class Shape
    {
        protected const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
        protected string _shapeName;
        protected Point _point1;
        protected Point _point2;
        protected Dictionary<Location, Point> _resizePoint;
        protected bool _isFirstPointBottom;
        public enum Location
        {
            LeftTop,
            Top,
            RightTop,
            Left,
            Right,
            LeftBottom,
            Bottom,
            RightBottom,
            None
        }
        public bool IsResizing
        {
            get;
            set;
        }
        public bool IsSelected
        { 
            get;
            set; 
        }

        public Shape()
        {
            _point1 = new Point(-1, -1);
            _point2 = new Point(-1, -1);
            _resizePoint = new Dictionary<Location, Point>();
            ResetResizePoint();
            _shapeName = Constant.Constant.NONE;
        }

        public Shape(Point point1, Point point2)
        {
            _point1 = new Point(point1.X, point1.Y);
            _point2 = new Point(point2.X, point2.Y);
            _resizePoint = new Dictionary<Location, Point>();
            ResetResizePoint();
            _shapeName = Constant.Constant.NONE;
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
        public virtual void Move(double offsetX, double offsetY)
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
        public virtual void DrawHint(IGraphics graphics)
        {
            graphics.DrawHint(_point1.X, _point1.Y, _point2.X, _point2.Y);
            double top = Math.Min(_point1.Y, _point2.Y);
            double bottom = Math.Max(_point1.Y, _point2.Y);
            double left = Math.Min(_point1.X, _point2.X);
            double right = Math.Max(_point1.X, _point2.X);
            graphics.DrawHintCircle(left - Constant.Constant.FOUR, top - Constant.Constant.FOUR, left + Constant.Constant.FOUR, top + Constant.Constant.FOUR);
            graphics.DrawHintCircle(GetMean(left, right) - Constant.Constant.FOUR, top - Constant.Constant.FOUR, GetMean(left, right) + Constant.Constant.FOUR, top + Constant.Constant.FOUR);
            graphics.DrawHintCircle(right - Constant.Constant.FOUR, top - Constant.Constant.FOUR, right + Constant.Constant.FOUR, top + Constant.Constant.FOUR);

            graphics.DrawHintCircle(left - Constant.Constant.FOUR, GetMean(top, bottom) - Constant.Constant.FOUR, left + Constant.Constant.FOUR, GetMean(top, bottom) + Constant.Constant.FOUR);
            graphics.DrawHintCircle(right - Constant.Constant.FOUR, GetMean(top, bottom) - Constant.Constant.FOUR, right + Constant.Constant.FOUR, GetMean(top, bottom) + Constant.Constant.FOUR);

            graphics.DrawHintCircle(left - Constant.Constant.FOUR, bottom - Constant.Constant.FOUR, left + Constant.Constant.FOUR, bottom + Constant.Constant.FOUR);
            graphics.DrawHintCircle(GetMean(left, right) - Constant.Constant.FOUR, bottom - Constant.Constant.FOUR, GetMean(left, right) + Constant.Constant.FOUR, bottom + Constant.Constant.FOUR);
            graphics.DrawHintCircle(right - Constant.Constant.FOUR, bottom - Constant.Constant.FOUR, right + Constant.Constant.FOUR, bottom + Constant.Constant.FOUR);
        }

        // get mean
        public double GetMean(double number1, double number2)
        {
            const double DIVISOR = 2;
            return (number1 + number2) / DIVISOR;
        }

        // reset resize point
        public void ResetResizePoint()
        {
            double top = Math.Min(_point1.Y, _point2.Y);
            double bottom = Math.Max(_point1.Y, _point2.Y);
            double left = Math.Min(_point1.X, _point2.X);
            double right = Math.Max(_point1.X, _point2.X);
            _resizePoint[Location.LeftTop] = new Point(left, top);
            _resizePoint[Location.Top] = new Point(GetMean(left, right), top);
            _resizePoint[Location.RightTop] = new Point(right, top);
            _resizePoint[Location.Left] = new Point(left, GetMean(top, bottom));
            _resizePoint[Location.Right] = new Point(right, GetMean(top, bottom));
            _resizePoint[Location.LeftBottom] = new Point(left, bottom);
            _resizePoint[Location.Bottom] = new Point(GetMean(left, right), bottom);
            _resizePoint[Location.RightBottom] = new Point(right, bottom);
        }

        // update point
        public virtual void UpdatePoint(Location location)
        {
            if (location == Location.RightBottom)
            {
                Point1.X = _resizePoint[Location.LeftTop].X;
                Point1.Y = _resizePoint[Location.LeftTop].Y;
                Point2.X = _resizePoint[Location.RightBottom].X;
                Point2.Y = _resizePoint[Location.RightBottom].Y;
            }
        }

        // set resize point
        public void SetResizePoint(Location location, Point point)
        {
            //Console.WriteLine("set resize point");
            //Console.WriteLine(location);
            _resizePoint[location] = point;
        }

        // check in resize point
        public bool IsInResizePoint(Location location, double pointX, double pointY)
        {
            //Console.WriteLine("hihihihihi");
            //Console.WriteLine(location);
            //Console.WriteLine(pointX);
            //Console.WriteLine(pointY);
            return (pointX >= _resizePoint[location].X - Constant.Constant.FOUR && pointY >= _resizePoint[location].Y - Constant.Constant.FOUR && pointX <= _resizePoint[location].X + Constant.Constant.FOUR && pointY <= _resizePoint[location].Y + Constant.Constant.FOUR);
        }

        // in resize point
        public Location GetLocation(double pointX, double pointY)
        {
            ResetResizePoint();
            //foreach (var iii in _resizePoint)
            //{
            //    Console.WriteLine(iii.Key);
            //    Console.WriteLine(iii.Value);
            //    Console.WriteLine(iii.Value.X);
            //    Console.WriteLine(iii.Value.Y);
            //}
            foreach (var resizePoint in _resizePoint)
            {
                if (IsInResizePoint(resizePoint.Key, pointX, pointY))
                {
                    //Console.WriteLine("in the point");
                    return resizePoint.Key;
                }
            }
            return Location.None;
        }

        // resolve feature envy = =
        // reset point without resizing
        public void ResetPointWithoutResizing()
        {
            if (!IsResizing)
                ResetPoint();
        }

        // get is first point bottom
        public bool IsFirstPointBottom
        {
            get
            {
                return _isFirstPointBottom;
            }
        }

        // check point1.y is top or bottom for line
        public void SetFirstPointBottom()
        {
            double bottom = Math.Max(_point1.Y, _point2.Y);
            _isFirstPointBottom = _point1.Y == bottom;
        }
    }
}
