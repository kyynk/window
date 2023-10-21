using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Point
    {
        public int X 
        { 
            get;
            set;
        }
        public int Y 
        { 
            get;
            set;
        }

        public Point(int locationX, int locationY)
        {
            X = locationX;
            Y = locationY;
        }
    }
}
