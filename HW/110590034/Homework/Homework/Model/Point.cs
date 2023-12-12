using System;

namespace Homework.Model
{
    public class Point
    {
        public double X 
        { 
            get;
            set;
        }
        public double Y 
        { 
            get;
            set;
        }

        public Point(double locationX, double locationY)
        {
            X = locationX;
            Y = locationY;
        }

        // resize point
        public void ResizePoint(double ratio)
        {
            X = Math.Round(X * ratio, 1);
            Y = Math.Round(Y * ratio, 1);
        }
    }
}
