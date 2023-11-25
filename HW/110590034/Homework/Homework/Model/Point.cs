using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
